﻿using System;
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
        internal IErrorReporter errorReporter = new ErrorReporter();
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
            returnType = tvv.returnType;
            inLoop = tvv.inLoop;
            errorReporter = tvv.errorReporter;
        }

        public void visit(ClassDecl cdecl)
        {
            foreach (var fparam in cdecl.fields)
            {
                if (!PrimitiveTypes.isPrimitiveType(fparam.type) && fparam.type != "String" && SymbolTable.isClass(fparam.type))
                {
                    isValidProgram = false;
                    errorReporter.Error("Unknown type '" + fparam.type + "'", fparam);
                    return;
                }
            }
            //Add constructor as classname.new
            var fdecl = new FunctionDecl(cdecl.fields, new Identifier(cdecl.id.id + ".new"), cdecl.id.id, new List<Statement>());
            SymbolTable.addConstructor(fdecl);
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

            if (leftType == "String" || rightType == "String")
            {
                if (leftType != "String" || rightType != "String")
                {
                    //There are no valid binary operations with string and non-string
                    isValidProgram = false;
                    errorReporter.Error("Can not perform binary operation and String and non-String type.", bop);
                    return;
                }
            }
            resultType = BinaryOperator.getResultTypeFromOp(bop.op, leftType, rightType);
        }

        public void visit(FormalParam fparam)
        {
            if (doesTypeExist(fparam.type))
            {
                table.addLocalVar(fparam.id.id, fparam.type);
            }
            else
            {
                isValidProgram = false;
                errorReporter.Error("Unknown type '" + fparam.type + "'", fparam);
            }
        }

        public void visit(LocalVarDecl ldecl)
        {
            if (!doesTypeExist(ldecl.type))
            {
                isValidProgram = false;
                errorReporter.Error("Unknown type '" + ldecl.type + "'", ldecl);
                return;
            }
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

            if(table.isLocalVar(ldecl.id.id))
            {
                isValidProgram = false;
                errorReporter.Error("Variable '" + ldecl.id + "' has already been declared.", ldecl);
                return;
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
                //Check for setting a field on an object
                if (id.id.IsFieldIdentifier())
                {
                    string objectName = id.id.GetBeforeField();
                    if (!table.isLocalVar(objectName))
                    {
                        isValidProgram = false;
                        errorReporter.Error(objectName + " does not exist in current scope.", id);
                        return;
                    }
                    string objType = table.getLocalVar(objectName);
                    if (!SymbolTable.isClass(objType))
                    {
                        isValidProgram = false;
                        errorReporter.Error(objType + " is not a valid class, can not access field of a non-class.", id);
                        return;
                    }
                    string fieldName = id.id.GetField();
                    ClassDecl cd = SymbolTable.classes[objType];
                    bool hasField = cd.fields.Any(fp => fp.id.id.Equals(fieldName));
                    if (!hasField)
                    {
                        isValidProgram = false;
                        errorReporter.Error("Class '" + objType + "' has no field '" + fieldName + "'.", id);
                        return;
                    }
                    FormalParam fparam = cd.fields.Find(fp => fp.id.id.Equals(fieldName));
                    resultType = fparam.type;
                }
                else
                {
                    isValidProgram = false;
                    errorReporter.Error(id + " does not exist in current scope.", id);
                }
            }
            else
            {
                resultType = table.getLocalVar(id.id);
            }
        }

        public void visit(FunctionInvocation fi)
        {
            //Methods need to have the type they're acting on added to the FI name
            //This is so the interpreter can locate the correct function to call
            //Luckily whatever is in the first parameter slot holds what we're going to act on
            if (fi.isMethod)
            {
                TypeValidatorVisitor tvv = new TypeValidatorVisitor(this);
                if(fi.paramsIn.Count < 1)
                {
                    errorReporter.Fatal("Method missing mandatory first argument.");
                    isValidProgram = false;
                    return;
                }
                fi.paramsIn[0].accept(tvv);
                if (!tvv.isValidProgram)
                {
                    return;
                }
                fi.fxnId = new Identifier(tvv.resultType + "." + fi.fxnId);
            }
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
                    + string.Join(",", fakeParams.Select(f => f.ToString())) + "'.", fi);
                isValidProgram = false;
                return;
            }
            FunctionDecl fdecl = SymbolTable.getFunction(invokeDecl);
            fi.actualFunction = fdecl;
            resultType = fdecl.retType;
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
            string varType = resultType;
            astmt.rhs.accept(this);
            if (!isValidProgram)
            {
                return;
            }
            if(varType != resultType){
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
                fxnVisitor.checkStatementForNonVoid(s);
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
            foreach(var fdecl in node.functions)
            {
                if (SymbolTable.isFunction(fdecl))
                {
                    isValidProgram = false;
                    errorReporter.Error("A function with name '" + fdecl.id + "' already exists.", fdecl);
                    return;
                }
                SymbolTable.addFunction(fdecl);
            }
            
            //Add classes to the symbol table
            foreach(var cd in node.classes)
            {
                if (SymbolTable.isClass(cd))
                {
                    isValidProgram = false;
                    errorReporter.Error("A class with name'" + cd.id + "' already exists.", cd);
                    return;
                }
                SymbolTable.addClass(cd.id.id, cd);
            }

            node.classes.ForEach(c => c.accept(this));

            if (!isValidProgram)
            {
                return;
            }

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

        public void visit(ForLoop floop)
        {
            TypeValidatorVisitor tvv = new TypeValidatorVisitor(this);
            floop.decl.accept(tvv);
            if(!tvv.isValidProgram)
            {
                isValidProgram = false;
                return;
            }
            floop.condition.accept(tvv);
            if (!tvv.isValidProgram)
            {
                isValidProgram = false;
                return;
            }
            string condType = tvv.resultType;
            if (condType != "bool")
            {
                isValidProgram = false;
                errorReporter.Error("Condition in for loop must be of type boolean, detected type as " + condType + ".", floop);
            }
            tvv.inLoop = true;
            foreach(Statement stmt in floop.loopBody)
            {
                stmt.accept(tvv);
                if (!tvv.isValidProgram)
                {
                    isValidProgram = false;
                    return;
                }
            }

            foreach (Statement stmt in floop.iteratedStmts)
            {
                stmt.accept(tvv);
                if (!tvv.isValidProgram)
                {
                    isValidProgram = false;
                    return;
                }
            }
        }

        public void visit(IndexOperation indexOperation)
        {
            indexOperation.index.accept(this);
            if (!isValidProgram)
            {
                return;
            }
            if (resultType != "int")
            {
                errorReporter.Error("Array index must be of type int. Detected type: '" + resultType + "'.", indexOperation);
                isValidProgram = false;
                return;
            }

            indexOperation.id.accept(this);
            if (!isValidProgram)
            {
                return;
            }

            //Check result type if we can even index this
            if (resultType == "String")
            {
                resultType = "char";
            }
            else if (resultType.IsList())
            {
                resultType = resultType.GetListType();
            }
            else
            {
                errorReporter.Error("Array index operator can not be used on type '" + resultType + "'.", indexOperation);
                isValidProgram = false;
                return;
            }
        }

        public void checkStatementForNonVoid(Statement s)
        {
            if (s is FunctionInvocation)
            {
                var fi = s as FunctionInvocation;
                if (fi.actualFunction.retType != "void")
                {
                    errorReporter.Warning("Using a non-void function as an expression.", s);
                }
            }
        }

        public bool doesTypeExist(string s)
        {
            if (PrimitiveTypes.isPrimitiveType(s) || s == "String")
            {
                return true;
            }
            if (SymbolTable.isClass(s))
            {
                return true;
            }
            if (s.IsList())
            {
                return true;
            }
            return false;
        }

        public void visit(ListLiteral listLiteral)
        {
            foreach (var exp in listLiteral.list)
            {
                exp.accept(this);
                if (!isValidProgram)
                {
                    return;
                }
                if (resultType != listLiteral.internal_type)
                {
                    errorReporter.Error("Expression '" + exp.ToString() + "' does not match type of List '" + listLiteral.internal_type + "'.", listLiteral);
                    isValidProgram = false;
                    return;
                }
            }
            resultType = listLiteral.getResultType();
        }

        public void visit(ClassLiteral classLiteral)
        {
            throw new NotImplementedException();
        }

        public void visit(FieldAccess fieldAccess)
        {
            TypeValidatorVisitor tvv = new TypeValidatorVisitor(this);
            fieldAccess.classLiteral.accept(tvv);
            if(!tvv.isValidProgram)
            {
                isValidProgram = false;
                return;
            }
            if (!doesTypeExist(tvv.resultType))
            {
                isValidProgram = false;
                errorReporter.Error("Unknown type '" + fieldAccess.classLiteral + "'", fieldAccess);
                return;
            }

            if (!SymbolTable.classes.ContainsKey(tvv.resultType))
            {
                isValidProgram = false;
                errorReporter.Error("Type '" + tvv.resultType + "' is not a class.", fieldAccess);
                return;
            }

            ClassDecl cd = SymbolTable.classes[tvv.resultType];
            bool hasField = cd.fields.Any(fp => fp.id.Equals(fieldAccess.fieldName));
            if (!hasField)
            {
                isValidProgram = false;
                errorReporter.Error("Class '" + tvv.resultType + "' has no field '" + fieldAccess.fieldName + "'.", fieldAccess);
                return;
            }
            FormalParam fparam = cd.fields.Find(fp => fp.id.Equals(fieldAccess.fieldName));
            resultType = fparam.type;
        }
    }
}
