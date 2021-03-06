﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace billc.TreeNodes
{
    enum lit_type
    {
        boolean,
        integer,
        floating,
        character,
        string_l,
        null_l
    }
    class Literal : Expression
    {
        public lit_type type;
        public bool b;
        public int i;
        public double d;
        public char c;
        public string s;

        internal static IErrorReporter errorReporter = new ErrorReporter();

        public Literal(bool b)
        {
            this.b = b;
            type = lit_type.boolean;
        }

        public Literal(int i)
        {
            this.i = i;
            type = lit_type.integer;
        }

        public Literal(double d)
        {
            this.d = d;
            type = lit_type.floating;
        }

        public Literal(char c)
        {
            this.c = c;
            type = lit_type.character;
        }
        
        public Literal(string s)
        {
            this.s = s;
            type = lit_type.string_l;
        }
        /// <summary>
        /// Defaults this literal to null
        /// </summary>
        public Literal()
        {
            type = lit_type.null_l;
        }

        public Literal(object o)
        {
            if (o is int)
            {
                i = (int)o;
                type = lit_type.integer;
            } else if (o is bool)
            {
                b = (bool)o;
                type = lit_type.boolean;
            } else if (o is double)
            {
                d = (double)o;
                type = lit_type.floating;
            } else if (o is char)
            {
                c = (char)o;
                type = lit_type.character;
            }
            else if (o is string)
            {
                s = (string)o;
                type = lit_type.string_l;
            }
            else
            {
                type = lit_type.null_l;
            }
        }

        public Literal(Literal l)
        {
            b = l.b;
            i = l.i;
            d = l.d;
            c = l.c;
            s = l.s;
            type = l.type;
        }

        public override void accept(Visitor v)
        {
            v.visit(this);
        }

        public override string ToString()
        {
            switch (type)
            {
                case lit_type.boolean:
                    return b.ToString().ToLower();
                case lit_type.character:
                    return c.ToString();
                case lit_type.floating:
                    return d.ToString();
                case lit_type.integer:
                    return i.ToString();
                case lit_type.null_l:
                    return "null";
                case lit_type.string_l:
                    return s;
                default:
                    Console.Error.WriteLine("Error in Literal node, unexpected literal type");
                    return "ERROR";
            }
        }

        public override string getResultType()
        {
            switch (type)
            {
                case lit_type.boolean:
                    return "bool";
                case lit_type.character:
                    return "char";
                case lit_type.floating:
                    return "double";
                case lit_type.integer:
                    return "int";
                case lit_type.null_l:
                    return "null";
                case lit_type.string_l:
                    return "String";
                default:
                    Console.Error.WriteLine("Error in Literal node type when getting result type");
                    return "ERROR";
            }
        }

        public override string getResultTypeWithCheck(SymbolTable table)
        {
            //A literal can not have a type error.
            return getResultType();
        }

        /// <summary>
        /// Performs a binary operation on two literals, types are assumed correct
        /// </summary>
        /// <param name="l"></param>
        /// <param name="r"></param>
        /// <returns>a literal holding the result of the operation</returns>
        public static Literal performBinOp(Literal l, Literal r, binops bop)
        {
            switch (l.type)
            {
                case lit_type.boolean:
                    switch (bop)
                    {
                        case binops.eq:
                            return new Literal(l.b == r.b);
                        case binops.neq:
                            return new Literal(l.b != r.b);
                        case binops.and:
                            return new Literal(l.b && r.b);
                        case binops.or:
                            return new Literal(l.b || r.b);
                        default:
                            Console.Error.WriteLine("Error! unexpected binop type in literal");
                            return null;
                    }
                case lit_type.character:
                    switch (bop)
                    {
                        case binops.eq:
                            return new Literal(l.c == r.c);
                        case binops.neq:
                            return new Literal(l.c != r.c);
                        default:
                            return null;
                    }
                case lit_type.floating:
                    //Upcast rhs to double if it's an integer
                    if (r.type == lit_type.integer)
                    {
                        r.d = r.i;
                    }
                    switch (bop)
                    {
                        case binops.eq:
                            return new Literal(l.d == r.d);
                        case binops.gt:
                            return new Literal(l.d > r.d);
                        case binops.gte:
                            return new Literal(l.d >= r.d);
                        case binops.lt:
                            return new Literal(l.d < r.d);
                        case binops.lte:
                            return new Literal(l.d <= r.d);
                        case binops.neq:
                            return new Literal(l.d != r.d);
                        case binops.add:
                            return new Literal(l.d + r.d);
                        case binops.div:
                            return new Literal(l.d / r.d); //TODO: div by zero check?
                        case binops.mul:
                            return new Literal(l.d * r.d);
                        case binops.sub:
                            return new Literal(l.d - r.d);
                        case binops.mod:
                            return new Literal(l.d % r.d);
                        default:
                            Console.Error.WriteLine("Error! unexpected binop type in literal");
                            return null;
                    }
                case lit_type.integer:
                    //If rhs is floating this should be redone as a floating point op
                    if (r.type == lit_type.floating)
                    {
                        double d = l.i;
                        return performBinOp(new Literal(d), r, bop);
                    }
                    switch (bop)
                    {
                        case binops.eq:
                            return new Literal(l.i == r.i);
                        case binops.gt:
                            return new Literal(l.i > r.i);
                        case binops.gte:
                            return new Literal(l.i >= r.i);
                        case binops.lt:
                            return new Literal(l.i < r.i);
                        case binops.lte:
                            return new Literal(l.i <= r.i);
                        case binops.neq:
                            return new Literal(l.i != r.i);
                        case binops.add:
                            return new Literal(checked(l.i + r.i));
                        case binops.div:
                            return new Literal(l.i / r.i); //TODO: div by zero check?
                        case binops.mul:
                            return new Literal(checked(l.i * r.i));
                        case binops.sub:
                            return new Literal(checked(l.i - r.i));
                        case binops.mod:
                            return new Literal(l.i % r.i);
                        default:
                            Console.Error.WriteLine("Error! unexpected binop type in literal");
                            return null;
                    }
                case lit_type.null_l:
                    Console.Error.WriteLine("Error! unexpected null value in literal binop");
                    return null;
                case lit_type.string_l:
                    switch (bop)
                    {
                        case binops.eq:
                            return new Literal(l.s == r.s);
                        case binops.neq:
                            return new Literal(l.s != r.s);
                        case binops.add:
                            return new Literal(l.s + r.s);
                        default:
                            errorReporter.Fatal("Error in Literal node, unexpected literal type");
                            return null;
                    }
                default:
                    errorReporter.Fatal("Error in Literal node, unexpected literal type");
                    return null;
            }
        }

        public Literal performUnop(unops unop)
        {
            switch (type)
            {
                case lit_type.boolean:
                    switch (unop)
                    {
                        case unops.not:
                            return new Literal(!b);
                        default:
                            errorReporter.Fatal("Attempted to perform not on non-boolean type in performUnop! Should have been caught by typechecker");
                            return null;
                    }
                case lit_type.integer:
                    switch (unop)
                    {
                        case unops.negate:
                            return new Literal(-i);
                        default:
                            errorReporter.Fatal("Attempted to perform not on int type in performUnop! Should have been caught by typechecker");
                            return null;
                    }
                case lit_type.floating:
                    switch (unop)
                    {
                        case unops.negate:
                            return new Literal(-d);
                        default:
                            errorReporter.Fatal("Attempted to perform not on double type in performUnop! Should have been caught by typechecker");
                            return null;
                    }
                case lit_type.null_l:
                    errorReporter.Error("Null Reference! Can not perform '" + UnaryOperator.unopToString(unop) + "' on a null variable.", this);
                    return null;
                default:
                    errorReporter.Fatal("Perform unop encountered an unexepected liter an operator.");
                    return null;
            }
        }
    }
}
