using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using billc.TreeNodes;

namespace billc
{
    class ErrorReporter : IErrorReporter
    {

        public void Warning(string msg)
        {
            Console.Error.WriteLine("Warning: " + msg);
        }

        public void Warning(string msg, Node n)
        {
            Console.Error.WriteLine("Warning: " + msg + "\n\t at line " + n.lineNum + ": \"" + n.ToString() + "\"");
        }

        public void Error(string msg)
        {
            Console.Error.WriteLine("ERROR: " + msg);
        }

        public void Error(string msg, Node n)
        {
            Console.Error.WriteLine("ERROR: " + msg + "\n\t at line " + n.lineNum + ": \"" + n.ToString() + "\"");
        }
    }
}
