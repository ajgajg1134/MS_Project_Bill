using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace billc.TreeNodes
{
    class IndexOperation : Expression
    {
        public Identifier id;
        public Expression index;

        public IndexOperation(Identifier i, Expression ix)
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
