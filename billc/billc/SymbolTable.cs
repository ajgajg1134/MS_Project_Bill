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
        internal static List<FunctionDecl> functions = new List<FunctionDecl>();

        /// <summary>
        /// A dictionary mapping class names to their class decl object
        /// (Static as class decls exist at global scope)
        /// </summary>
        internal static Dictionary<string, ClassDecl> classes = new Dictionary<string, ClassDecl>();

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

        public static bool isConstructor(string id)
        {
            if (id.Substring(id.Length - 4) == ".new")
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Checks if a given classdecl already exists in the symbol table
        /// </summary>
        /// <param name="cd">The class to add</param>
        /// <returns>true if it exist already, false otherwise</returns>
        public static bool isClass(ClassDecl cd)
        {
            return classes.ContainsKey(cd.id.id);
        }

        public static bool isClass(string s)
        {
            return classes.ContainsKey(s);
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
            var toStrInt = new FunctionDecl(toStrIntParams, new Identifier("toStr"), "String", new List<Statement>());
            builtin_functions.Add(toStrInt);

            var toStrDblParams = new List<FormalParam>();
            toStrDblParams.Add(new FormalParam(new Identifier(""), "double"));
            var toStrDbl = new FunctionDecl(toStrDblParams, new Identifier("toStr"), "String", new List<Statement>());
            builtin_functions.Add(toStrDbl);

            var toStrCharParams = new List<FormalParam>();
            toStrCharParams.Add(new FormalParam(new Identifier(""), "char"));
            var toStrChar = new FunctionDecl(toStrCharParams, new Identifier("toStr"), "String", new List<Statement>());
            builtin_functions.Add(toStrChar);

            var printlnParams = new List<FormalParam>();
            printlnParams.Add(new FormalParam(new Identifier(""), "String"));
            var println = new FunctionDecl(printlnParams, new Identifier("println"), "void", new List<Statement>());
            builtin_functions.Add(println);

            var inputParams = new List<FormalParam>();
            var input = new FunctionDecl(inputParams, new Identifier("input"), "String", new List<Statement>());
            builtin_functions.Add(input);

            var toIntParams = new List<FormalParam>();
            toIntParams.Add(new FormalParam(new Identifier(""), "String"));
            var toInt = new FunctionDecl(toIntParams, new Identifier("toInt"), "int", new List<Statement>());
            builtin_functions.Add(toInt);

            var toDblParams = new List<FormalParam>();
            toDblParams.Add(new FormalParam(new Identifier(""), "String"));
            var toDbl = new FunctionDecl(toDblParams, new Identifier("toDouble"), "double", new List<Statement>());
            builtin_functions.Add(toDbl);

            var lengthParams = new List<FormalParam>();
            lengthParams.Add(new FormalParam(new Identifier(""), "String"));
            var length = new FunctionDecl(lengthParams, new Identifier("length"), "int", new List<Statement>());
            builtin_functions.Add(length);

            var buildListInt = new List<FormalParam>();
            var intList = new FunctionDecl(buildListInt, new Identifier("List<int>.new"), "List<int>", new List<Statement>());
            builtin_functions.Add(intList);

            var buildListDbl = new List<FormalParam>();
            var dblList = new FunctionDecl(buildListDbl, new Identifier("List<double>.new"), "List<double>", new List<Statement>());
            builtin_functions.Add(dblList);

            string[] arrayableTypes = { "int", "double", "char", "bool" };
            foreach(string s in arrayableTypes)
            {
                addTypeFunctions(s);
            }
        }

        /// <summary>
        /// Add pre-built functions to the symbol table for a type with name s
        /// </summary>
        /// <param name="s">the name of the type</param>
        public static void addTypeFunctions(string s)
        {
            //Todo: refactor some functions into using this format

            //Add a list<s>.size() function for this type
            var listSizeParams = new List<FormalParam>();
            listSizeParams.Add(new FormalParam(new Identifier(""), "List<" + s + ">"));
            var listSize = new FunctionDecl(listSizeParams, new Identifier("List<" + s + ">.size"), "int", new List<Statement>());
            builtin_functions.Add(listSize);
        }
    }
}
