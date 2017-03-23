using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace billc.TreeNodes
{
    //Okay so actually expressions aren't valid statements always.
    //But certain things kinda are, like function calls and assignment
    //Rule <StatementExp> in the grammar defines these.
    //Because these are limited at the grammar level it's _okay_ that we extend Statement here
    //Even though not all expressions are truly valid statements.  
    abstract class Expression : Statement
    {
        public abstract string getResultType();

        /// <summary>
        /// Gets the result type of an expression while verifying if this expression
        /// (And sub expressions) pass a type check.
        /// If an error is found a message is printed to standard error and the string
        /// "ERROR" is returned in place of a normal type
        /// </summary>
        /// <param name="table">The symbol table to lookup valid types in</param>
        /// <returns>the result type of this expression, or "ERROR"</returns>
        public abstract string getResultTypeWithCheck(SymbolTable table);

        public static string toString(List<Expression> stmts)
        {
            return stmts.Select(s => s.ToString()).Aggregate("", (a, b) => a + b + ",");
        }
    }
}
