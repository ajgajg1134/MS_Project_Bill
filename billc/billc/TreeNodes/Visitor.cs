using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace billc.TreeNodes
{
    interface Visitor
    {
        //TODO: Add more concrete visit functions for every node type
        void visit(ProgramNode node);
    }
}
