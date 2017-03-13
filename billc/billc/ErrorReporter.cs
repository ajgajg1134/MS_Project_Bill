using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using billc.TreeNodes;

namespace billc
{
    class ErrorReporter
    {

        public static void Warning(string msg)
        {
            Console.Error.WriteLine("Warning: " + msg);
        }

        public static void Warning(string msg, Node n)
        {
            Console.Error.WriteLine("Warning: " + msg + "\n\t at line " + n.lineNum);
        }

        public static void Error(string msg)
        {
            Console.Error.WriteLine("ERROR: " + msg);
        }

        public static void Error(string msg, Node n)
        {
            Console.Error.WriteLine("ERROR: " + msg + "\n\t at line " + n.lineNum);
        }
    }
}
