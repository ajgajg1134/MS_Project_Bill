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
        IErrorReporter errorReporter = new ErrorReporter();
        public bool isValidProgram = true;
        public string resultType = "";

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
            bop.left.accept(this);
            if (!isValidProgram)
            {
                return;
            }
            string leftType = resultType;
            if (string.IsNullOrEmpty(leftType))
            {
                isValidProgram = false;
                errorReporter.Error("Unable to determine result type of Binary LHS", bop.left);
                return;
            }

            bop.right.accept(this);
            if (!isValidProgram)
            {
                return;
            }
            string rightType = resultType;
            if (string.IsNullOrEmpty(rightType))
            {
                isValidProgram = false;
                errorReporter.Error("Unable to determine result type of Binary RHS", bop.right);
                return;
            }

            if (!BinaryOperator.isValidTypeWithOp(bop.op, leftType))
            {
                isValidProgram = false;
                errorReporter.Error(leftType + " on left hand side not valid with operator '" + BinaryOperator.binopToString(bop.op) + "'", bop);
                return;
            }
            if (!BinaryOperator.isValidTypeWithOp(bop.op, rightType))
            {
                isValidProgram = false;
                errorReporter.Error(rightType + " on right hand side not valid with operator '" + BinaryOperator.binopToString(bop.op) + "'", bop);
                return;
            }
            resultType = bop.getResultType();
        }

        public void visit(FormalParam fparam)
        {
            throw new NotImplementedException();
        }

        public void visit(LocalVarDecl ldecl)
        {
            ldecl.val.accept(this);
            if (!isValidProgram)
            {
                return;
            }
            string valType = resultType;

            if (ldecl.type != valType)
            {
                errorReporter.Error("Declared type of " + ldecl.type + " does not match expression type of " + valType, ldecl);
            }

            table.addLocalVar(ldecl.id.id, valType);
            resultType = "";
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
                errorReporter.Error(id + " does not exist in current scope.", id);
            }
            else
            {
                resultType = table.getLocalVar(id.id);
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
            cond.condition.accept(this);
            if (!isValidProgram)
            {
                return;
            }
            string condType = resultType;
            if (condType != "bool")
            {
                isValidProgram = false;
                errorReporter.Error("Condition in if statement must be of type boolean, detected type as " + condType + ".", cond);
            }
            TypeValidatorVisitor tvvThen = new TypeValidatorVisitor(this);
            foreach (Statement stmt in cond.thenBlock)
            {
                stmt.accept(tvvThen);
                if (!tvvThen.isValidProgram)
                {
                    return;
                }
            }
            TypeValidatorVisitor tvvElse = new TypeValidatorVisitor(this);
            foreach (Statement stmt in cond.elseBlock)
            {
                stmt.accept(tvvElse);
                if (!tvvElse.isValidProgram)
                {
                    return;
                }
            }
        }

        public void visit(Literal literal)
        {
            resultType = literal.getResultType();
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
            FunctionDecl main = node.functions.FirstOrDefault(f => f.id.id == "main");
            //Todo: check params list and return type
            if (main == null)
            {
                isValidProgram = false;
                errorReporter.Error("No main function found");
            }
        }
    }
}
