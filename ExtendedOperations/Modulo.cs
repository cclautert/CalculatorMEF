using System.ComponentModel.Composition;
using static BasicOperations.CalculatorMefInterfaces;

namespace ExtendedOperations
{
    [Export(typeof(IOperation))]
    [ExportMetadata("Operador", '%')]
    public partial class Modulo : IOperation
    {
        public int Operate(int left, int right)
        {
            return left % right;
        }

        public int Version()
        {
            return 1;
        }
    }
}
