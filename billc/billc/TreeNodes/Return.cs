using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace billc.TreeNodes
{
    class Return : Statement
    {
        Expression toRet;

        public Return(Expression ret)
        {
            toRet = ret;
        }
        public override void accept(Visitor v)
        {
            v.visit(this);
        }

        public override string ToString()
        {
            return "return " + toRet.ToString() + ";\n";
        }
    }
}
