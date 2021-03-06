﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace billc.TreeNodes
{
    class LocalVarDecl : Statement
    {
        public Identifier id;
        public string type;
        public Expression val;

        public LocalVarDecl(string t, Identifier i, Expression v)
        {
            type = t;
            id = i;
            val = v;
        }

        /// <summary>
        /// When using this constructor the type of this declaration must be added later before code gen
        /// </summary>
        /// <param name="i"></param>
        /// <param name="v"></param>
        public LocalVarDecl(Identifier i, Expression v)
        {
            id = i;
            val = v;
        }

        public void addType(string t)
        {
            type = t;
            if (val is ListLiteral)
            {
                (val as ListLiteral).internal_type = t.GetListType();
            }
        }

        public override string ToString()
        {
            return type + " " + id + " = " + val.ToString() + ";";
        }

        public override void accept(Visitor v)
        {
            v.visit(this);
        }
    }
}
