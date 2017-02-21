using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace billc.TreeNodes
{
    class ClassDecl : Node
    {
        List<FormalParam> fields;
        //List<FunctionDecl> methods; //To be implemented as possible future work

        public ClassDecl(List<FormalParam> f)
        {
            fields = f;
        }

        public override void accept(Visitor v)
        {
            v.visit(this);
        }
    }
}
