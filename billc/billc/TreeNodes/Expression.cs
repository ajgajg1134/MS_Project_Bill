using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace billc.TreeNodes
{
    abstract class Expression : Node
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
    }
}
