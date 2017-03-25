using billc.TreeNodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace billc.Tests
{
    class TestErrorReporter : IErrorReporter
    {
        internal string buffer;
        internal Node inObj;

        public TestErrorReporter()
        {
            buffer = "";
        }
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
}
