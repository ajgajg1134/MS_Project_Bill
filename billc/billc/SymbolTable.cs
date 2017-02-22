using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using billc.TreeNodes;

namespace billc
{
    class SymbolTable
    {
        /// <summary>
        /// A dictionary mapping local var ids to their type
        /// </summary>
        Dictionary<string, string> localVars = new Dictionary<string, string>(); 

        public SymbolTable()
        {

        }

        public SymbolTable(SymbolTable table)
        {

        }

        /// <summary>
        /// Checks if a given id exists in the local var table
        /// </summary>
        /// <param name="id">the id to look for</param>
        /// <returns>true if it exists, false otherwise</returns>
        public bool isLocalVar(string id)
        {
            return localVars.ContainsKey(id);
        }

        /// <summary>
        /// Gets the type of a local var by its id, assumes the id is valid and exists
        /// </summary>
        /// <param name="id">the id of the local var</param>
        /// <returns>the type of that local var</returns>
        public string getLocalVar(string id)
        {
            return localVars[id];
        }

        public void addLocalVar(string id, string type)
        {
            localVars.Add(id, type);
        }

        /// <summary>
        /// Checks if a given type exists (ie a class)
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool isTypeValid(string type)
        {
            throw new NotImplementedException();
        }
    }
}
