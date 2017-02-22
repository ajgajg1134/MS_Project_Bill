using System.Collections.Generic;
using System.Linq;

namespace billc.TreeNodes
{
    abstract class Statement : Node
    {
        public static string toString(List<Statement> stmts)
        {
            return stmts.Select(s => s.ToString()).Aggregate("", (a, b) => a + b + "\n");
        }
    }
}
