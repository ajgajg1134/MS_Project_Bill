namespace billc.TreeNodes
{
    class PrimitiveTypes
    {
        public static string[] primitiveTypes = { "int", "double", "char", "bool" };
        
        public static bool isNumberType(string type)
        {
            if (type == "int" || type == "double")
                return true;
            return false;
        }
    }
}
