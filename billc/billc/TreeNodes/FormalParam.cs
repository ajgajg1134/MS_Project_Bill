using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace billc.TreeNodes
{
    class FormalParam : Node
    {
        public string id;
        public string type;

        public FormalParam(string i, string t)
        {
            id = i;
            type = t;
        }

        public override void accept(Visitor v)
        {
            v.visit(this);
        }

        public override string ToString()
        {
            return type + " " + id;
        }

        public bool typeEqual(FormalParam other)
        {
            return type == other.type;
        }
        /// <summary>
        /// Determines if two parameter lists are type equivalent
        /// </summary>
        /// <param name="fparam"></param>
        /// <param name="other"></param>
        /// <returns>true if they are in order type equal, false otherwise (two empty lists are equal)</returns>
        public static bool typeEqual(List<FormalParam> fparam, List<FormalParam> other)
        {
            if (fparam.Count != other.Count)
            {
                return false;
            }
            for(int i = 0; i < fparam.Count; i++)
            {
                if (!fparam[i].typeEqual(other[i]))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
