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
    }
}
