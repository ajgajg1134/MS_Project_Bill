using billc.Visitors;
using billc.TreeNodes;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace billc.Tests
{
    [TestFixture]
    class InterpreterVisitorTests
    {
        InterpreterVisitor iv;
        TestErrorReporter errorReporter;
        TypeValidatorVisitor tvv;
        
        [SetUp]
        public void Setup()
        {
            iv = new InterpreterVisitor();
            errorReporter = new TestErrorReporter();
            tvv = new TypeValidatorVisitor();
            iv.errorReporter = errorReporter;
            tvv.errorReporter = errorReporter;
        }

        [Test]
        public void emptyMain()
        {
            ProgramNode prgrm = new ProgramNode(new List<FunctionDecl>(), new List<ClassDecl>());
            prgrm.functions.Add(new FunctionDecl(new List<FormalParam>(), new Identifier("main"), "void", new List<Statement>()));

            prgrm.accept(tvv);
            prgrm.accept(iv);

            Assert.True(tvv.isValidProgram);
            Assert.IsEmpty(errorReporter.buffer);
        }
    }
}
