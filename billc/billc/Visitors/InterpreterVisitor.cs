using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using billc.TreeNodes;
using System.Text.RegularExpressions;

namespace billc.Visitors
{
    class BillRuntimeException : Exception
    {

    }

    public delegate void Println(string s);
    public delegate string Input();

    /// <summary>
    /// A visitor that will interpret the code of the part of the tree it visits
    /// Currently much of this visitor assumes that the AST is completely type-checked and valid
    /// </summary>
    class InterpreterVisitor : Visitor
    {
        /// <summary>
        /// name of variable to the value it currently holds
        /// </summary>
        internal Dictionary<string, Literal> primitive_vars = new Dictionary<string, Literal>();
        /// <summary>
        /// Maps name of list variable to the actual list object
        /// </summary>
        internal Dictionary<string, List<Expression>> lists = new Dictionary<string, List<Expression>>();
        /// <summary>
        /// Maps Identifiers to their type
        /// </summary>
        internal Dictionary<Identifier, string> id_to_type = new Dictionary<Identifier, string>();

        internal IErrorReporter errorReporter = new ErrorReporter();

        internal Println println;
        internal Input input;

        internal static int stack_counter = 0;
        internal const int MAX_STACK = 1000;

        Literal result = null;
        object result_ref = null;
        
        bool leaveFxn = false;
        bool shouldContinue = false;
        bool leaveLoop = false;
        bool wasReferenceResult = false;

        public InterpreterVisitor()
        {
            println = Console.WriteLine;
            input = Console.ReadLine;
        }

        public InterpreterVisitor(Println printer, Input inp)
        {
            println = printer;
            input = inp;
        }
        public InterpreterVisitor(InterpreterVisitor iv)
        {
            primitive_vars = new Dictionary<string, Literal>(iv.primitive_vars);
            lists = new Dictionary<string, List<Expression>>(iv.lists);
            id_to_type = new Dictionary<Identifier, string>(iv.id_to_type);
            errorReporter = iv.errorReporter;
            println = iv.println;
            input = iv.input;
        }

        public void visit(ClassDecl cdecl)
        {
            //TODO
            throw new NotImplementedException();
        }

        public void visit(BinaryOperator bop)
        {
            InterpreterVisitor iv_left = new InterpreterVisitor(this);
            InterpreterVisitor iv_right = new InterpreterVisitor(this);
            bop.left.accept(iv_left);
            bop.right.accept(iv_right);
            try
            {
                result = Literal.performBinOp(iv_left.result, iv_right.result, bop.op);
            } catch (OverflowException)
            {
                errorReporter.Error("Integer overflow occurred, number too big or too small.", bop);
                throw new BillRuntimeException();
            }
        }

        public void visit(FormalParam fparam)
        {
            throw new NotImplementedException();
        }

        public void visit(LocalVarDecl ldecl)
        {
            InterpreterVisitor sub_exp = new InterpreterVisitor(this);
            ldecl.val.accept(sub_exp);
            if (!sub_exp.wasReferenceResult)
            {
                primitive_vars.Add(ldecl.id.id, sub_exp.result);
            }
            else
            {
                //For now just handle lists
                if (!(sub_exp.result_ref is List<Expression>))
                {
                    errorReporter.Fatal("Expected reference type in local var decl but was not List<Expression>!");
                    throw new BillRuntimeException();
                }
                lists.Add(ldecl.id.id, sub_exp.result_ref as List<Expression>);
                id_to_type.Add(ldecl.id, ldecl.type);
            }
        }

        public void visit(UnaryOperator unop)
        {
            InterpreterVisitor iv_inner = new InterpreterVisitor(this);
            unop.inner.accept(iv_inner);
            result = iv_inner.result.performUnop(unop.unop);
            if (result == null)
            {
                throw new BillRuntimeException();
            }
        }

        public void visit(Identifier id)
        {
            if (primitive_vars.ContainsKey(id.id))
            {
                result = primitive_vars[id.id];
            } else if (lists.ContainsKey(id.id))
            {
                wasReferenceResult = true;
                result_ref = lists[id.id];
            } else
            {
                throw new NotImplementedException();
            }
        }

        public void visit(FunctionInvocation fi)
        {
            stack_counter++;
            if(stack_counter > MAX_STACK)
            {
                errorReporter.Error("Stack overflow occurred! Check you aren't infinitely looping or recursing.", fi);
                throw new BillRuntimeException();
            }
            if (SymbolTable.isLocalFunction(fi.fxnId.id))
            {
                //User defined function
                InterpreterVisitor param_iv = new InterpreterVisitor(println, input);
                for(int i = 0; i < fi.paramsIn.Count; i++)
                {
                    result = null;
                    fi.paramsIn[i].accept(this);
                    if (result == null)
                    {
                        errorReporter.Fatal("Interpreter encountered null literal in function invocation.");
                        throw new BillRuntimeException();
                    }
                    param_iv.primitive_vars[fi.actualFunction.fParams[i].id.id] = result;
                }
                param_iv.result = null;
                foreach (Statement s in fi.actualFunction.block)
                {
                    s.accept(param_iv);
                    if (param_iv.leaveFxn)
                    {
                        leaveFxn = true;
                        result = param_iv.result;
                        stack_counter--;
                        return;
                    }
                }
                result = param_iv.result;

            }
            else if (SymbolTable.isBuiltinFunction(fi.fxnId.id))
            {
                //Built-in
                switch (fi.fxnId.id)
                {
                    case "println":
                        InterpreterVisitor paramiv = new InterpreterVisitor(this);
                        fi.paramsIn[0].accept(paramiv);
                        println(paramiv.result.s);
                        stack_counter--;
                        return;
                    case "toStr":
                        InterpreterVisitor paramivstr = new InterpreterVisitor(this);
                        fi.paramsIn[0].accept(paramivstr);
                        result = new Literal(paramivstr.result.ToString());
                        stack_counter--;
                        return;
                    case "input":
                        result = new Literal(input());
                        stack_counter--;
                        return;
                    case "toInt":
                        InterpreterVisitor paramivint = new InterpreterVisitor(this);
                        fi.paramsIn[0].accept(paramivint);
                        try
                        {
                            result = new Literal(int.Parse(paramivint.result.s));
                        } catch (FormatException)
                        {
                            errorReporter.Error("Unable to convert string '" + paramivint.result.s + "' to integer.", fi);
                            stack_counter--;
                            throw new BillRuntimeException();
                        }
                        stack_counter--;
                        return;
                    case "toDouble":
                        InterpreterVisitor paramivdbl = new InterpreterVisitor(this);
                        fi.paramsIn[0].accept(paramivdbl);
                        result = new Literal(double.Parse(paramivdbl.result.s));
                        stack_counter--;
                        return;
                    case "length":
                        InterpreterVisitor paramivlen = new InterpreterVisitor(this);
                        fi.paramsIn[0].accept(paramivlen);
                        result = new Literal(paramivlen.result.s.Length);
                        stack_counter--;
                        return;
                    default:
                        if (SymbolTable.isConstructor(fi.fxnId.id))
                        {
                            handleConstructors(fi);
                            return;
                        }
                        if (fi.isMethod)
                        {
                            //Check for a List<type>.size() function
                            const string listsizereg = @"(List<)\w+(>\.size)";
                            Regex regex = new Regex(listsizereg);
                            if (regex.IsMatch(fi.fxnId.id))
                            {
                                InterpreterVisitor listsize = new InterpreterVisitor(this);
                                fi.paramsIn[0].accept(listsize);
                                result = new Literal((listsize.result_ref as List<Expression>).Count);
                                stack_counter--;
                                return;
                            }

                            //Check for a List<type>.add() function
                            const string listaddreg = @"(List<)\w+(>\.add)";
                            Regex addreg = new Regex(listaddreg);
                            if (addreg.IsMatch(fi.fxnId.id))
                            {
                                InterpreterVisitor iv = new InterpreterVisitor(this);
                                fi.paramsIn[0].accept(iv);
                                List<Expression> list = iv.result_ref as List<Expression>;
                                iv.wasReferenceResult = false;
                                fi.paramsIn[1].accept(iv);
                                if (iv.wasReferenceResult)
                                {
                                    list.Add(iv.result_ref as Expression);
                                }
                                else
                                {
                                    list.Add(iv.result);
                                }
                                result = new Literal();
                                stack_counter--;
                                return;
                            }

                        }
                        errorReporter.Fatal("Interpreter encountered builtin-function in symbol table but without known implementation.");
                        throw new NotImplementedException();
                }
            }
            else
            {
                //Error!
                errorReporter.Fatal("Unknown function call: " + fi.fxnId + " should have been caught by typechecker");
                throw new BillRuntimeException();
            }
            stack_counter--;
        }

        public void handleConstructors(FunctionInvocation fi)
        {
            if (fi.fxnId.id.IsList())
            {
                //string listType = string.Concat(fi.fxnId.id.Substring(5).TakeWhile(c => c != '>'));
                result_ref = new List<Expression>();
                wasReferenceResult = true;
            }
        }

        public void visit(WhileLoop wloop)
        {
            InterpreterVisitor iv = new InterpreterVisitor(this);
            wloop.conditional.accept(iv);
            if (iv.result.type != lit_type.boolean)
            {
                errorReporter.Fatal("Interpreter found non boolean type in while loop. This should have failed in type check.");
            }
            while (iv.result.b)
            {
                foreach(Statement s in wloop.loopBody)
                {
                    s.accept(iv);
                    if (iv.shouldContinue || iv.leaveLoop || iv.leaveFxn)
                    {
                        break;
                    }
                }
                if (iv.leaveLoop || iv.leaveFxn)
                {
                    break;
                }
                //Re-check conditional
                wloop.conditional.accept(iv);
            }
            copyStateChanges(iv);
        }

        public void visit(Continue ct)
        {
            shouldContinue = true;
            result = null;
        }

        public void visit(Break br)
        {
            leaveLoop = true;
            result = null;
        }

        public void visit(Return ret)
        {
            ret.toRet.accept(this);
            leaveFxn = true;
        }

        public void visit(Conditional cond)
        {
            cond.condition.accept(this);
            if (result.type != lit_type.boolean)
            {
                errorReporter.Fatal("Interpreter found non boolean type in conditional. This should have failed in type check.");
            }
            InterpreterVisitor iv;
            if (result.b)
            {
                iv = new InterpreterVisitor(this);
                foreach(Statement s in cond.thenBlock)
                {
                    s.accept(iv);
                    if (iv.leaveFxn)
                    {
                        leaveFxn = true;
                        result = iv.result;
                        return;
                    }
                }
            }
            else
            {
                iv = new InterpreterVisitor(this);
                foreach (Statement s in cond.elseBlock)
                {
                    s.accept(iv);
                    if (iv.leaveFxn)
                    {
                        leaveFxn = true;
                        result = iv.result;
                        return;
                    }
                }
            }
            result = null;
            copyStateChanges(iv);
        }

        public void visit(Literal literal)
        {
            result = literal;
        }

        public void visit(Assignment astmt)
        {
            astmt.rhs.accept(this);
            primitive_vars[astmt.id.id] = result;
        }

        public void visit(FunctionDecl fdecl)
        {
            errorReporter.Fatal("Interpreter attempted to interpret a function decl.");
            throw new NotImplementedException();
        }

        public void visit(ProgramNode node)
        {
            FunctionDecl main = node.functions.FirstOrDefault(f => f.id.id == "main");

            if (main == null)
            {
                Console.Error.WriteLine("Error! main function not found");
                return;
            }
            //TODO: add command line args
            try
            {
                foreach (Statement s in main.block)
                {
                    s.accept(this);
                    if (leaveFxn)
                    {
                        return;
                    }
                }
            } catch (BillRuntimeException)
            {
                errorReporter.Error("Runtime exception occurred. Exiting.");
            }
        }

        public void visit(ForLoop floop)
        {
            InterpreterVisitor iv = new InterpreterVisitor(this);
            floop.decl.accept(iv);

            floop.condition.accept(iv);
            if (iv.result.type != lit_type.boolean)
            {
                errorReporter.Fatal("Interpreter found non boolean type in for loop. This should have failed in type check.");
            }
            while (iv.result.b)
            {
                foreach (Statement s in floop.loopBody)
                {
                    s.accept(iv);
                    if (iv.shouldContinue || iv.leaveLoop || iv.leaveFxn)
                    {
                        break;
                    }
                }
                if (iv.leaveLoop || iv.leaveFxn)
                {
                    break;
                }

                foreach (Statement s in floop.iteratedStmts)
                {
                    s.accept(iv);
                    if (iv.shouldContinue || iv.leaveLoop || iv.leaveFxn)
                    {
                        break;
                    }
                }
                if (iv.leaveLoop || iv.leaveFxn)
                {
                    break;
                }
                //Re-check conditional
                floop.condition.accept(iv);
            }
            leaveFxn = iv.leaveFxn;
            result = iv.result;
            copyStateChanges(iv);
        }

        /// <summary>
        /// Take an interpreter visitor that has finished executing at lower scope
        /// copy all changes in state for variables that existed at higher scope up
        /// </summary>
        /// <param name="lowerScope">the interpreter visitor from lower scope</param>
        public void copyStateChanges(InterpreterVisitor lowerScope)
        {
            foreach (var kv in lowerScope.primitive_vars)
            {
                if (primitive_vars.ContainsKey(kv.Key))
                {
                    primitive_vars[kv.Key] = kv.Value;
                }
            }
        }

        public void visit(IndexOperation indexOperation)
        {
            indexOperation.index.accept(this);
            int index = result.i;

            indexOperation.id.accept(this);
            if (wasReferenceResult)
            {
                //Just lists for now
                List<Expression> list = result_ref as List<Expression>;
                InterpreterVisitor iv = new InterpreterVisitor(this);
                list[index].accept(iv);
                if (iv.wasReferenceResult)
                {
                    wasReferenceResult = true;
                    result_ref = iv.result_ref;
                }
                else
                {
                    result = iv.result;
                }
            }
            else
            {
                Literal toIndex = result;
                result = new Literal(toIndex.s[index]);
            }            
        }

        public void visit(ListLiteral listLiteral)
        {
            wasReferenceResult = true;
            result_ref = listLiteral.list;
        }
    }
}
