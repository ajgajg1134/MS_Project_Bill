using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace billc.TreeNodes
{
    class FieldAccess : Expression
    {
        public Expression classLiteral;
        public Identifier fieldName;

        public FieldAccess(Expression lhs, Identifier fname)
        {
            classLiteral = lhs;
            fieldName = fname;
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

        public override string ToString()
        {
            return classLiteral.ToString() + "." + fieldName;
        }
    }
}
