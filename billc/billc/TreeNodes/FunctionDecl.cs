using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace billc.TreeNodes
{
    class FunctionDecl
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


    }
}
