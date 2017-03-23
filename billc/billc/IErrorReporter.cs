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
        /// <summary>
        /// Issue a warning, interpreter may continue but results may be unexpected
        /// </summary>
        /// <param name="msg">a message indicating the warning</param>
        void Warning(string msg);

        /// <summary>
        /// Issue a warning
        /// </summary>
        /// <param name="msg">a message indicating the warning</param>
        /// <param name="n">the node where the warning was found</param>
        void Warning(string msg, Node n);

        /// <summary>
        /// Issue an error, interpreter must stop executing.
        /// There is an issue in the source code.
        /// </summary>
        /// <param name="msg">a message indicating the error</param>
        void Error(string msg);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg">a message indicating the error</param>
        /// <param name="n">the node where the error was found</param>
        void Error(string msg, Node n);

        /// <summary>
        /// An error has occurred in the interpreter. This indicates a bug
        /// has been found in the interpreter NOT the source code being executed.
        /// </summary>
        /// <param name="msg">a message detailing what happened</param>
        void Fatal(string msg);
    }
}
