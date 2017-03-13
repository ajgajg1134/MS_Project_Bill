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
        public bool isValidProgram = true;
        public string resultType;

        public TypeValidatorVisitor()
        {
            table = new SymbolTable();
        }

        public TypeValidatorVisitor(TypeValidatorVisitor tvv)
        {
            table = new SymbolTable(tvv.table);
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

        public void visit(Identifier id)
        {
            if (!table.isLocalVar(id.id))
            {
                isValidProgram = false;
                ErrorReporter.Error(id + " does not exist in current scope.", id);
            }
        }

        public void visit(FunctionInvocation fi)
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
            TypeValidatorVisitor fxnVisitor = new TypeValidatorVisitor(this);
            //Add params as available local vars
            fdecl.fParams.ForEach(fp => fxnVisitor.table.addLocalVar(fp.id, fp.type));

            //go through each statement to verify
            foreach(Statement s in fdecl.block)
            {
                s.accept(fxnVisitor);
            }

            if (!fxnVisitor.isValidProgram)
            {
                isValidProgram = false;
            }
            //todo: figure out how to check return type (maybe do a seperate special thing?)
        }

        public void visit(ProgramNode node)
        {
            //node.classes.ForEach(c => c.accept(this));
            node.functions.ForEach(f => f.accept(this));

            //Check for a main function
            FunctionDecl main = node.functions.FirstOrDefault(f => f.id == "main");
            //Todo: check params list and return type
            if (main == null)
            {
                isValidProgram = false;
                ErrorReporter.Error("No main function found");
            }
        }
    }
}
