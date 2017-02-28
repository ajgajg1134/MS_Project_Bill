using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace billc.TreeNodes
{
    class WhileLoop : Statement
    {
        List<Statement> loopBody;
        Expression conditional;

        public WhileLoop(List<Statement> body, Expression cond)
        {
            conditional = cond;
            loopBody = body;
        }

        public override void accept(Visitor v)
        {
            v.visit(this);
        }

        public override string ToString()
        {
            string s = "while(" + conditional.ToString() + "){\n" + toString(loopBody) + "}\n";
            return s;
        }
    }
}
