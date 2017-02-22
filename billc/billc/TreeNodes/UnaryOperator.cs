using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace billc.TreeNodes
{
    enum unops
    {
        not, //Boolean not !
        negate //number negation -
    }
    class UnaryOperator : Expression
    {
        Expression inner;
        unops unop;

        public UnaryOperator(Expression inn, unops uno)
        {
            inner = inn;
            unop = uno;
        }

        public override string ToString()
        {
            return unopToString(unop) + inner.ToString();
        }

        public override string getResultType()
        {
            switch (unop)
            {
                case unops.not:
                    return "bool";
                case unops.negate:
                    return inner.getResultType();
                default:
                    Console.Error.WriteLine("Error in UnaryOperator node, unexpected type");
                    return "ERROR";
            }
        }

        public override string getResultTypeWithCheck(SymbolTable table)
        {
            switch (unop)
            {
                case unops.not:

                    return "bool";
                case unops.negate:
                    return inner.getResultType();
                default:
                    Console.Error.WriteLine("Error in UnaryOperator node, unexpected type");
                    return "ERROR";
            }
        }

        public static string unopToString(unops unop)
        {
            switch (unop)
            {
                case unops.not:
                    return "!";
                case unops.negate:
                    return "-";
                default:
                    Console.Error.WriteLine("Error in UnaryOperator node, unexpected type");
                    return "ERROR";
            }
        }

        public override void accept(Visitor v)
        {
            v.visit(this);
        }
    }
}
