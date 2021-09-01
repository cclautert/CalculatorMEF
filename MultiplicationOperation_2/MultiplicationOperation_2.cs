using System.ComponentModel.Composition;
using static BasicOperations.CalculatorMefInterfaces;

namespace MultiplicationOperation_2
{
    [Export(typeof(IOperation))]
    [ExportMetadata("Operador", '*')]
    public class Multiplication : IOperation
    {
        public int Operate(int left, int right)
        {
            return left * right * 10;
        }

        public int Version()
        {
            return 2;
        }

    }
}
