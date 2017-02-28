﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace billc.TreeNodes
{
    class FunctionInvocation : Expression
    {
        Identifier fxnId;
        List<Expression> paramsIn;
        List<FunctionInvocation> chainedCalls; //These are function invocations on the result of this invocation

        public FunctionInvocation(Identifier id, List<Expression> paramsIn)
        {
            fxnId = id;
            this.paramsIn = paramsIn;
            chainedCalls = new List<FunctionInvocation>();
        }

        public FunctionInvocation(Identifier id, List<Expression> paramsIn, List<FunctionInvocation> chainedCalls) : this(id, paramsIn)
        {
            this.chainedCalls = chainedCalls;
        }

        public override void accept(Visitor v)
        {
            v.visit(this);
        }

        public override string getResultType()
        {
            throw new NotImplementedException();
        }

        public override string getResultTypeWithCheck(SymbolTable table)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return fxnId.ToString() + "(" + toString(paramsIn) + ")";
        }
    }
}