using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace billc.TreeNodes
{
    abstract class Node : Visitable
    {
        /// <summary>
        /// The line number where this node begins in the original source code
        /// -1 by default
        /// </summary>
        public int lineNum = -1;
        public abstract void accept(Visitor v);
    }
}
