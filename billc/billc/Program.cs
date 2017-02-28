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

            string binop_test = "void main() {\n int a = a + 2;\n}";
            string fxn_test = "void main() {\n int a = 5;\n return a;\n}";
            string if_test = "void main() {\n int a = 5;\n if (a == 5) { return 1;} return 2;\n}";
            string while_test = "void main() {\n while(b){ return 2; }\n}";

            MyParser parser = new MyParser("Bill_Grammar.cgt");

            ProgramNode program = (ProgramNode)parser.Parse(while_test);

            //TypeValidatorVisitor tvv = new TypeValidatorVisitor();

            //program.accept(tvv);

            Console.WriteLine(program.ToString());

            Console.ReadLine();
        }
    }
}
