using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using billc.TreeNodes;

namespace billc.Visitors
{
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

        Literal result = null;
        public InterpreterVisitor()
        {

        }
        public InterpreterVisitor(InterpreterVisitor iv)
        {
            primitive_vars = new Dictionary<string, Literal>(iv.primitive_vars);
        }

        public void visit(ClassDecl cdecl)
        {
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
            throw new NotImplementedException();
        }

        public void visit(Identifier id)
        {
            result = primitive_vars[id.id];
        }

        public void visit(FunctionInvocation fi)
        {
            if (SymbolTable.isFunction(fi.fxnId.id))
            {
                //User defined function

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
                    default:
                        throw new NotImplementedException();
                }
            }
            else
            {
                //Error!
                Console.Error.WriteLine("Unknown function call: " + fi.fxnId);
            }
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
            result = literal;
        }

        public void visit(Assignment astmt)
        {
            throw new NotImplementedException();
        }

        public void visit(FunctionDecl fdecl)
        {
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
            //TODO: add params
            main.block.ForEach(stmt => stmt.accept(this));
        }
    }
}
