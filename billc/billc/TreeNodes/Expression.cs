using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace billc.TreeNodes
{
    class Expression : Node
    {

        public override void accept(Visitor v)
        {
            v.visit(this);
        }
    }
}
