using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace billc.TreeNodes
{
    class Assignment : Statement
    {
        string id;
        Expression rhs;

        //todo: add other kinds of assignment

        public Assignment(string i, Expression r)
        {
            id = i;
            rhs = r;
        }

        public override string ToString()
        {
            return id + " = " + rhs.ToString() + "\n";
        }

        public override void accept(Visitor v)
        {
            v.visit(this);
        }
    }
}
