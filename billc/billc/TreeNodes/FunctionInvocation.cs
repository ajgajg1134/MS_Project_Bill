using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace billc.TreeNodes
{
    class FunctionInvocation : Expression
    {
        public Identifier fxnId;
        public List<Expression> paramsIn;
        /// <summary>
        /// This is a reference to the function to call
        /// Set by the typechecker
        /// </summary>
        public FunctionDecl actualFunction;

        public FunctionInvocation(Identifier id, List<Expression> paramsIn)
        {
            fxnId = id;
            this.paramsIn = paramsIn;
        }

        public override void accept(Visitor v)
        {
            v.visit(this);
        }

        public override string getResultType()
        {
            throw new NotImplementedException();
        }

        public override string getResultTypeWithCheck(SymbolTable table)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return fxnId.ToString() + "(" + string.Join(",", paramsIn.Select(f => f.ToString()).Aggregate("", (a, b) => a + b)) + ")";
        }
    }
}
