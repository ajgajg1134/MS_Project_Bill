using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using billc.TreeNodes;

namespace billc.Visitors
{
    class BillRuntimeException : Exception
    {

    } 

    /// <summary>
    /// A visitor that will interpret the code of the part of the tree it visits
    /// Currently much of this visitor assumes that the AST is completely type-checked and valid
    /// </summary>
    class InterpreterVisitor : Visitor
    {
        /// <summary>
        /// name of variable to the value it currently holds
        /// </summary>
        Dictionary<string, Literal> primitive_vars = new Dictionary<string, Literal>();

        internal IErrorReporter errorReporter = new ErrorReporter();

        Literal result = null;
        bool leaveFxn = false;
        bool shouldContinue = false;
        bool leaveLoop = false;

        public InterpreterVisitor()
        {

        }
        public InterpreterVisitor(InterpreterVisitor iv)
        {
            primitive_vars = new Dictionary<string, Literal>(iv.primitive_vars);
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
            result = Literal.performBinOp(iv_left.result, iv_right.result, bop.op);
        }

        public void visit(FormalParam fparam)
        {
            throw new NotImplementedException();
        }

        public void visit(LocalVarDecl ldecl)
        {
            InterpreterVisitor sub_exp = new InterpreterVisitor(this);
            ldecl.val.accept(sub_exp);
            primitive_vars.Add(ldecl.id.id, sub_exp.result);
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
            result = primitive_vars[id.id];
        }

        public void visit(FunctionInvocation fi)
        {
            if (SymbolTable.isLocalFunction(fi.fxnId.id))
            {
                //User defined function
                InterpreterVisitor param_iv = new InterpreterVisitor(this);
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
                        Console.Out.WriteLine(paramiv.result.s);
                        return;
                    case "toStr":
                        InterpreterVisitor paramivstr = new InterpreterVisitor(this);
                        fi.paramsIn[0].accept(paramivstr);
                        result = new Literal(paramivstr.result.ToString());
                        return;
                    case "input":
                        result = new Literal(Console.ReadLine());
                        return;
                    case "toInt":
                        InterpreterVisitor paramivint = new InterpreterVisitor(this);
                        fi.paramsIn[0].accept(paramivint);
                        result = new Literal(int.Parse(paramivint.result.s));
                        return;
                    case "toDouble":
                        InterpreterVisitor paramivdbl = new InterpreterVisitor(this);
                        fi.paramsIn[0].accept(paramivdbl);
                        result = new Literal(double.Parse(paramivdbl.result.s));
                        return;
                    default:
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
            if (result.b)
            {
                InterpreterVisitor iv = new InterpreterVisitor(this);
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
                InterpreterVisitor iv = new InterpreterVisitor(this);
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
        }
    }
}
