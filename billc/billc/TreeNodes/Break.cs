using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace billc.TreeNodes
{
    class Break : Statement
    {
        public Break() { }

        public override void accept(Visitor v)
        {
            v.visit(this);
        }

        public override string ToString()
        {
            return "break;\n";
        }
    }
}
