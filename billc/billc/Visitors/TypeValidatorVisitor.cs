using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using billc.TreeNodes;

namespace billc.Visitors
{
    class TypeValidatorVisitor : Visitor
    {

        public TypeValidatorVisitor()
        {
            //todo: need symbol table?
        }

        public void visit(ClassDecl cdecl)
        {
            throw new NotImplementedException();
        }

        public void visit(BinaryOperator bop)
        {
            throw new NotImplementedException();
        }

        public void visit(FormalParam fparam)
        {
            throw new NotImplementedException();
        }

        public void visit(LocalVarDecl ldecl)
        {
            throw new NotImplementedException();
        }

        public void visit(UnaryOperator unop)
        {
            throw new NotImplementedException();
        }

        public void visit(Conditional cond)
        {
            throw new NotImplementedException();
        }

        public void visit(Expression exp)
        {
            throw new NotImplementedException();
        }

        public void visit(Literal literal)
        {
            throw new NotImplementedException();
        }

        public void visit(Assignment astmt)
        {
            throw new NotImplementedException();
        }

        public void visit(FunctionDecl fdecl)
        {
            
        }

        public void visit(ProgramNode node)
        {
            node.functions.ForEach(f => f.accept(this));
            //node.classes.ForEach(c => c.accept(this));

            //Check for a main function
            FunctionDecl main = node.functions.FirstOrDefault(f => f.id == "main");
            //Todo: check params list and return type
            if (main == null)
            {
                Console.Error.WriteLine("Error no main function identified");
            }
        }
    }
}
