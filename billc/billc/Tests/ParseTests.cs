using billc.TreeNodes;
using com.calitha.goldparser;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace billc.Tests
{
    [TestFixture]
    class ParseTests
    {
        MyParser parser;

        [SetUp]
        protected void SetUp()
        {
            var assembly = Assembly.GetExecutingAssembly();
            using (var stream = assembly.GetManifestResourceStream("billc.Bill_Grammar.cgt"))
            {
                parser = new MyParser(stream);
            }
        }

        [Test]
        public void EmptyMain()
        {
            string prgrm = "void main() {}";
            object parseRet = parser.Parse(prgrm);
            Assert.IsNotNull(parseRet);
            Assert.IsInstanceOf<ProgramNode>(parseRet);
        }
    }
}
