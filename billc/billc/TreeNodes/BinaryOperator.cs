using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace billc.TreeNodes
{
    enum binops
    {
        or,
        and,
        eq,
        neq,
        lt,
        lte,
        gt,
        gte,
        add,
        sub,
        mul,
        div,
        mod
    }
    class BinaryOperator : Expression
    {
        Expression left;
        Expression right;
        binops op;

        public BinaryOperator(Expression l, Expression r, binops o)
        {
            left = l;
            right = r;
            op = o;
        }

        public override string ToString()
        {
            return left.ToString() + " " + binopToString(op) + " " + right.ToString();
        }

        public static string binopToString(binops binop)
        {
            switch (binop)
            {
                case binops.add:
                    return "+";
                case binops.and:
                    return "&&";
                case binops.div:
                    return "/";
                case binops.eq:
                    return "==";
                case binops.gt:
                    return ">";
                case binops.gte:
                    return ">=";
                case binops.lt:
                    return "<";
                case binops.lte:
                    return "<=";
                case binops.mod:
                    return "%";
                case binops.mul:
                    return "*";
                case binops.neq:
                    return "!=";
                case binops.or:
                    return "||";
                case binops.sub:
                    return "-";
                default:
                    Console.Error.WriteLine("Error in BinaryOperator node, unexpected literal type");
                    return "ERROR";
            }
        }
    }
}
