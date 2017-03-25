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
            public void Fatal(string msg)
            {
                buffer = msg;
            }
        }

        MyParser parser;
        TestErrorReporter errReporter;

        [SetUp]
        protected void SetUp()
        {
            errReporter = new TestErrorReporter();
            var assembly = Assembly.GetExecutingAssembly();
            using (var stream = assembly.GetManifestResourceStream("billc.Bill_Grammar_v2.cgt"))
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

        [Test]
        public void InvokeParseOneArg()
        {
            string src = "void main() { foo(5);\n}";
            var prgrm = testParse(src);
            Assert.IsNotNull(prgrm);
            Assert.NotZero(prgrm.functions[0].block.Count);
            Assert.IsInstanceOf<FunctionInvocation>(prgrm.functions[0].block[0]);
            FunctionInvocation lvDecl = prgrm.functions[0].block[0] as FunctionInvocation;
            Assert.AreEqual(lvDecl.fxnId, new Identifier("foo"));
            Assert.AreEqual(lvDecl.paramsIn.Count, 1);
            Assert.IsInstanceOf<Literal>(lvDecl.paramsIn[0]);
            Assert.AreEqual((lvDecl.paramsIn[0] as Literal).i, 5);
        }

        [Test]
        public void InvokeParseMultipleArgs()
        {
            string src = "void main() { bar(5, true, 2.5); \n}";
            var prgrm = testParse(src);
            Assert.IsNotNull(prgrm);
            Assert.NotZero(prgrm.functions[0].block.Count);
            Assert.IsInstanceOf<FunctionInvocation>(prgrm.functions[0].block[0]);
            FunctionInvocation fi = prgrm.functions[0].block[0] as FunctionInvocation;
            Assert.AreEqual(fi.fxnId, new Identifier("bar"));
            Assert.AreEqual(fi.paramsIn.Count, 3);
            Assert.IsInstanceOf<Literal>(fi.paramsIn[0]);
            Assert.AreEqual((fi.paramsIn[0] as Literal).i, 5);
        }

        [Test]
        public void BasicWhileLoop()
        {
            string src = "void main() {\n while(b){ return 2; }\n}";
            var prgrm = testParse(src);
            Assert.IsNotNull(prgrm);
            Assert.NotZero(prgrm.functions[0].block.Count);
            Assert.IsInstanceOf<WhileLoop>(prgrm.functions[0].block[0]);
            WhileLoop wloop = prgrm.functions[0].block[0] as WhileLoop;
            Assert.IsInstanceOf<Identifier>(wloop.conditional);
        }


        [Test]
        public void BasicClassDecl()
        {
            string src = "void main() { }\n class foo(int a, string s){ }";
            var prgrm = testParse(src);
            Assert.IsNotNull(prgrm);
            Assert.NotNull(prgrm.classes.Count);
            ClassDecl cs = prgrm.classes[0];
            Assert.AreEqual(cs.id, new Identifier("foo"));
            Assert.AreEqual(cs.fields.Count, 2);
        }

        [Test]
        public void ForLoop()
        {
            string src = "void main() { for(int i = 0; i < 10; i += 1){ println(toStr(i)); } }";
            var prgrm = testParse(src);
            Assert.IsNotNull(prgrm);
            Assert.NotZero(prgrm.functions[0].block.Count);
            Assert.IsInstanceOf<ForLoop>(prgrm.functions[0].block[0]);
            ForLoop floop = prgrm.functions[0].block[0] as ForLoop;
            Assert.IsNotNull(floop.decl);
            Assert.IsNotNull(floop.condition);
            Assert.IsNotNull(floop.iteratedStmts);
            Assert.AreEqual(floop.iteratedStmts.Count, 1);
            Assert.AreEqual(floop.loopBody.Count, 1);
        }
    }
}
