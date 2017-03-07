using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace billc.TreeNodes
{
    class LocalVarDecl : Statement
    {
        public string id;
        public string type;
        public Expression val;

        public LocalVarDecl(string t, string i, Expression v)
        {
            type = t;
            id = i;
            val = v;
        }

        /// <summary>
        /// When using this constructor the type of this declaration must be added later before code gen
        /// </summary>
        /// <param name="i"></param>
        /// <param name="v"></param>
        public LocalVarDecl(string i, Expression v)
        {
            id = i;
            val = v;
        }

        public void addType(string t)
        {
            type = t;
        }

        public override string ToString()
        {
            return id + " = " + val.ToString() + ";";
        }

        public override void accept(Visitor v)
        {
            v.visit(this);
        }
    }
}
