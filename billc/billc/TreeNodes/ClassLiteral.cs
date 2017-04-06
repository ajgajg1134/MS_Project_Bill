using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace billc.TreeNodes
{
    class ClassLiteral : Expression
    {
        public Identifier type;
        public List<Expression> fieldValues = new List<Expression>();

        public ClassLiteral(Identifier type, List<Expression> values)
        {
            this.type = type;
            fieldValues = values;
        }

        public override string getResultType()
        {
            throw new NotImplementedException();
        }

        public override string getResultTypeWithCheck(SymbolTable table)
        {
            throw new NotImplementedException();
        }

        public override void accept(Visitor v)
        {
            v.visit(this);
        }
    }
}
