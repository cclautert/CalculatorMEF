using System;
using System.ComponentModel.Composition;
using static BasicOperations.CalculatorMefInterfaces;

namespace MultiplicationOperation
{
    [Export(typeof(IOperation))]
    [ExportMetadata("Operador", '*')]
    public class Multiplication : IOperation
    {
        public int Operate(int left, int right)
        {
            return left * right;
        }

        public int Version()
        {
            return 1;
        }
    }
}
