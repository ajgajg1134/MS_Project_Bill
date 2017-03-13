using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.calitha.goldparser;
using billc.TreeNodes;
using billc.Visitors;

namespace billc
{
    class Program
    {
        static void Main(string[] args)
        { 
            //These tests create semantically invalid programs, but are sufficient to test construction of the AST
            string binop_test = "void main() {\n int a = a + 2;\n}";
            string fxn_test = "void main() {\n int a = 5;\n return a;\n}";
            string if_test = "void main() {\n int a = 5;\n if (a == 5) { return 1 + ;} return 2;\n}";
            string while_test = "void main() {\n while(b){ return 2; }\n}";
            string assign_test = "void main() {\n a = 7;\n}";
            string invoke_test = "void main() { foo(5);\n}";
            string invoke_test2 = "void main() { foo(5, true, 2.5); \n}";
            string member_test = "void main() { a.thing = 10; }";
            string class_test = "class foo(int a, string s){ }";
            string interpret_test = "void main() { println(\"hello world\"); }";
            string interpret2_test = "void main() { println(toStr(2)); }";
            string interpret3_test = "void main() { println(toStr(1 + 2)); }";
            string interpret4_test = "void main() { int a = 2 + 5; \n println(toStr(a)); }";

            //Tests to see quality of parsing error messages
            string missingBrace = "void main() { ";
            string extraBrace = "void main() { }}";
            string badExpression = "void main() {\n int a = 5 + ;\n}";

            //Type checker errors
            string emptyProgram = "";

            MyParser parser = new MyParser("Bill_Grammar.cgt");

            ProgramNode program = (ProgramNode)parser.Parse(emptyProgram);

            if (program == null)
            {
                ErrorReporter.Error("Parsing failed, Exiting.");
                return;
            } 

            TypeValidatorVisitor tvv = new TypeValidatorVisitor();
            
            program.accept(tvv);

            if (!tvv.isValidProgram)
            {
                ErrorReporter.Error("Type check failed, Exiting.");
                return;
            }
            Console.WriteLine(program.ToString());

            //InterpreterVisitor iv = new InterpreterVisitor();

            //Console.WriteLine("Execute interpreter");
            //program.accept(iv);
            //Console.WriteLine("Execution complete");

            Console.ReadLine();
        }
    }
}
