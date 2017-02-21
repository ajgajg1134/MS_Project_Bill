using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace billc.TreeNodes
{
    class LocalVarDecl : Statement
    {
        string id;
        Expression val;

        public LocalVarDecl(string i, Expression v)
        {
            id = i;
            val = v;
        }

        public override string ToString()
        {
            return id + " = " + val.ToString();
        }

        public override void accept(Visitor v)
        {
            v.visit(this);
        }
    }
}
