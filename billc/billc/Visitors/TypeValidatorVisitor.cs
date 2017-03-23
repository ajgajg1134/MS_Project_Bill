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
        /// <summary>
        /// Is set by this visitor to indicate what the result type of a visited node is.
        /// </summary>
        public string resultType = "";
        /// <summary>
        /// Holds the context of if at this point a return type exists where this visitor is.
        /// </summary>
        public string returnType = "";
        bool inLoop = false;
        

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
            resultType = BinaryOperator.getResultTypeFromOp(bop.op, leftType, rightType);
        }

        public void visit(FormalParam fparam)
        {
            table.addLocalVar(fparam.id.id, fparam.type);
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
                isValidProgram = false;
                errorReporter.Error("Declared type of " + ldecl.type + " does not match expression type of " + valType, ldecl);
            }

            table.addLocalVar(ldecl.id.id, valType);
            resultType = "";
        }

        public void visit(WhileLoop wloop)
        {
            wloop.conditional.accept(this);
            if (!isValidProgram)
            {
                return;
            }
            string condType = resultType;
            if (condType != "bool")
            {
                isValidProgram = false;
                errorReporter.Error("Condition in while loop must be of type boolean, detected type as " + condType + ".", wloop);
            }
            TypeValidatorVisitor tvvLoop = new TypeValidatorVisitor(this);
            tvvLoop.inLoop = true;
            foreach (Statement stmt in wloop.loopBody)
            {
                stmt.accept(tvvLoop);
                if (!tvvLoop.isValidProgram)
                {
                    return;
                }
            }
        }

        public void visit(Continue ct)
        {
            if (!inLoop)
            {
                isValidProgram = false;
                errorReporter.Error("Continue can only be used inside a loop.", ct);
            }
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
            if (!SymbolTable.isLocalFunction(fi.fxnId.id) && !SymbolTable.isBuiltinFunction(fi.fxnId.id))
            {
                isValidProgram = false;
                errorReporter.Error("Could not find function '" + fi.fxnId + "'.", fi);
                return;
            }
            List<FormalParam> fakeParams = new List<FormalParam>();
            foreach(Expression actualParam in fi.paramsIn)
            {
                var tvv = new TypeValidatorVisitor(this);
                actualParam.accept(tvv);
                if(!tvv.isValidProgram)
                {
                    isValidProgram = false;
                    return;
                }
                FormalParam fp = new FormalParam(new Identifier(""), tvv.resultType);
                fakeParams.Add(fp);
            }
            var invokeDecl = new FunctionDecl(fakeParams, fi.fxnId, "", new List<Statement>());
            if (!SymbolTable.isFunction(invokeDecl))
            {
                errorReporter.Error("Function '" + fi.fxnId + "' does not match provided parameter list types: '"
                    + fakeParams.Select(f => f.ToString()).Aggregate("", (a, b) => a + b + ", ") + "'.", fi);
                isValidProgram = false;
                return;
            }
            resultType = SymbolTable.getFunction(invokeDecl).retType;
        }

        public void visit(Break br)
        {
            if (!inLoop)
            {
                isValidProgram = false;
                errorReporter.Error("Break can only be used inside a loop.", br);
            }
        }

        public void visit(Return ret)
        {
            if (returnType == "" || returnType == "void")
            {
                if (ret.toRet != null)
                {
                    isValidProgram = false;
                    errorReporter.Error("Can not return value in void function.", ret);
                    return;
                }
                else
                {
                    return;
                }
            }
            ret.toRet.accept(this);
            if (!isValidProgram)
            {
                return;
            }
            if (returnType != resultType)
            {
                isValidProgram = false;
                errorReporter.Error("Return type " + resultType + " does not match return type of function: " + returnType + ".", ret);
            }
        }

        public void visit(UnaryOperator unop)
        {
            unop.inner.accept(this);
            if (!isValidProgram)
            {
                return;
            }
            string innerType = resultType;
            if (!UnaryOperator.isValidTypeWithOp(innerType, unop.unop))
            {
                isValidProgram = false;
                errorReporter.Error(innerType + " not valid with operator '" + UnaryOperator.unopToString(unop.unop) + "'", unop);
                return;
            }
            resultType = UnaryOperator.getResultTypeFromOp(unop.unop, innerType);
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
                    isValidProgram = false;
                    return;
                }
            }
            TypeValidatorVisitor tvvElse = new TypeValidatorVisitor(this);
            foreach (Statement stmt in cond.elseBlock)
            {
                stmt.accept(tvvElse);
                if (!tvvElse.isValidProgram)
                {
                    isValidProgram = false;
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
            astmt.id.accept(this);
            if (!isValidProgram)
            {
                return;
            }
            astmt.rhs.accept(this);
            if (!isValidProgram)
            {
                return;
            }
            if(table.getLocalVar(astmt.id.id) != resultType){
                isValidProgram = false;
                errorReporter.Error("Variable '" + astmt.id + "' is of type " + table.getLocalVar(astmt.id.id) + ", but expression is of type " + resultType, astmt);
            }
        }

        public void visit(FunctionDecl fdecl)
        {
            //Create a copy so we don't pollute the program level TypeValidatorVisitor
            TypeValidatorVisitor fxnVisitor = new TypeValidatorVisitor(this);
            //Add params as available local vars
            fdecl.fParams.ForEach(fp => fp.accept(fxnVisitor));

            fxnVisitor.returnType = fdecl.retType;

            //go through each statement to verify
            foreach(Statement s in fdecl.block)
            {
                s.accept(fxnVisitor);
                if (!fxnVisitor.isValidProgram)
                {
                    isValidProgram = false;
                    return;
                }
            }

            if (!fxnVisitor.isValidProgram)
            {
                isValidProgram = false;
            }
            //TODO: check for return statement in non-void functions
        }

        public void visit(ProgramNode node)
        {
            //Copy function decls to local visitor
            node.functions.ForEach(f => SymbolTable.addFunction(f));

            //node.classes.ForEach(c => c.accept(this));

            //Don't worry about this visitor getting polluted by other functions, 
            //Visiting a function decl will create a copy and use that to check the function 
            node.functions.ForEach(f => f.accept(this));

            //Check for a main function
            FunctionDecl main = node.functions.FirstOrDefault(f => f.id.id == "main");
            if (main == null)
            {
                isValidProgram = false;
                errorReporter.Error("No main function found");
            }
        }
    }
}
