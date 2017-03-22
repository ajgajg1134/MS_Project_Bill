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
        public Expression inner;
        public unops unop;

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

        public static bool isValidTypeWithOp(string type, unops unop)
        {
            switch (unop)
            {
                case unops.negate:
                    return PrimitiveTypes.isNumberType(type);
                case unops.not:
                    return type == "bool";
                default:
                    Console.Error.WriteLine("Internal Error in unop. Unexpected unary operator type");
                    return false;
            }
        }

        /// <summary>
        /// Determines the result type of this unary operation
        /// right now a unary op will never change the inner type so this function is trivial
        /// </summary>
        /// <param name="unop">the operation being performed</param>
        /// <param name="innerType">the type the operation is being performed on</param>
        /// <returns>the rtesult type of this operation</returns>
        public static string getResultTypeFromOp(unops unop, string innerType)
        {
            return innerType;
        }

        public override void accept(Visitor v)
        {
            v.visit(this);
        }
    }
}
