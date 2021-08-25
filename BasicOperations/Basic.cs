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
        }

        public partial interface IOperationData
        {
            char Symbol { get; }
        }

        [Export(typeof(IOperation))]
        [ExportMetadata("Symbol", '+')]
        public partial class Add : IOperation
        {
            public int Operate(int left, int right)
            {
                return left + right;
            }
        }

        [Export(typeof(IOperation))]
        [ExportMetadata("Symbol", '-')]
        public partial class Subtract : IOperation
        {
            public int Operate(int left, int right)
            {
                return left - right;
            }
        }
    }
}
