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
            Console.Error.WriteLine("Warning: " + msg + "\n\t at line " + (n.lineNum + 1) + ": \"" + n.ToString() + "\"");
        }

        public void Error(string msg)
        {
            Console.Error.WriteLine("ERROR: " + msg);
        }

        public void Error(string msg, Node n)
        {
            Console.Error.WriteLine("ERROR: " + msg + "\n\t at line " + (n.lineNum + 1) + ": \"" + n.ToString() + "\"");
        }

        public void Fatal(string msg)
        {
            Console.Error.WriteLine("FATAL: " + msg + "\n This error indicates you have found a bug in the BILL Interpreter. Contact your Instructor or file an issue \'https://github.com/ajgajg1134/MS_Project_Bill'");
        }
    }
}
