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
        mod,
    }
    class BinaryOperator : Expression
    {
        public Expression left;
        public Expression right;
        public binops op;

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

        public override void accept(Visitor v)
        {
            v.visit(this);
        }

        /// <summary>
        /// Gets the result type of this expression.
        /// Assumes that the types are already valid
        /// </summary>
        /// <returns>the result type of this expression</returns>
        public override string getResultType()
        {
            string l = left.getResultType();
            string r = right.getResultType();

            return getResultTypeFromOp(op, l, r);
        }

        public override string getResultTypeWithCheck(SymbolTable table)
        {
            string l = left.getResultTypeWithCheck(table);
            string r = right.getResultTypeWithCheck(table);

            if (isValidTypeWithOp(op, l))
            {
                if (isValidTypeWithOp(op, r))
                {
                    return getResultTypeFromOp(op, l, r);
                } else
                {
                    Console.Error.WriteLine("ERROR: invalid type of RHS (" + r + ") with operator "+ binopToString(op));
                }
            } else
            {
                Console.Error.WriteLine("ERROR: invalid type of LHS (" + l + ") with operator " + binopToString(op));
            }
            return "ERROR";
        }

        /// <summary>
        /// Checks to see if an operator is compatible with a given type
        /// </summary>
        /// <param name="binop">the operator to check</param>
        /// <param name="type">the type to check</param>
        /// <returns>true if the type is compatible, false otherwise</returns>
        public static bool isValidTypeWithOp(binops binop, string type)
        {
            switch (binop)
            {
                case binops.add:
                    return PrimitiveTypes.isNumberType(type) || type == "String";
                case binops.div:
                case binops.eq:
                case binops.gt:
                case binops.gte:
                case binops.lt:
                case binops.lte:
                case binops.mul:
                case binops.neq:
                case binops.sub:
                    return PrimitiveTypes.isNumberType(type) || type == "char";
                case binops.and:
                case binops.or:
                    return type == "bool";
                case binops.mod:
                    return type == "int";
                default:
                    Console.Error.WriteLine("Error in BinaryOperator node, unexpected type with operator");
                    return false;
            }
        }

        /// <summary>
        /// Determines the result type of an operation given the types of the left and right hand side
        /// Assumes that the types are valid for this given operation
        /// </summary>
        /// <param name="binop">the operation performed</param>
        /// <param name="startTypeL">the type of the LHS</param>
        /// <param name="startTypeR">the type of the RHS</param>
        /// <returns>the result type of this operation</returns>
        public static string getResultTypeFromOp(binops binop, string startTypeL, string startTypeR)
        {
            switch (binop)
            {
                case binops.eq:
                case binops.gt:
                case binops.gte:
                case binops.lt:
                case binops.lte:
                case binops.and:
                case binops.or:
                case binops.neq:
                    return "bool";
                case binops.add:
                case binops.div:
                case binops.mul:
                case binops.sub:
                    if (startTypeL == "int" && startTypeR == "int")
                        return "int";
                    else if (startTypeL != "String")
                        return "double";
                    else
                        return "String";
                case binops.mod:
                    return "int";
                default:
                    Console.Error.WriteLine("Error in BinaryOperator node, unexpected type with operator");
                    return "ERROR";
            }
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
