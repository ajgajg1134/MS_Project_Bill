﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace billc.TreeNodes
{
    abstract class Node : Visitable
    {
        public abstract void accept(Visitor v);
    }
}