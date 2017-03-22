using billc.TreeNodes;
using com.calitha.goldparser;
using NUnit.Framework;
using System.Reflection;

namespace billc.Tests
{
    [TestFixture]
    class ParseTests
    {
        internal class TestErrorReporter : IErrorReporter
        {
            internal string buffer;
            internal Node inObj;
            public void Error(string msg)
            {
                buffer = msg;
            }

            public void Error(string msg, Node n)
            {
                buffer = msg;
                inObj = n;
            }

            public void Warning(string msg)
            {
                buffer = msg;
            }

            public void Warning(string msg, Node n)
            {
                buffer = msg;
                inObj = n;
            }
        }

        MyParser parser;
        TestErrorReporter errReporter;
        
        [SetUp]
        protected void SetUp()
        {
            errReporter = new TestErrorReporter();
            var assembly = Assembly.GetExecutingAssembly();
            using (var stream = assembly.GetManifestResourceStream("billc.Bill_Grammar.cgt"))
            {
                parser = new MyParser(stream);
            }
            parser.errorReporter = errReporter;
        }

        /// <summary>
        /// Takes source and verifies if that parses to a valid program
        /// Verifies a program node object is returned and there is at least 1 function
        /// in it
        /// </summary>
        /// <param name="src">source string to parse</param>
        /// <returns>the parsed program node</returns>
        public ProgramNode testParse(string src)
        {
            object parseRet = parser.Parse(src);
            Assert.IsNotNull(parseRet);
            Assert.IsInstanceOf<ProgramNode>(parseRet);
            ProgramNode pgm = parseRet as ProgramNode;
            Assert.NotZero(pgm.functions.Count);
            return pgm;
        }

        [Test]
        public void EmptyMain()
        {
            string src = "void main() {}";
            testParse(src);
        }

        [Test]
        public void VarDecl()
        {
            string src = "void main() {int a = 5;}";
            var prgrm = testParse(src);
            Assert.NotZero(prgrm.functions[0].block.Count);
            Assert.IsInstanceOf<LocalVarDecl>(prgrm.functions[0].block[0]);
            LocalVarDecl lvDecl = prgrm.functions[0].block[0] as LocalVarDecl;
            Expression val = lvDecl.val;
            Assert.IsInstanceOf<Literal>(val);
            Literal val_l = val as Literal;
            Assert.AreEqual(val_l.i, 5);
            Assert.AreEqual(lvDecl.id.id, "a");
            Assert.AreEqual(lvDecl.type, "int");
        }

        [Test]
        public void ExpressionParsing()
        {
            string src = "void main() {int a = 2 + 3 - 1 * 7;}";
            var prgrm = testParse(src);
            Assert.NotZero(prgrm.functions[0].block.Count);
        }

        [Test]
        public void MissingBrace()
        {
            string src = "void main() { ";
            object parseRet = parser.Parse(src);
            Assert.IsNull(parseRet);
            Assert.IsNotEmpty(errReporter.buffer);
        }
    }
}
