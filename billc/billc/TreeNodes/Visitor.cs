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
        void visit(FunctionDecl fdecl);
        void visit(ClassDecl cdecl);
        void visit(Assignment astmt);
        void visit(BinaryOperator bop);
        void visit(Literal literal);
        void visit(FormalParam fparam);
        void visit(Expression exp);
        void visit(LocalVarDecl ldecl);
        void visit(Conditional cond);
        void visit(UnaryOperator unop);
        void visit(Return ret);
        void visit(WhileLoop wloop);
    }
}
