using billc.TreeNodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace billc.Visitors
{
    class SymbolTableVisitor : Visitor
    {

        SymbolTable symTable = new SymbolTable();

        public SymbolTableVisitor()
        {
            
        }

        public SymbolTableVisitor(SymbolTableVisitor stv)
        {
            symTable = new SymbolTable(stv.symTable);
        }

        public void visit(ClassDecl cdecl)
        {
            SymbolTable.addClass(cdecl.id.id, cdecl);
        }

        public void visit(BinaryOperator bop)
        {
            //do nothing
        }

        public void visit(FormalParam fparam)
        {
            symTable.addLocalVar(fparam.id.id, fparam.type);
        }

        public void visit(LocalVarDecl ldecl)
        {
            symTable.addLocalVar(ldecl.id.id, ldecl.type);
        }

        public void visit(UnaryOperator unop)
        {
            //do nothing
        }

        public void visit(FunctionInvocation fi)
        {
            throw new NotImplementedException();
        }

        public void visit(ForLoop floop)
        {
            throw new NotImplementedException();
        }

        public void visit(Identifier id)
        {
            throw new NotImplementedException();
        }

        public void visit(WhileLoop wloop)
        {
            throw new NotImplementedException();
        }

        public void visit(Continue ct)
        {
            //do nothing
        }

        public void visit(Break br)
        {
            //do nothing
        }

        public void visit(Return ret)
        {
            //do nothing
        }

        public void visit(Conditional cond)
        {
            SymbolTableVisitor stv_then = new SymbolTableVisitor(this);
            cond.thenBlock.ForEach(stmt => stmt.accept(stv_then));
        }

        public void visit(Expression exp)
        {
            throw new NotImplementedException();
        }

        public void visit(Literal literal)
        {
            //do nothing
        }

        public void visit(Assignment astmt)
        {
            //do nothing
        }

        public void visit(FunctionDecl fdecl)
        {
            SymbolTable.addFunction(fdecl);
            SymbolTableVisitor stv = new SymbolTableVisitor(this);
            fdecl.fParams.ForEach(fparam => fparam.accept(stv));
            fdecl.block.ForEach(stmt => stmt.accept(stv));
        }

        public void visit(ProgramNode node)
        {
            node.classes.ForEach(c => c.accept(this));
            node.functions.ForEach(f => f.accept(this));
        }
    }
}
