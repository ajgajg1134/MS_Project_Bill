namespace billc.TreeNodes
{
    class PrimitiveTypes
    {
        public static string[] primitiveTypes = { "int", "double", "char", "bool", "null" };
        
        public static bool isNumberType(string type)
        {
            if (type == "int" || type == "double")
                return true;
            return false;
        }

        /// <summary>
        /// Checks if a given string is a valid primitive type
        /// </summary>
        /// <param name="type">the string to check</param>
        /// <returns>true if it is a primitive type, false otherwise</returns>
        public static bool isPrimitiveType(string type)
        {
            foreach (string s in primitiveTypes)
            {
                if (s == type.ToLower())
                {
                    return true;
                }
            }
            return false;
        }
    }
}
