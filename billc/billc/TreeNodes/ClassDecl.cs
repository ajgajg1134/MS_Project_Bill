using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace billc.TreeNodes
{
    class ClassDecl : Node
    {
        public Identifier id;
        List<FormalParam> fields;
        //List<FunctionDecl> methods; //To be implemented as possible future work

        public ClassDecl(Identifier id, List<FormalParam> f)
        {
            this.id = id;
            fields = f;
        }

        public override void accept(Visitor v)
        {
            v.visit(this);
        }
    }
}
