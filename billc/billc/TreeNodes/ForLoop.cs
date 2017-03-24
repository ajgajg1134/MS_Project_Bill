using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace billc.TreeNodes
{
    class ForLoop : Statement
    {
        public List<Statement> loopBody;
        public LocalVarDecl decl;
        public Expression condition;
        /// <summary>
        /// These statements always occur at the end of a loop (after loop body)
        /// </summary>
        public List<Statement> iteratedStmts;

        public ForLoop(List<Statement> lBody, LocalVarDecl dec, Expression cond, List<Statement> ist)
        {
            loopBody = lBody;
            decl = dec;
            condition = cond;
            iteratedStmts = ist;
        }

        public override void accept(Visitor v)
        {
            v.visit(this);
        }

        public override string ToString()
        {
            string s = "for(" + decl.ToString() + condition.ToString() + toString(iteratedStmts)
                + "){\n" + toString(loopBody) + "}\n";
            return s;
        }
    }
}
