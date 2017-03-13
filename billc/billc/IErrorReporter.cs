using billc.TreeNodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace billc
{
    interface IErrorReporter
    {
        void Warning(string msg);

        void Warning(string msg, Node n);

        void Error(string msg);

        void Error(string msg, Node n);
    }
}
