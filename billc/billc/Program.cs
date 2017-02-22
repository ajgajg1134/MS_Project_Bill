using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.calitha.goldparser;
using billc.TreeNodes;
using billc.Visitors;

namespace billc
{
    class Program
    {
        static void Main(string[] args)
        {
            string test = "void main(int a) {\n a = -5; \n}\n int foo(string s, double d){ s = 2; }";

            MyParser parser = new MyParser("Bill_Grammar.cgt");

            ProgramNode program = (ProgramNode)parser.Parse(test);

            TypeValidatorVisitor tvv = new TypeValidatorVisitor();

            program.accept(tvv);

            Console.WriteLine(program.ToString());

            Console.ReadLine();
        }
    }
}
