using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.calitha.goldparser;

namespace billc
{
    class Program
    {
        static void Main(string[] args)
        {
            string test = "void main() {\n a = 2 + 2; }";

            MyParser parser = new MyParser("Bill_Grammar.cgt");

            parser.Parse(test);

        }
    }
}
