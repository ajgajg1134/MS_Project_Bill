﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace billc.TreeNodes
{
    class Identifier : Expression
    {
        public string id;

        public Identifier(string id)
        {
            this.id = id;
        }

        public override string ToString()
        {
            return id;
        }

        public override void accept(Visitor v)
        {
            throw new NotImplementedException();
        }

        public override string getResultType()
        {
            throw new NotImplementedException();
        }

        public override string getResultTypeWithCheck(SymbolTable table)
        {
            throw new NotImplementedException();
        }
    }
}
