﻿using System;
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
            string if_test = "void main() {\n int a = 5;\n if (a == 5) { return 1 + 2;} return 2;\n}";
            string while_test = "void main() {\n while(b){ return 2; }\n}";
            string assign_test = "void main() {\n a = 7;\n}";
            string invoke_test = "void main() { foo(5);\n}";
            string invoke_test2 = "void main() { foo(5, true, 2.5); \n}";
            string member_test = "void main() { a.thing = 10; }";
            string class_test = "class foo(int a, string s){ }";
            string assignparamTest = "void main() { toString(a = 3); }";
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
            string funcInvokeTest = "void main() { addTwo(4); }\n int addTwo(int a){ return a + 2; }";
            string funcInvokeTest2 = "void main() { addTwo(); }\n int addTwo(int a){ return a + 2; }";



            MyParser parser = new MyParser("Bill_Grammar.cgt");

            IErrorReporter errorReporter = new ErrorReporter();

            ProgramNode program;

            if(args.Length == 0)
            {
                //running in debug mode for now (TODO replace with a usage message)
                program = (ProgramNode)parser.Parse(funcInvokeTest2);
            } else
            {
                //TODO: open a file and make it into a program
                program = null;
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
            Console.WriteLine(program.ToString());

            //InterpreterVisitor iv = new InterpreterVisitor();

            //Console.WriteLine("Execute interpreter");
            //program.accept(iv);
            //Console.WriteLine("Execution complete");

            Console.ReadLine();
        }
    }
}
