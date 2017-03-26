﻿using billc.TreeNodes;
using billc.Visitors;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace billc.Tests
{
    [TestFixture]
    class TypeValidatorVisitorTests
    {
        TestErrorReporter errReporter;
        TypeValidatorVisitor tvv;

        [SetUp]
        protected void SetUp()
        {
            errReporter = new TestErrorReporter();
            tvv = new TypeValidatorVisitor();
            tvv.errorReporter = errReporter;
        }

        [Test]
        public void noMain()
        {
            ProgramNode prgrm = new ProgramNode(new List<FunctionDecl>(), new List<ClassDecl>());

            prgrm.accept(tvv);
            Assert.False(tvv.isValidProgram);
            Assert.IsNotEmpty(errReporter.buffer);
        }

        [Test]
        public void simpleMain()
        {
            ProgramNode prgrm = new ProgramNode(new List<FunctionDecl>(), new List<ClassDecl>());
            prgrm.functions.Add(new FunctionDecl(new List<FormalParam>(), new Identifier("main"), "void", new List<Statement>()));

            prgrm.accept(tvv);
            Assert.True(tvv.isValidProgram);
            Assert.IsEmpty(errReporter.buffer);
        }

        [Test]
        public void notDefinedIdentifier()
        {
            ProgramNode prgrm = new ProgramNode(new List<FunctionDecl>(), new List<ClassDecl>());
            var fdecl = new FunctionDecl(new List<FormalParam>(), new Identifier("main"), "void", new List<Statement>());
            fdecl.block.Add(new LocalVarDecl("int", new Identifier("a"), new Identifier("b")));
            prgrm.functions.Add(fdecl);

            prgrm.accept(tvv);
            Assert.False(tvv.isValidProgram);
            Assert.IsNotEmpty(errReporter.buffer);
        }

        [Test]
        public void breakOutsideLoop()
        {
            ProgramNode prgrm = new ProgramNode(new List<FunctionDecl>(), new List<ClassDecl>());
            var fdecl = new FunctionDecl(new List<FormalParam>(), new Identifier("main"), "void", new List<Statement>());
            fdecl.block.Add(new Break());
            prgrm.functions.Add(fdecl);

            prgrm.accept(tvv);
            Assert.False(tvv.isValidProgram);
            Assert.IsNotEmpty(errReporter.buffer);
        }

        [Test]
        public void breakInsideLoop()
        {
            ProgramNode prgrm = new ProgramNode(new List<FunctionDecl>(), new List<ClassDecl>());
            var fdecl = new FunctionDecl(new List<FormalParam>(), new Identifier("main"), "void", new List<Statement>());
            var loopBody = new List<Statement>();
            loopBody.Add(new Break());
            fdecl.block.Add(new WhileLoop(loopBody, new Literal(false)));
            prgrm.functions.Add(fdecl);

            prgrm.accept(tvv);
            Assert.True(tvv.isValidProgram);
            Assert.IsEmpty(errReporter.buffer);
        }

    }
}