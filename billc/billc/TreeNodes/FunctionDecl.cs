using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace billc.TreeNodes
{
    class FunctionDecl : Node
    {
        public List<FormalParam> fParams;
        public Identifier id;
        public string retType;
        public List<Statement> block;

        public FunctionDecl(List<FormalParam> fps, Identifier i, string rt, List<Statement> b)
        {
            fParams = fps;
            id = i;
            retType = rt;
            block = b;
        }

        public override string ToString()
        {
            return retType + " " + id + " (" + string.Join(",", fParams.Select(f => f.ToString()).Aggregate("", (a, b) => a + b )) + ") {\n" +
                Statement.toString(block) + "}\n";
        }

        public override void accept(Visitor v)
        {
            v.visit(this);
        }

        /// <summary>
        /// Compares two function declarations for equality
        /// Two functions with identical signatures (ignoring body & return type) are considered equal
        /// </summary>
        /// <param name="obj">object to compare to</param>
        /// <returns>true if the signatures are the same, false otherwise</returns>
        public override bool Equals(object obj)
        {
            FunctionDecl other = obj as FunctionDecl;
            if (other == null)
            {
                return false;
            }
            if (id.Equals(other.id) && fParams.Count == other.fParams.Count)
            {
                for(int i = 0; i < fParams.Count; i++)
                {
                    if (fParams[i].type != other.fParams[i].type)
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            int code = id.GetHashCode();

            for (int i = 0; i < fParams.Count; i++)
            {
                code = code ^ fParams[i].GetHashCode();
            }
        }
    }
}
