using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace billc.TreeNodes
{
    class ListLiteral : Expression
    {
        public List<Expression> list;
        public string internal_type;

        public ListLiteral(List<Expression> lst, string inType)
        {
            list = lst;
            internal_type = inType;
        }

        public override string getResultType()
        {
            return "List<" + internal_type + ">";
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
            //Todo: fix me
            return "{ " + string.Join(",", list.Select(f => f.ToString()).Aggregate("", (a, b) => a + b)) + "}";
        }
    }
}
