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
        SymbolTable table;
        public TypeValidatorVisitor()
        {
            table = new SymbolTable();
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

        public void visit(WhileLoop wloop)
        {
            throw new NotImplementedException();
        }

        public void visit(Continue ct)
        {
            throw new NotImplementedException();
        }

        public void visit(Break br)
        {
            throw new NotImplementedException();
        }

        public void visit(Return ret)
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
            /* TODO Fix me
            if (!table.isLocalVar(astmt.id))
            {
                Console.Error.WriteLine("Error: " + astmt.id + " has not been declared.");
                return;
            }
            string rhsType = astmt.rhs.getResultTypeWithCheck(table);

            if (table.getLocalVar(astmt.id) != rhsType)
            {
                Console.Error.WriteLine("Error: " + astmt.id + " is of type " + table.getLocalVar(astmt.id) + " but RHS of expression is of type " + rhsType + ".");
            }
            */
        }

        public void visit(FunctionDecl fdecl)
        {
            //todo: Add formal params to symbol table (check if they're accurate too)
            //Keep a copy to replace the "bad" symbol table that has local vars
            
            //go through each statement to verify
            foreach(Statement s in fdecl.block)
            {
                s.accept(this);
            }
            //todo: figure out how to check return type (maybe do a seperate special thing?)
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
