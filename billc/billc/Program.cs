using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.calitha.goldparser;
using billc.TreeNodes;
using billc.Visitors;
using System.IO;

namespace billc
{
    class Program
    {
        static void Main(string[] args)
        {
            //These tests create semantically invalid programs, but are sufficient to test construction of the AST
            /*
            string binop_test = "void main() {\n int a = a + 2;\n}";
            string list_test = "void main() {\n List<int> a = new List<int>();\n }";
            string fxn_test = "void main() {\n int a = 5;\n return a;\n}";
            string if_test = "void main() {\n int a = 5;\n if (a == 5) { return 1 + 2;} return 2;\n}";
            string while_test = "void main() {\n while(b){ return 2; }\n}";
            string assign_test = "void main() {\n a = 7;\n}";
            string invoke_test = "void main() { foo(5);\n}";
            string invoke_test2 = "void main() { foo(5, true, 2.5); \n}";
            string member_test = "void main() { a.thing = 10; }";
            string class_test = "class foo(int a, string s){ }";
            string assignparamTest = "void main() { toString(a = 3); }";
            string forTest = "void main() { for(int i = 0; i < 10; i += 1){ println(toStr(i)); } }";
            string forTestbad = "void main() { for(int i = 2.5; i < 10; i += 1){ } }";
            string strSubTest = "void main() { String s = \"hello\";\n char c = s[0]; }";
            string strSubTestBadIdx = "void main() { String s = \"hello\";\n char c = s[2.5]; }";

            string interpret_test = "void main() { println(\"hello world\"); }";
            string interpret2_test = "void main() { println(toStr(2)); }";
            string interpret3_test = "void main() { println(toStr(1 + 2)); }";
            string interpret4_test = "void main() { int a = 2 + 5; \n println(toStr(a)); }";
            string interpret5_test = "void main() { int a = 2 + 5;\n a = 99;\n println(toStr(a)); }";
            string interpret6_test = "void main() { bool b = false; if (b) { println(\"was true\"); } else { println(\"was false\"); }}";
            string interpret7_test = "void main() { println(toStr(-4)); }";
            string interpret8_test = "void main() { int a = 0; while(a < 5){ println(toStr(a));\n a = a + 1; }}";
            string interpret9_test = "void main() { println(\"hello\" + \"nerd\"); }";
            string strSubInterTest = "void main() { String s = \"hello\";\n char c = s[0]; println(toStr(c));}";

            //Tests to see quality of parsing error messages
            string missingBrace = "void main() { ";
            string extraBrace = "void main() { }}";
            string badExpression = "void main() {\n int a = 5 + ;\n}";

            //Type checker errors
            string emptyProgram = "";
            string bad_types = "void main() {\n\n\n int a =\"hello\";\n}";
            string no_identifier = "void main() {\n\n\n int a = b + 2;\n}";
            string badCond = "void main() {\n if (5) { }\n }";
            string badLoop = "void main() {\n while(5) { }\n}";
            string goodLoop = "void main() {\n while(true) { }\n}";
            string contTest = "void main() {\n continue;\n }";
            string breakTest = "void main() {\n break;\n }";
            string retTest = "void main() {\n return 2;\n }";
            string unopTest = "void main() {\n bool b = !2;\n }";
            string assignTest = "void main() {\n a = 3;\n }";
            string funcInvokeTest = "void main() { println(toStr(addTwo(4))); }\n int addTwo(int a){ return a + 2; }";
            string funcInvokeTest2 = "void main() { addTwo(); }\n int addTwo(int a){ return a + 2; }";
            string classConst = "class foo(int a){ }\n void main() { foo x = new foo(5); }";
            */
            //string listConst = "void main() { List<int> x = new List<int>();\n int a = x[0]; }";
            string listConst = "void main() { List<int> x = {5}; x.add(2); println(toStr(x.size()));}";

            MyParser parser = new MyParser("Bill_Grammar_v2.cgt");

            IErrorReporter errorReporter = new ErrorReporter();

            ProgramNode program;

            if(args.Length == 0)
            {
                //running in debug mode for now (TODO replace with a usage message)
                program = (ProgramNode)parser.Parse(listConst);
                //Console.WriteLine("Usage: billc <filename>");
                //return;
            } else
            {
                try
                {
                    using (StreamReader sr = new StreamReader(args[0]))
                    {
                        string full_src = sr.ReadToEnd();
                        program = (ProgramNode)parser.Parse(full_src);
                    }
                } catch (FileNotFoundException)
                {
                    program = null;
                    errorReporter.Error("File not found: '" + args[0] + "'.");
                    return;
                }
            }

            if (program == null || parser.badParse)
            {
                errorReporter.Error("Parsing failed, Exiting."); // Syntax error?
                return;
            } 

            TypeValidatorVisitor tvv = new TypeValidatorVisitor();
            
            program.accept(tvv);

            if (!tvv.isValidProgram)
            {
                errorReporter.Error("Type check failed, Exiting.");
                return;
            }
            //Console.WriteLine(program.ToString());

            InterpreterVisitor iv = new InterpreterVisitor();

            //Console.WriteLine("Execute interpreter");
            program.accept(iv);
            //Console.WriteLine("Execution complete");

            //Console.ReadLine();
        }
    }
}
