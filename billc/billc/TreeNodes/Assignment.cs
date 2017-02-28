using billc.Visitors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace billc.TreeNodes
{
    class Assignment : Statement
    {
        public Expression lhs;
        public Expression rhs;

        public Assignment(Expression l, Expression r)
        {
            lhs = l;
            rhs = r;
        }

        public override string ToString()
        {
            return lhs.ToString() + " = " + rhs.ToString() + ";";
        }

        public override void accept(Visitor v)
        {
            v.visit(this);
        }
    }
}
