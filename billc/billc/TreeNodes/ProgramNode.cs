using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace billc.TreeNodes
{
    class ProgramNode : Node
    {
        List<FunctionDecl> functions;
        List<ClassDecl> classes;

        public ProgramNode(List<FunctionDecl> fs, List<ClassDecl> cs)
        {
            functions = fs;
            classes = cs;
        }

        public void addFunc(FunctionDecl f)
        {
            functions.Add(f);
        }
        public void addClass(ClassDecl c)
        {
            classes.Add(c);
        }

        public override string ToString()
        {
            return classes.Select(s => s.ToString()).Aggregate("", (a, b) => a + "\n" + b) +
                functions.Select(f => f.ToString()).Aggregate("", (a, b) => a + "\n" + b);
        }

        public override void accept(Visitor v)
        {
            v.visit(this);
        }
    }
}
