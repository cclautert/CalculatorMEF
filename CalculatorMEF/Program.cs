using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Reflection;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using static BasicOperations.CalculatorMefInterfaces;

public static partial class Program
{
    private readonly static string ApplicationPath = Assembly.GetEntryAssembly().Location;
    private readonly static string ApplicationDirectory = Path.GetDirectoryName(ApplicationPath);

    [Export(typeof(ICalculator))]
    public partial class CalculatorMEF : ICalculator
    {
        [ImportMany()]
        public IEnumerable<Lazy<IOperation, IOperationData>> Operations { get; set; }

        public string Calculate(string input)
        {
            int left, right;
            char operation;
            int fn = FindFirstNonDigit(input); // Finds the operator
            if (fn < 0)
            {
                return "Could not parse command.";
            }

            operation = input[fn];
            try
            {
                left = int.Parse(input.Substring(0, fn));
                right = int.Parse(input.Substring(fn + 1));
            }
            catch (Exception ex)
            {
                return "Could not parse command.";
            }

            foreach (Lazy<IOperation, IOperationData> i in Operations)
            {
                if (i.Metadata.Symbol == operation)
                {
                    return i.Value.Operate(left, right).ToString();
                }
            }

            return "Operation not found!";
        }

        private int FindFirstNonDigit(string s)
        {
            for (int i = 0, loopTo = (s?.Length ?? 0) - 1; i <= loopTo; i++)
            {
                if (!char.IsDigit(s[i]))
                    return i;
            }

            return -1;
        }
    }

    private partial class CalculatorMEFComposition
    {
        private readonly CompositionContainer _container;
        public readonly bool HasExtensions;

        [Import(typeof(ICalculator))]
        public ICalculator Calculator { get; set; }

        public CalculatorMEFComposition()
        {
            // An aggregate catalog that combines multiple catalogs
            var catalog = new AggregateCatalog();

            // Adds all the parts found in the MefCalculator assembly (class MefCalculatorInterfaces)
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(BasicOperations.CalculatorMefInterfaces).Assembly));

            // Adds all the parts found in the same assembly as this class (MySimpleCalculatorComposer)
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(CalculatorMEFComposition).Assembly));

            // Add parts which can be found in the Extensions subdirectory.
            string extensionsDir = Path.Combine(ApplicationDirectory, "Extensions");
            if (Directory.Exists(extensionsDir))
            {
                catalog.Catalogs.Add(new DirectoryCatalog(extensionsDir));
                HasExtensions = true;
            }

            // Create the CompositionContainer with the parts in the catalog
            _container = new CompositionContainer(catalog);

            // Fill the imports of this object
            try
            {
                _container.ComposeParts(this);
            }
            catch (Exception ex)
            {
                FileSystem.WriteLine(Conversions.ToInteger(ex.ToString()));
            }
        }
    }    

    public static void Main(string[] args)
    {
        var o = new CalculatorMEFComposition();

        if (o.HasExtensions)
            Console.WriteLine("Extensions loaded.");

        Console.WriteLine("Enter Command:");
        do
        {
            var s = Console.ReadLine();
            if (s is null)
                break;
            Console.WriteLine(o.Calculator.Calculate(s));
        }
        while (true);

        Console.WriteLine(Conversions.ToInteger("Exited"));
    }
}