using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace billc.TreeNodes
{
    class Conditional : Statement
    {
        List<Statement> thenBlock;
        List<Statement> elseBlock;
        Expression condition;

        public Conditional(List<Statement> tBlock, List<Statement> eBlock, Expression cond)
        {
            thenBlock = tBlock;
            elseBlock = eBlock;
            condition = cond;
        }

        public override string ToString()
        {
            return "if (" + condition.ToString() + "){\n" +
                toString(thenBlock) + "} else {\n" + toString(elseBlock) + "}";
        }

        public override void accept(Visitor v)
        {
            v.visit(this);
        }
    }
}
