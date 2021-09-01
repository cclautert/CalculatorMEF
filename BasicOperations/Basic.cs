using System.ComponentModel.Composition;

namespace BasicOperations
{
    public static partial class CalculatorMefInterfaces
    {
        public partial interface ICalculator
        {
            string Calculate(string input);
        }

        public partial interface IOperation
        {
            int Operate(int left, int right);
            int Version();
        }

        //public partial interface IOperation
        //{
        //    int Version();
        //}

        public partial interface IOperationData
        {
            char Operador { get; }
        }

        [Export(typeof(IOperation))]
        [ExportMetadata("Operador", '+')]
        public partial class Add : IOperation
        {
            public int Operate(int left, int right)
            {
                return left + right;
            }

            public int Version()
            {
                return 1;
            }
        }

        [Export(typeof(IOperation))]
        [ExportMetadata("Operador", '-')]
        public partial class Subtract : IOperation
        {
            public int Operate(int left, int right)
            {
                return left - right;
            }

            public int Version()
            {
                return 1;
            }
        }
    }
}
