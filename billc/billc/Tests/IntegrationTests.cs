using NUnit.Framework;
using billc.TreeNodes;
using System.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.calitha.goldparser;
using billc.Visitors;

namespace billc.Tests
{
    /// <summary>
    /// These tests are designed to test end to end
    /// They will use the parser, type checker, and interpreter to run full programs
    /// and verify they run correctly.
    /// </summary>
    [TestFixture]
    class IntegrationTests
    {
        InterpreterVisitor iv;
        TestErrorReporter errorReporter;
        TypeValidatorVisitor tvv;
        MyParser parser;
        string outputBuffer;
        string inputBuffer;

        [SetUp]
        public void Setup()
        {
            outputBuffer = "";
            inputBuffer = "";
            iv = new InterpreterVisitor();
            errorReporter = new TestErrorReporter();
            tvv = new TypeValidatorVisitor();
            iv.input = testInput;
            iv.println = testPrinter;
            var assembly = Assembly.GetExecutingAssembly();
            using (var stream = assembly.GetManifestResourceStream("billc.Bill_Grammar_v2.cgt"))
            {
                parser = new MyParser(stream);
            }
            parser.errorReporter = errorReporter;
        }

        private void testPrinter(string s)
        {
            outputBuffer += s;
        }

        private string testInput()
        {
            return inputBuffer;
        }

        [Test, Timeout(2000)]
        public void WhileLoopPrinting()
        {
            string src = "void main() { int a = 0; while(a < 5){ println(toStr(a));\n a = a + 1; }}";

            object parseResult = parser.Parse(src);
            Assert.NotNull(parseResult);
            Assert.False(parser.badParse);
            Assert.IsEmpty(errorReporter.buffer);
            Assert.IsInstanceOf<ProgramNode>(parseResult);
            var prgrm = parseResult as ProgramNode;

            prgrm.accept(iv);
            Assert.IsEmpty(errorReporter.buffer);
            Assert.AreEqual("01234", outputBuffer);
        }

        [Test, Timeout(2000)]
        public void ConditionalPrint([Values("true", "false")] string boolean)
        {
            const string falsePrint = "Was False";
            const string truePrint = "Was True";
            string src = "void main() { bool b = " + boolean + "; if (b) { println(\"" + truePrint + "\"); } else { println(\"" + falsePrint + "\"); }}";
            object parseResult = parser.Parse(src);
            Assert.NotNull(parseResult);
            Assert.False(parser.badParse);
            Assert.IsEmpty(errorReporter.buffer);
            Assert.IsInstanceOf<ProgramNode>(parseResult);
            var prgrm = parseResult as ProgramNode;

            prgrm.accept(iv);
            Assert.IsEmpty(errorReporter.buffer);
            if (boolean == "true")
            {
                Assert.AreEqual(truePrint, outputBuffer);
            }
            else
            {
                Assert.AreEqual(falsePrint, outputBuffer);
            }
        }

        [Test, Timeout(2000)]
        public void StringLength([Values("", "hello", "what a long string")] string val)
        {
            string src = "void main() { int a = length(\"" + val + "\"); println(toStr(a)); }";
            object parseResult = parser.Parse(src);
            Assert.NotNull(parseResult);
            Assert.False(parser.badParse);
            Assert.IsEmpty(errorReporter.buffer);
            Assert.IsInstanceOf<ProgramNode>(parseResult);
            var prgrm = parseResult as ProgramNode;

            prgrm.accept(iv);
            Assert.IsEmpty(errorReporter.buffer);
            Assert.AreEqual(val.Length + "", outputBuffer);
        }

        [Test, Timeout(2000)]
        public void FunctionCallWithParams([Values(0, 5)] int val)
        {
            string src = "void main() { println(toStr(addTwo(" + val + "))); }\n int addTwo(int a){ return a + 2; }";
            object parseResult = parser.Parse(src);
            Assert.NotNull(parseResult);
            Assert.False(parser.badParse);
            Assert.IsEmpty(errorReporter.buffer);
            Assert.IsInstanceOf<ProgramNode>(parseResult);
            var prgrm = parseResult as ProgramNode;

            prgrm.accept(tvv);
            prgrm.accept(iv);
            Assert.IsEmpty(errorReporter.buffer);
            Assert.AreEqual((val + 2) + "", outputBuffer);
        }
    }
}
