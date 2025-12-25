using Excel.Core.Implementation.Classes;
using Excel.Implementation.Classes.Functions;
using Excel.Tests.Mock;

namespace Excel.Tests.Utils
{
    public static class FormulaFactory
    {
        public static FormulaManager Create(TestTable table)
        {
            var funcs = new FunctionManager();
            funcs.Register(new SumFunction());
            funcs.Register(new AvgFunction());
            funcs.Register(new CountFunction());

            return new FormulaManager(table, funcs);
        }
    }
}