using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace billc.TreeNodes
{
    class FormalParam : Node
    {
        string id;
        string type;

        public FormalParam(string i, string t)
        {
            id = i;
            type = t;
        }

        public override void accept(Visitor v)
        {
            v.visit(this);
        }
    }
}
