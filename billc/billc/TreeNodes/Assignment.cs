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
        public Identifier id;
        public Expression rhs;

        public Assignment(Identifier i, Expression r)
        {
            id = i;
            rhs = r;
        }

        public override string ToString()
        {
            return id.ToString() + " = " + rhs.ToString() + ";";
        }

        public override void accept(Visitor v)
        {
            v.visit(this);
        }
    }
}
