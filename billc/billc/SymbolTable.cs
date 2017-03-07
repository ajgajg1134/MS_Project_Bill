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

        /// <summary>
        /// A dictionary mapping function names to their function decl object
        /// (Static as functions exist at global scope)
        /// </summary>
        static Dictionary<string, FunctionDecl> functions = new Dictionary<string, FunctionDecl>();

        /// <summary>
        /// A dictionary mapping class names to their class decl object
        /// (Static as class decls exist at global scope)
        /// </summary>
        static Dictionary<string, ClassDecl> classes = new Dictionary<string, ClassDecl>();

        public SymbolTable()
        {

        }

        public SymbolTable(SymbolTable table)
        {
            localVars = new Dictionary<string, string>(table.localVars);
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

        public static bool isFunction(string id)
        {
            return functions.ContainsKey(id);
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

        public static FunctionDecl getFunction(string id)
        {
            return functions[id];
        }

        public void addLocalVar(string id, string type)
        {
            localVars.Add(id, type);
        }

        public static void addFunction(string id, FunctionDecl f)
        {
            functions.Add(id, f);
        }

        public static void addClass(string id, ClassDecl cd)
        {
            classes.Add(id, cd);
        }

        /// <summary>
        /// Checks if a given type exists (ie a class, or primitive type)
        /// </summary>
        /// <param name="type">string to check</param>
        /// <returns>true if the type exists</returns>
        public static bool isTypeValid(string type)
        {
            if (PrimitiveTypes.isPrimitiveType(type))
            {
                return true;
            }
            else
            {
                return classes.ContainsKey(type);
            }
        }
    }
}
