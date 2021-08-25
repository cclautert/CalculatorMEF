using System.ComponentModel.Composition;
using static BasicOperations.CalculatorMefInterfaces;

namespace ExtendedOperations
{
    [Export(typeof(IOperation))]
    [ExportMetadata("Symbol", '%')]
    public partial class Modulo : IOperation
    {
        public int Operate(int left, int right)
        {
            return left % right;
        }
    }
}
