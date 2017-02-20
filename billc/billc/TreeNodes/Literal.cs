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
        lit_type type;
        bool b;
        int i;
        double d;
        char c;
        string s;

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
        public Literal()
        {
            type = lit_type.null_l;
        }
    }
}