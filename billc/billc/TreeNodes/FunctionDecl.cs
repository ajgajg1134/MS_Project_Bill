using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace billc.TreeNodes
{
    class FunctionDecl : Node
    {
        List<FormalParam> fParams;
        string id;
        string retType;
        List<Statement> block;

        public FunctionDecl(List<FormalParam> fps, string i, string rt, List<Statement> b)
        {
            fParams = fps;
            id = i;
            retType = rt;
            block = b;
        }

        public override string ToString()
        {
            return retType + " " + id + " (" + fParams.Select(f => f.ToString()).Aggregate("", (a, b) => a +  ", " + b) + ") {\n" +
                Statement.toString(block) + "}\n";
        }

        public override void accept(Visitor v)
        {
            v.visit(this);
        }
    }
}
