using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace billc.TreeNodes
{
    class Continue : Statement
    {
        public Continue() { }

        public override void accept(Visitor v)
        {
            v.visit(this);
        }

        public override string ToString()
        {
            return "continue;\n";
        }
    }
}
