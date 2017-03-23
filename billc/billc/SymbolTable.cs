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
        /// A list of all functions
        /// (Static as functions exist at global scope)
        /// </summary>
        static List<FunctionDecl> functions = new List<FunctionDecl>();

        /// <summary>
        /// A dictionary mapping class names to their class decl object
        /// (Static as class decls exist at global scope)
        /// </summary>
        static Dictionary<string, ClassDecl> classes = new Dictionary<string, ClassDecl>();

        static List<FunctionDecl> builtin_functions = new List<FunctionDecl>();

        public SymbolTable()
        { }

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

        /// <summary>
        /// Determines if a function exists with a given id
        /// </summary>
        /// <param name="id">the id to look for</param>
        /// <returns>true if at least one function with that id exists, false otherwise</returns>
        public static bool isLocalFunction(string id)
        {
            return functions.Any(f => f.id.id == id);
        }

        public static bool isFunction(FunctionDecl fd)
        {
            return functions.Contains(fd) || builtin_functions.Contains(fd);
        }


        public static bool isLocalFunction(FunctionDecl fd)
        {
            return functions.Contains(fd);
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

        /// <summary>
        /// Returns all functions that match a given id
        /// </summary>
        /// <param name="id">id to look for</param>
        /// <returns>all functions with given identifier</returns>
        public static List<FunctionDecl> getFunctions(string id)
        { 
            return functions.Where(f => f.id.id == id).ToList();
        }

        public static FunctionDecl getFunction(FunctionDecl fd)
        {
            return builtin_functions.FirstOrDefault(f => f.Equals(fd)) ?? functions.FirstOrDefault(f => f.Equals(fd));
        }

        public static FunctionDecl getBuiltinFunction(FunctionDecl fd)
        {
            return builtin_functions.First(f => f.Equals(fd));
        }

        public void addLocalVar(string id, string type)
        {
            localVars.Add(id, type);
        }

        public static void addFunction(FunctionDecl f)
        {
            functions.Add(f);
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

        public static bool isBuiltinFunction(string fid)
        {
            //var builtins = new List<string>{ "toInt", "println", "print", "toStr" };
            if (builtin_functions.Count == 0)
            {
                populateBuiltins();
            }
            return builtin_functions.Any(f => f.id.id == fid);
        }

        /// <summary>
        /// Call to populate static builtin functions
        /// </summary>
        public static void populateBuiltins()
        {
            var toStrIntParams = new List<FormalParam>();
            toStrIntParams.Add(new FormalParam(new Identifier(""), "int"));
            var toStrInt = new FunctionDecl(toStrIntParams, new Identifier("toStr"), "string", new List<Statement>());
            builtin_functions.Add(toStrInt);

            var printlnParams = new List<FormalParam>();
            printlnParams.Add(new FormalParam(new Identifier(""), "string"));
            var println = new FunctionDecl(printlnParams, new Identifier("println"), "void", new List<Statement>());
            builtin_functions.Add(println);

            var inputParams = new List<FormalParam>();
            var input = new FunctionDecl(inputParams, new Identifier("input"), "string", new List<Statement>());
            builtin_functions.Add(input);
        }
    }
}
