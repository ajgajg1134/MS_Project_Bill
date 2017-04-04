using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace billc.TreeNodes
{
    class IndexOperation : Expression
    {
        //An index operation can be performed on the result of a function call that returns a string
        //That's why this needs to be an expression and not just an Identifier
        public Expression id; 
        public Expression index;

        public IndexOperation(Expression i, Expression ix)
        {
            id = i;
            index = ix;
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
            return id.ToString() + "[" + index.ToString() + "];";
        }
    }
}
