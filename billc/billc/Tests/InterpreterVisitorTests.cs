using billc.Visitors;
using billc.TreeNodes;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.calitha.goldparser;
using System.Reflection;

namespace billc.Tests
{
    [TestFixture]
    class InterpreterVisitorTests
    {
        InterpreterVisitor iv;
        TestErrorReporter errorReporter;
        string outputBuffer;
        string inputBuffer;

        private void testPrinter(string s)
        {
            outputBuffer += s;
        }

        private string testInput()
        {
            return inputBuffer;
        }
        
        [SetUp]
        public void Setup()
        {
            iv = new InterpreterVisitor();
            errorReporter = new TestErrorReporter();
            outputBuffer = "";
            iv.errorReporter = errorReporter;
            iv.println = testPrinter;
            iv.input = testInput;
        }

        [Test]
        public void EmptyMain()
        {
            ProgramNode prgrm = new ProgramNode(new List<FunctionDecl>(), new List<ClassDecl>());
            prgrm.functions.Add(new FunctionDecl(new List<FormalParam>(), new Identifier("main"), "void", new List<Statement>()));

            prgrm.accept(iv);

            Assert.IsEmpty(errorReporter.buffer);
        }

        [Test]
        public void PrintLiteralString()
        {
            const string toPrint = "Hello World";
            ProgramNode prgrm = new ProgramNode(new List<FunctionDecl>(), new List<ClassDecl>());
            var fdecl = new FunctionDecl(new List<FormalParam>(), new Identifier("main"), "void", new List<Statement>());
            var expressions = new List<Expression>();
            expressions.Add(new Literal(toPrint));
            fdecl.block.Add(new FunctionInvocation(new Identifier("println"), expressions));
            prgrm.functions.Add(fdecl);

            prgrm.accept(iv);

            Assert.IsEmpty(errorReporter.buffer);
            Assert.AreEqual(toPrint, outputBuffer);
        }

        [Test]
        public void BasicInOut()
        {
            const string fake_input = "Potato";
            inputBuffer = fake_input;

            ProgramNode prgrm = new ProgramNode(new List<FunctionDecl>(), new List<ClassDecl>());
            var fdecl = new FunctionDecl(new List<FormalParam>(), new Identifier("main"), "void", new List<Statement>());
            //string a = input();
            fdecl.block.Add(new LocalVarDecl("string", new Identifier("a"), new FunctionInvocation(new Identifier("input"), new List<Expression>())));
            //println(a);
            var expressions = new List<Expression>();
            expressions.Add(new Identifier("a"));
            fdecl.block.Add(new FunctionInvocation(new Identifier("println"), expressions));
            prgrm.functions.Add(fdecl);

            prgrm.accept(iv);

            Assert.IsEmpty(errorReporter.buffer);
            Assert.AreEqual(fake_input, outputBuffer);
        }

        [Test]
        public void ToStrInt()
        {
            const int val = 5;
            ProgramNode prgrm = new ProgramNode(new List<FunctionDecl>(), new List<ClassDecl>());
            var fdecl = new FunctionDecl(new List<FormalParam>(), new Identifier("main"), "void", new List<Statement>());
            //int a = 5;
            fdecl.block.Add(new LocalVarDecl("int", new Identifier("a"), new Literal(val)));
            //println(toStr(a));
            var toStrExp = new List<Expression>();
            toStrExp.Add(new Identifier("a"));
            var toPrintExp = new List<Expression>();
            toPrintExp.Add(new FunctionInvocation(new Identifier("toStr"), toStrExp));
            fdecl.block.Add(new FunctionInvocation(new Identifier("println"), toPrintExp));
            prgrm.functions.Add(fdecl);

            prgrm.accept(iv);

            Assert.IsEmpty(errorReporter.buffer);
            Assert.AreEqual(val + "", outputBuffer);
        }

        [Test]
        public void BinaryAdd([Random(0, 500, 2)] int a, [Random(0, 500, 2)] int b)
        {
            const string varName = "a";
            ProgramNode prgrm = new ProgramNode(new List<FunctionDecl>(), new List<ClassDecl>());
            var fdecl = new FunctionDecl(new List<FormalParam>(), new Identifier("main"), "void", new List<Statement>());
            //int a = 5;
            var add = new BinaryOperator(new Literal(a), new Literal(b), binops.add);
            fdecl.block.Add(new LocalVarDecl("int", new Identifier(varName), add));

            //println(toStr(a));
            var toStrExp = new List<Expression>();
            toStrExp.Add(new Identifier("a"));
            var toPrintExp = new List<Expression>();
            toPrintExp.Add(new FunctionInvocation(new Identifier("toStr"), toStrExp));
            fdecl.block.Add(new FunctionInvocation(new Identifier("println"), toPrintExp));
            prgrm.functions.Add(fdecl);

            prgrm.accept(iv);

            Assert.IsEmpty(errorReporter.buffer);
            Assert.AreEqual((a + b) + "", outputBuffer);
        }
    }
}
