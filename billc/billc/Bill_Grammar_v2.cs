
using System;
using System.IO;
using System.Runtime.Serialization;
using com.calitha.goldparser.lalr;
using com.calitha.commons;
using billc;
using billc.TreeNodes;
using System.Collections.Generic;

namespace com.calitha.goldparser
{

    [Serializable()]
    public class SymbolException : System.Exception
    {
        public SymbolException(string message) : base(message)
        {
        }

        public SymbolException(string message,
            Exception inner) : base(message, inner)
        {
        }

        protected SymbolException(SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }

    }

    [Serializable()]
    public class RuleException : System.Exception
    {

        public RuleException(string message) : base(message)
        {
        }

        public RuleException(string message,
                             Exception inner) : base(message, inner)
        {
        }

        protected RuleException(SerializationInfo info,
                                StreamingContext context) : base(info, context)
        {
        }

    }

    enum SymbolConstants : int
    {
        SYMBOL_EOF = 0, // (EOF)
        SYMBOL_ERROR = 1, // (Error)
        SYMBOL_COMMENT = 2, // Comment
        SYMBOL_NEWLINE = 3, // NewLine
        SYMBOL_WHITESPACE = 4, // Whitespace
        SYMBOL_TIMESDIV = 5, // '*/'
        SYMBOL_DIVTIMES = 6, // '/*'
        SYMBOL_DIVDIV = 7, // '//'
        SYMBOL_MINUS = 8, // '-'
        SYMBOL_MINUSMINUS = 9, // '--'
        SYMBOL_EXCLAM = 10, // '!'
        SYMBOL_EXCLAMEQ = 11, // '!='
        SYMBOL_PERCENT = 12, // '%'
        SYMBOL_AMPAMP = 13, // '&&'
        SYMBOL_LPAREN = 14, // '('
        SYMBOL_RPAREN = 15, // ')'
        SYMBOL_TIMES = 16, // '*'
        SYMBOL_TIMESEQ = 17, // '*='
        SYMBOL_COMMA = 18, // ','
        SYMBOL_DIV = 19, // '/'
        SYMBOL_DIVEQ = 20, // '/='
        SYMBOL_COLON = 21, // ':'
        SYMBOL_SEMI = 22, // ';'
        SYMBOL_QUESTION = 23, // '?'
        SYMBOL_LBRACKET = 24, // '['
        SYMBOL_RBRACKET = 25, // ']'
        SYMBOL_LBRACE = 26, // '{'
        SYMBOL_PIPEPIPE = 27, // '||'
        SYMBOL_RBRACE = 28, // '}'
        SYMBOL_PLUS = 29, // '+'
        SYMBOL_PLUSPLUS = 30, // '++'
        SYMBOL_PLUSEQ = 31, // '+='
        SYMBOL_LT = 32, // '<'
        SYMBOL_LTEQ = 33, // '<='
        SYMBOL_EQ = 34, // '='
        SYMBOL_MINUSEQ = 35, // '-='
        SYMBOL_EQEQ = 36, // '=='
        SYMBOL_GT = 37, // '>'
        SYMBOL_GTEQ = 38, // '>='
        SYMBOL_BOOL = 39, // bool
        SYMBOL_BREAK = 40, // break
        SYMBOL_CHAR = 41, // char
        SYMBOL_CHARLITERAL = 42, // CharLiteral
        SYMBOL_CLASS = 43, // class
        SYMBOL_CONTINUE = 44, // continue
        SYMBOL_DECLITERAL = 45, // DecLiteral
        SYMBOL_DOUBLE = 46, // double
        SYMBOL_ELSE = 47, // else
        SYMBOL_FALSE = 48, // false
        SYMBOL_FOR = 49, // for
        SYMBOL_FOREACH = 50, // foreach
        SYMBOL_HEXLITERAL = 51, // HexLiteral
        SYMBOL_IDENTIFIER = 52, // Identifier
        SYMBOL_IF = 53, // if
        SYMBOL_IN = 54, // in
        SYMBOL_INT = 55, // int
        SYMBOL_LIST = 56, // List
        SYMBOL_MEMBERNAME = 57, // MemberName
        SYMBOL_NEW = 58, // new
        SYMBOL_NULL = 59, // null
        SYMBOL_REALLITERAL = 60, // RealLiteral
        SYMBOL_RETURN = 61, // return
        SYMBOL_STRING = 62, // String
        SYMBOL_STRINGLITERAL = 63, // StringLiteral
        SYMBOL_THIS = 64, // this
        SYMBOL_TRUE = 65, // true
        SYMBOL_VOID = 66, // void
        SYMBOL_WHILE = 67, // while
        SYMBOL_ADDEXP = 68, // <Add Exp>
        SYMBOL_ANDEXP = 69, // <And Exp>
        SYMBOL_ARGLIST = 70, // <Arg List>
        SYMBOL_ARGLISTOPT = 71, // <Arg List Opt>
        SYMBOL_ARGUMENT = 72, // <Argument>
        SYMBOL_ARRAYINITIALIZER = 73, // <Array Initializer>
        SYMBOL_ASSIGNTAIL = 74, // <Assign Tail>
        SYMBOL_BLOCK = 75, // <Block>
        SYMBOL_CLASSDECL = 76, // <Class Decl>
        SYMBOL_CLASSITEM = 77, // <Class Item>
        SYMBOL_CLASSITEMDECSOPT = 78, // <Class Item Decs Opt>
        SYMBOL_COMPAREEXP = 79, // <Compare Exp>
        SYMBOL_COMPILATIONUNIT = 80, // <Compilation Unit>
        SYMBOL_CONDITIONALEXP = 81, // <Conditional Exp>
        SYMBOL_EQUALITYEXP = 82, // <Equality Exp>
        SYMBOL_EXPRESSION = 83, // <Expression>
        SYMBOL_EXPRESSIONLIST = 84, // <Expression List>
        SYMBOL_EXPRESSIONOPT = 85, // <Expression Opt>
        SYMBOL_FORCONDITIONOPT = 86, // <For Condition Opt>
        SYMBOL_FORINITOPT = 87, // <For Init Opt>
        SYMBOL_FORITERATOROPT = 88, // <For Iterator Opt>
        SYMBOL_FORMALPARAM = 89, // <Formal Param>
        SYMBOL_FORMALPARAMLIST = 90, // <Formal Param List>
        SYMBOL_FORMALPARAMLISTOPT = 91, // <Formal Param List Opt>
        SYMBOL_LITERAL = 92, // <Literal>
        SYMBOL_LOCALVARDECL = 93, // <Local Var Decl>
        SYMBOL_MEMBERLIST = 94, // <Member List>
        SYMBOL_METHOD = 95, // <Method>
        SYMBOL_METHODDEC = 96, // <Method Dec>
        SYMBOL_METHODEXP = 97, // <Method Exp>
        SYMBOL_METHODSOPT = 98, // <Methods Opt>
        SYMBOL_MULTEXP = 99, // <Mult Exp>
        SYMBOL_NORMALSTM = 100, // <Normal Stm>
        SYMBOL_OBJECTEXP = 101, // <Object Exp>
        SYMBOL_OREXP = 102, // <Or Exp>
        SYMBOL_PRIMARY = 103, // <Primary>
        SYMBOL_PRIMARYEXP = 104, // <Primary Exp>
        SYMBOL_PROGRAMITEM = 105, // <Program Item>
        SYMBOL_PROGRAMITEMS = 106, // <Program Items>
        SYMBOL_QUALIFIEDID = 107, // <Qualified ID>
        SYMBOL_STATEMENT = 108, // <Statement>
        SYMBOL_STATEMENTEXP = 109, // <Statement Exp>
        SYMBOL_STATEMENTEXPLIST = 110, // <Statement Exp List>
        SYMBOL_STMLIST = 111, // <Stm List>
        SYMBOL_THENSTM = 112, // <Then Stm>
        SYMBOL_TYPE = 113, // <Type>
        SYMBOL_UNARYEXP = 114, // <Unary Exp>
        SYMBOL_VALIDID = 115, // <Valid ID>
        SYMBOL_VARIABLEDECLARATOR = 116, // <Variable Declarator>
        SYMBOL_VARIABLEDECS = 117, // <Variable Decs>
        SYMBOL_VARIABLEINITIALIZER = 118, // <Variable Initializer>
        SYMBOL_VARIABLEINITIALIZERLIST = 119, // <Variable Initializer List>
        SYMBOL_VARIABLEINITIALIZERLISTOPT = 120  // <Variable Initializer List Opt>
    };

    enum RuleConstants : int
    {
        RULE_VALIDID_IDENTIFIER = 0, // <Valid ID> ::= Identifier
        RULE_VALIDID_THIS = 1, // <Valid ID> ::= this
        RULE_VALIDID = 2, // <Valid ID> ::= <Type>
        RULE_QUALIFIEDID = 3, // <Qualified ID> ::= <Valid ID> <Member List>
        RULE_MEMBERLIST_MEMBERNAME = 4, // <Member List> ::= <Member List> MemberName
        RULE_MEMBERLIST = 5, // <Member List> ::= 
        RULE_LITERAL_TRUE = 6, // <Literal> ::= true
        RULE_LITERAL_FALSE = 7, // <Literal> ::= false
        RULE_LITERAL_DECLITERAL = 8, // <Literal> ::= DecLiteral
        RULE_LITERAL_HEXLITERAL = 9, // <Literal> ::= HexLiteral
        RULE_LITERAL_REALLITERAL = 10, // <Literal> ::= RealLiteral
        RULE_LITERAL_CHARLITERAL = 11, // <Literal> ::= CharLiteral
        RULE_LITERAL_STRINGLITERAL = 12, // <Literal> ::= StringLiteral
        RULE_LITERAL_NULL = 13, // <Literal> ::= null
        RULE_TYPE_INT = 14, // <Type> ::= int
        RULE_TYPE_DOUBLE = 15, // <Type> ::= double
        RULE_TYPE_CHAR = 16, // <Type> ::= char
        RULE_TYPE_STRING = 17, // <Type> ::= String
        RULE_TYPE_VOID = 18, // <Type> ::= void
        RULE_TYPE_BOOL = 19, // <Type> ::= bool
        RULE_TYPE_LIST_LT_GT = 20, // <Type> ::= List '<' <Qualified ID> '>'
        RULE_EXPRESSIONOPT = 21, // <Expression Opt> ::= <Expression>
        RULE_EXPRESSIONOPT2 = 22, // <Expression Opt> ::= 
        RULE_EXPRESSIONLIST = 23, // <Expression List> ::= <Expression>
        RULE_EXPRESSIONLIST_COMMA = 24, // <Expression List> ::= <Expression> ',' <Expression List>
        RULE_EXPRESSION_EQ = 25, // <Expression> ::= <Conditional Exp> '=' <Expression>
        RULE_EXPRESSION_PLUSEQ = 26, // <Expression> ::= <Conditional Exp> '+=' <Expression>
        RULE_EXPRESSION_MINUSEQ = 27, // <Expression> ::= <Conditional Exp> '-=' <Expression>
        RULE_EXPRESSION_TIMESEQ = 28, // <Expression> ::= <Conditional Exp> '*=' <Expression>
        RULE_EXPRESSION_DIVEQ = 29, // <Expression> ::= <Conditional Exp> '/=' <Expression>
        RULE_EXPRESSION = 30, // <Expression> ::= <Conditional Exp>
        RULE_CONDITIONALEXP_QUESTION_COLON = 31, // <Conditional Exp> ::= <Or Exp> '?' <Or Exp> ':' <Conditional Exp>
        RULE_CONDITIONALEXP = 32, // <Conditional Exp> ::= <Or Exp>
        RULE_OREXP_PIPEPIPE = 33, // <Or Exp> ::= <Or Exp> '||' <And Exp>
        RULE_OREXP = 34, // <Or Exp> ::= <And Exp>
        RULE_ANDEXP_AMPAMP = 35, // <And Exp> ::= <And Exp> '&&' <Equality Exp>
        RULE_ANDEXP = 36, // <And Exp> ::= <Equality Exp>
        RULE_EQUALITYEXP_EQEQ = 37, // <Equality Exp> ::= <Equality Exp> '==' <Compare Exp>
        RULE_EQUALITYEXP_EXCLAMEQ = 38, // <Equality Exp> ::= <Equality Exp> '!=' <Compare Exp>
        RULE_EQUALITYEXP = 39, // <Equality Exp> ::= <Compare Exp>
        RULE_COMPAREEXP_LT = 40, // <Compare Exp> ::= <Compare Exp> '<' <Add Exp>
        RULE_COMPAREEXP_GT = 41, // <Compare Exp> ::= <Compare Exp> '>' <Add Exp>
        RULE_COMPAREEXP_LTEQ = 42, // <Compare Exp> ::= <Compare Exp> '<=' <Add Exp>
        RULE_COMPAREEXP_GTEQ = 43, // <Compare Exp> ::= <Compare Exp> '>=' <Add Exp>
        RULE_COMPAREEXP = 44, // <Compare Exp> ::= <Add Exp>
        RULE_ADDEXP_PLUS = 45, // <Add Exp> ::= <Add Exp> '+' <Mult Exp>
        RULE_ADDEXP_MINUS = 46, // <Add Exp> ::= <Add Exp> '-' <Mult Exp>
        RULE_ADDEXP = 47, // <Add Exp> ::= <Mult Exp>
        RULE_MULTEXP_TIMES = 48, // <Mult Exp> ::= <Mult Exp> '*' <Unary Exp>
        RULE_MULTEXP_DIV = 49, // <Mult Exp> ::= <Mult Exp> '/' <Unary Exp>
        RULE_MULTEXP_PERCENT = 50, // <Mult Exp> ::= <Mult Exp> '%' <Unary Exp>
        RULE_MULTEXP = 51, // <Mult Exp> ::= <Unary Exp>
        RULE_UNARYEXP_EXCLAM = 52, // <Unary Exp> ::= '!' <Unary Exp>
        RULE_UNARYEXP_MINUS = 53, // <Unary Exp> ::= '-' <Unary Exp>
        RULE_UNARYEXP_PLUSPLUS = 54, // <Unary Exp> ::= '++' <Unary Exp>
        RULE_UNARYEXP_MINUSMINUS = 55, // <Unary Exp> ::= '--' <Unary Exp>
        RULE_UNARYEXP = 56, // <Unary Exp> ::= <Object Exp>
        RULE_OBJECTEXP = 57, // <Object Exp> ::= <Method Exp>
        RULE_METHODEXP = 58, // <Method Exp> ::= <Method Exp> <Method>
        RULE_METHODEXP2 = 59, // <Method Exp> ::= <Primary Exp>
        RULE_PRIMARYEXP_NEW_LPAREN_RPAREN = 60, // <Primary Exp> ::= new <Valid ID> '(' <Arg List Opt> ')'
        RULE_PRIMARYEXP = 61, // <Primary Exp> ::= <Primary>
        RULE_PRIMARYEXP_LPAREN_RPAREN = 62, // <Primary Exp> ::= '(' <Expression> ')'
        RULE_PRIMARY = 63, // <Primary> ::= <Valid ID>
        RULE_PRIMARY_LPAREN_RPAREN = 64, // <Primary> ::= <Valid ID> '(' <Arg List Opt> ')'
        RULE_PRIMARY2 = 65, // <Primary> ::= <Literal>
        RULE_ARGLISTOPT = 66, // <Arg List Opt> ::= <Arg List>
        RULE_ARGLISTOPT2 = 67, // <Arg List Opt> ::= 
        RULE_ARGLIST_COMMA = 68, // <Arg List> ::= <Arg List> ',' <Argument>
        RULE_ARGLIST = 69, // <Arg List> ::= <Argument>
        RULE_ARGUMENT = 70, // <Argument> ::= <Expression>
        RULE_STMLIST = 71, // <Stm List> ::= <Stm List> <Statement>
        RULE_STMLIST2 = 72, // <Stm List> ::= <Statement>
        RULE_STATEMENT_SEMI = 73, // <Statement> ::= <Local Var Decl> ';'
        RULE_STATEMENT_IF_LPAREN_RPAREN = 74, // <Statement> ::= if '(' <Expression> ')' <Statement>
        RULE_STATEMENT_IF_LPAREN_RPAREN_ELSE = 75, // <Statement> ::= if '(' <Expression> ')' <Then Stm> else <Statement>
        RULE_STATEMENT_FOR_LPAREN_SEMI_SEMI_RPAREN = 76, // <Statement> ::= for '(' <For Init Opt> ';' <For Condition Opt> ';' <For Iterator Opt> ')' <Statement>
        RULE_STATEMENT_FOREACH_LPAREN_IDENTIFIER_IN_RPAREN = 77, // <Statement> ::= foreach '(' <Valid ID> Identifier in <Expression> ')' <Statement>
        RULE_STATEMENT_WHILE_LPAREN_RPAREN = 78, // <Statement> ::= while '(' <Expression> ')' <Statement>
        RULE_STATEMENT = 79, // <Statement> ::= <Normal Stm>
        RULE_THENSTM_IF_LPAREN_RPAREN_ELSE = 80, // <Then Stm> ::= if '(' <Expression> ')' <Then Stm> else <Then Stm>
        RULE_THENSTM_FOR_LPAREN_SEMI_SEMI_RPAREN = 81, // <Then Stm> ::= for '(' <For Init Opt> ';' <For Condition Opt> ';' <For Iterator Opt> ')' <Then Stm>
        RULE_THENSTM_FOREACH_LPAREN_IDENTIFIER_IN_RPAREN = 82, // <Then Stm> ::= foreach '(' <Valid ID> Identifier in <Expression> ')' <Then Stm>
        RULE_THENSTM_WHILE_LPAREN_RPAREN = 83, // <Then Stm> ::= while '(' <Expression> ')' <Then Stm>
        RULE_THENSTM = 84, // <Then Stm> ::= <Normal Stm>
        RULE_NORMALSTM_BREAK_SEMI = 85, // <Normal Stm> ::= break ';'
        RULE_NORMALSTM_CONTINUE_SEMI = 86, // <Normal Stm> ::= continue ';'
        RULE_NORMALSTM_RETURN_SEMI = 87, // <Normal Stm> ::= return <Expression Opt> ';'
        RULE_NORMALSTM_SEMI = 88, // <Normal Stm> ::= <Statement Exp> ';'
        RULE_NORMALSTM_SEMI2 = 89, // <Normal Stm> ::= ';'
        RULE_NORMALSTM = 90, // <Normal Stm> ::= <Block>
        RULE_BLOCK_LBRACE_RBRACE = 91, // <Block> ::= '{' <Stm List> '}'
        RULE_BLOCK_LBRACE_RBRACE2 = 92, // <Block> ::= '{' '}'
        RULE_VARIABLEDECS = 93, // <Variable Decs> ::= <Variable Declarator>
        RULE_VARIABLEDECS_COMMA = 94, // <Variable Decs> ::= <Variable Decs> ',' <Variable Declarator>
        RULE_VARIABLEDECLARATOR_IDENTIFIER = 95, // <Variable Declarator> ::= Identifier
        RULE_VARIABLEDECLARATOR_IDENTIFIER_EQ = 96, // <Variable Declarator> ::= Identifier '=' <Variable Initializer>
        RULE_VARIABLEINITIALIZER = 97, // <Variable Initializer> ::= <Expression>
        RULE_VARIABLEINITIALIZER2 = 98, // <Variable Initializer> ::= <Array Initializer>
        RULE_ARRAYINITIALIZER_LBRACE_RBRACE = 99, // <Array Initializer> ::= '{' <Variable Initializer List Opt> '}'
        RULE_ARRAYINITIALIZER_LBRACE_COMMA_RBRACE = 100, // <Array Initializer> ::= '{' <Variable Initializer List> ',' '}'
        RULE_VARIABLEINITIALIZERLISTOPT = 101, // <Variable Initializer List Opt> ::= <Variable Initializer List>
        RULE_VARIABLEINITIALIZERLISTOPT2 = 102, // <Variable Initializer List Opt> ::= 
        RULE_VARIABLEINITIALIZERLIST = 103, // <Variable Initializer List> ::= <Variable Initializer>
        RULE_VARIABLEINITIALIZERLIST_COMMA = 104, // <Variable Initializer List> ::= <Variable Initializer List> ',' <Variable Initializer>
        RULE_FORINITOPT = 105, // <For Init Opt> ::= <Local Var Decl>
        RULE_FORINITOPT2 = 106, // <For Init Opt> ::= <Statement Exp List>
        RULE_FORINITOPT3 = 107, // <For Init Opt> ::= 
        RULE_FORITERATOROPT = 108, // <For Iterator Opt> ::= <Statement Exp List>
        RULE_FORITERATOROPT2 = 109, // <For Iterator Opt> ::= 
        RULE_FORCONDITIONOPT = 110, // <For Condition Opt> ::= <Expression>
        RULE_FORCONDITIONOPT2 = 111, // <For Condition Opt> ::= 
        RULE_STATEMENTEXPLIST_COMMA = 112, // <Statement Exp List> ::= <Statement Exp List> ',' <Statement Exp>
        RULE_STATEMENTEXPLIST = 113, // <Statement Exp List> ::= <Statement Exp>
        RULE_LOCALVARDECL = 114, // <Local Var Decl> ::= <Qualified ID> <Variable Decs>
        RULE_STATEMENTEXP_LPAREN_RPAREN = 115, // <Statement Exp> ::= <Qualified ID> '(' <Arg List Opt> ')'
        RULE_STATEMENTEXP_LPAREN_RPAREN2 = 116, // <Statement Exp> ::= <Qualified ID> '(' <Arg List Opt> ')' <Methods Opt> <Assign Tail>
        RULE_STATEMENTEXP_LBRACKET_RBRACKET = 117, // <Statement Exp> ::= <Qualified ID> '[' <Expression List> ']' <Methods Opt> <Assign Tail>
        RULE_STATEMENTEXP_PLUSPLUS = 118, // <Statement Exp> ::= <Qualified ID> '++' <Methods Opt> <Assign Tail>
        RULE_STATEMENTEXP_MINUSMINUS = 119, // <Statement Exp> ::= <Qualified ID> '--' <Methods Opt> <Assign Tail>
        RULE_STATEMENTEXP = 120, // <Statement Exp> ::= <Qualified ID> <Assign Tail>
        RULE_ASSIGNTAIL_PLUSPLUS = 121, // <Assign Tail> ::= '++'
        RULE_ASSIGNTAIL_MINUSMINUS = 122, // <Assign Tail> ::= '--'
        RULE_ASSIGNTAIL_EQ = 123, // <Assign Tail> ::= '=' <Expression>
        RULE_ASSIGNTAIL_PLUSEQ = 124, // <Assign Tail> ::= '+=' <Expression>
        RULE_ASSIGNTAIL_MINUSEQ = 125, // <Assign Tail> ::= '-=' <Expression>
        RULE_ASSIGNTAIL_TIMESEQ = 126, // <Assign Tail> ::= '*=' <Expression>
        RULE_ASSIGNTAIL_DIVEQ = 127, // <Assign Tail> ::= '/=' <Expression>
        RULE_METHODSOPT = 128, // <Methods Opt> ::= <Methods Opt> <Method>
        RULE_METHODSOPT2 = 129, // <Methods Opt> ::= 
        RULE_METHOD_MEMBERNAME = 130, // <Method> ::= MemberName
        RULE_METHOD_MEMBERNAME_LPAREN_RPAREN = 131, // <Method> ::= MemberName '(' <Arg List Opt> ')'
        RULE_METHOD_LBRACKET_RBRACKET = 132, // <Method> ::= '[' <Expression List> ']'
        RULE_METHOD_PLUSPLUS = 133, // <Method> ::= '++'
        RULE_METHOD_MINUSMINUS = 134, // <Method> ::= '--'
        RULE_COMPILATIONUNIT = 135, // <Compilation Unit> ::= <Program Items>
        RULE_PROGRAMITEMS = 136, // <Program Items> ::= <Program Items> <Program Item>
        RULE_PROGRAMITEMS2 = 137, // <Program Items> ::= 
        RULE_PROGRAMITEM = 138, // <Program Item> ::= <Method Dec>
        RULE_PROGRAMITEM2 = 139, // <Program Item> ::= <Class Decl>
        RULE_METHODDEC_IDENTIFIER_LPAREN_RPAREN = 140, // <Method Dec> ::= <Qualified ID> Identifier '(' <Formal Param List Opt> ')' <Block>
        RULE_FORMALPARAMLISTOPT = 141, // <Formal Param List Opt> ::= <Formal Param List>
        RULE_FORMALPARAMLISTOPT2 = 142, // <Formal Param List Opt> ::= 
        RULE_FORMALPARAMLIST = 143, // <Formal Param List> ::= <Formal Param>
        RULE_FORMALPARAMLIST_COMMA = 144, // <Formal Param List> ::= <Formal Param List> ',' <Formal Param>
        RULE_FORMALPARAM_IDENTIFIER = 145, // <Formal Param> ::= <Qualified ID> Identifier
        RULE_CLASSDECL_CLASS_IDENTIFIER_LPAREN_RPAREN_LBRACE_RBRACE = 146, // <Class Decl> ::= class Identifier '(' <Formal Param List Opt> ')' '{' <Class Item Decs Opt> '}'
        RULE_CLASSITEMDECSOPT = 147, // <Class Item Decs Opt> ::= <Class Item Decs Opt> <Class Item>
        RULE_CLASSITEMDECSOPT2 = 148, // <Class Item Decs Opt> ::= 
        RULE_CLASSITEM = 149  // <Class Item> ::= <Method Dec>
    };

    public class MyParser
    {
        private LALRParser parser;
        internal IErrorReporter errorReporter = new ErrorReporter();
        public bool badParse = false;
        /// <summary>
        /// For use in Assign Tail rules which need context to properly create an AST
        /// </summary>
        private Identifier local_ident;

        /// <summary>
        /// For use in MethodExp Method rules so the identifier from a rule above is available.
        /// </summary>
        private Identifier local_method_ident;

        public MyParser(string filename)
        {
            FileStream stream = new FileStream(filename,
                                               FileMode.Open,
                                               FileAccess.Read,
                                               FileShare.Read);
            Init(stream);
            stream.Close();
        }

        public MyParser(string baseName, string resourceName)
        {
            byte[] buffer = ResourceUtil.GetByteArrayResource(
                System.Reflection.Assembly.GetExecutingAssembly(),
                baseName,
                resourceName);
            MemoryStream stream = new MemoryStream(buffer);
            Init(stream);
            stream.Close();
        }

        public MyParser(Stream stream)
        {
            Init(stream);
        }

        private void Init(Stream stream)
        {
            CGTReader reader = new CGTReader(stream);
            parser = reader.CreateNewParser();
            parser.TrimReductions = false;
            parser.StoreTokens = LALRParser.StoreTokensMode.NoUserObject;

            parser.OnTokenError += new LALRParser.TokenErrorHandler(TokenErrorEvent);
            parser.OnParseError += new LALRParser.ParseErrorHandler(ParseErrorEvent);
        }

        public object Parse(string source)
        {
            NonterminalToken token = parser.Parse(source);
            if (token != null)
            {
                object obj = CreateObject(token);
                return obj;
            }
            return null;
        }

        private Object CreateObject(Token token)
        {
            if (token is TerminalToken)
                return CreateObjectFromTerminal((TerminalToken)token);
            else
                return CreateObjectFromNonterminal((NonterminalToken)token);
        }

        private Expression CreateExpression(Token token)
        {
            object o = CreateObject(token);
            if (o is Expression)
            {
                return (Expression)o;
            }
            else if (o is string)
            {
                errorReporter.Warning("Used the identifier constructor without line num");
                return new Identifier((string)o);
            }
            else if (o == null)
            {
                return null;
            }
            else
            {
                errorReporter.Error("Unexpected expression type in create expression: " + o.GetType());
                badParse = true;
                return null;
            }
        }

        private Object CreateObjectFromTerminal(TerminalToken token)
        {
            switch (token.Symbol.Id)
            {
                case (int)SymbolConstants.SYMBOL_EOF:
                    //(EOF)
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_ERROR:
                    //(Error)
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_COMMENT:
                    //Comment
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_NEWLINE:
                    //NewLine
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_WHITESPACE:
                    //Whitespace
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_TIMESDIV:
                    //'*/'
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_DIVTIMES:
                    //'/*'
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_DIVDIV:
                    //'//'
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_MINUS:
                    //'-'
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_MINUSMINUS:
                    //'--'
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_EXCLAM:
                    //'!'
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_EXCLAMEQ:
                    //'!='
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_PERCENT:
                    //'%'
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_AMPAMP:
                    //'&&'
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_LPAREN:
                    //'('
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_RPAREN:
                    //')'
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_TIMES:
                    //'*'
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_TIMESEQ:
                    //'*='
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_COMMA:
                    //','
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_DIV:
                    //'/'
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_DIVEQ:
                    //'/='
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_COLON:
                    //':'
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_SEMI:
                    //';'
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_QUESTION:
                    //'?'
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_LBRACKET:
                    //'['
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_RBRACKET:
                    //']'
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_LBRACE:
                    //'{'
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_PIPEPIPE:
                    //'||'
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_RBRACE:
                    //'}'
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_PLUS:
                    //'+'
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_PLUSPLUS:
                    //'++'
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_PLUSEQ:
                    //'+='
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_LT:
                    //'<'
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_LTEQ:
                    //'<='
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_EQ:
                    //'='
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_MINUSEQ:
                    //'-='
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_EQEQ:
                    //'=='
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_GT:
                    //'>'
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_GTEQ:
                    //'>='
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_BOOL:
                    //bool
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_BREAK:
                    //break
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_CHAR:
                    //char
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_CHARLITERAL:
                    //CharLiteral
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_CLASS:
                    //class
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_CONTINUE:
                    //continue
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_DECLITERAL:
                    //DecLiteral
                    var decLit = new Literal(int.Parse(token.Text));
                    decLit.lineNum = token.Location.LineNr;
                    return decLit;

                case (int)SymbolConstants.SYMBOL_DOUBLE:
                    //double
                    //todo: Create a new object that corresponds to the symbol
                    return "double";

                case (int)SymbolConstants.SYMBOL_ELSE:
                    //else
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_FALSE:
                    //false
                    var falseLit = new Literal(false);
                    falseLit.lineNum = token.Location.LineNr;
                    return falseLit;

                case (int)SymbolConstants.SYMBOL_FOR:
                    //for
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_FOREACH:
                    //foreach
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_HEXLITERAL:
                    //HexLiteral
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_IDENTIFIER:
                    //Identifier
                    var id = new Identifier(token.Text);
                    id.lineNum = token.Location.LineNr;
                    return id;

                case (int)SymbolConstants.SYMBOL_IF:
                    //if
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_IN:
                    //in
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_INT:
                    //int
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_LIST:
                    //List
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_MEMBERNAME:
                    //MemberName
                    //removes the dot at the beginning
                    return token.Text.Substring(1);

                case (int)SymbolConstants.SYMBOL_NEW:
                    //new
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_NULL:
                    //null
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_REALLITERAL:
                    //RealLiteral
                    var lit = new Literal(double.Parse(token.Text));
                    lit.lineNum = token.Location.LineNr;
                    return lit;

                case (int)SymbolConstants.SYMBOL_RETURN:
                    //return
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_STRING:
                    //String
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_STRINGLITERAL:
                    //StringLiteral
                    var lit2 = new Literal(token.Text.Substring(1, token.Text.Length - 2));
                    lit2.lineNum = token.Location.LineNr;
                    return lit2;

                case (int)SymbolConstants.SYMBOL_THIS:
                    //this
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_TRUE:
                    //true
                    var lit3 = new Literal(true);
                    lit3.lineNum = token.Location.LineNr;
                    return lit3;

                case (int)SymbolConstants.SYMBOL_VOID:
                    //void
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_WHILE:
                    //while
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_ADDEXP:
                    //<Add Exp>
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_ANDEXP:
                    //<And Exp>
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_ARGLIST:
                    //<Arg List>
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_ARGLISTOPT:
                    //<Arg List Opt>
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_ARGUMENT:
                    //<Argument>
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_ARRAYINITIALIZER:
                    //<Array Initializer>
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_ASSIGNTAIL:
                    //<Assign Tail>
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_BLOCK:
                    //<Block>
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_CLASSDECL:
                    //<Class Decl>
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_CLASSITEM:
                    //<Class Item>
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_CLASSITEMDECSOPT:
                    //<Class Item Decs Opt>
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_COMPAREEXP:
                    //<Compare Exp>
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_COMPILATIONUNIT:
                    //<Compilation Unit>
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_CONDITIONALEXP:
                    //<Conditional Exp>
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_EQUALITYEXP:
                    //<Equality Exp>
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_EXPRESSION:
                    //<Expression>
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_EXPRESSIONLIST:
                    //<Expression List>
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_EXPRESSIONOPT:
                    //<Expression Opt>
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_FORCONDITIONOPT:
                    //<For Condition Opt>
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_FORINITOPT:
                    //<For Init Opt>
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_FORITERATOROPT:
                    //<For Iterator Opt>
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_FORMALPARAM:
                    //<Formal Param>
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_FORMALPARAMLIST:
                    //<Formal Param List>
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_FORMALPARAMLISTOPT:
                    //<Formal Param List Opt>
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_LITERAL:
                    //<Literal>
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_LOCALVARDECL:
                    //<Local Var Decl>
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_MEMBERLIST:
                    //<Member List>
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_METHOD:
                    //<Method>
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_METHODDEC:
                    //<Method Dec>
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_METHODEXP:
                    //<Method Exp>
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_METHODSOPT:
                    //<Methods Opt>
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_MULTEXP:
                    //<Mult Exp>
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_NORMALSTM:
                    //<Normal Stm>
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_OBJECTEXP:
                    //<Object Exp>
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_OREXP:
                    //<Or Exp>
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_PRIMARY:
                    //<Primary>
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_PRIMARYEXP:
                    //<Primary Exp>
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_PROGRAMITEM:
                    //<Program Item>
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_PROGRAMITEMS:
                    //<Program Items>
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_QUALIFIEDID:
                    //<Qualified ID>
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_STATEMENT:
                    //<Statement>
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_STATEMENTEXP:
                    //<Statement Exp>
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_STATEMENTEXPLIST:
                    //<Statement Exp List>
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_STMLIST:
                    //<Stm List>
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_THENSTM:
                    //<Then Stm>
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_TYPE:
                    //<Type>
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_UNARYEXP:
                    //<Unary Exp>
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_VALIDID:
                    //<Valid ID>
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_VARIABLEDECLARATOR:
                    //<Variable Declarator>
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_VARIABLEDECS:
                    //<Variable Decs>
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_VARIABLEINITIALIZER:
                    //<Variable Initializer>
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_VARIABLEINITIALIZERLIST:
                    //<Variable Initializer List>
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_VARIABLEINITIALIZERLISTOPT:
                    //<Variable Initializer List Opt>
                    //todo: Create a new object that corresponds to the symbol
                    return null;

            }
            throw new SymbolException("Unknown symbol");
        }

        public Object CreateObjectFromNonterminal(NonterminalToken token)
        {
            switch (token.Rule.Id)
            {
                case (int)RuleConstants.RULE_VALIDID_IDENTIFIER:
                    //<Valid ID> ::= Identifier
                    return CreateObject(token.Tokens[0]);

                case (int)RuleConstants.RULE_VALIDID_THIS:
                    //<Valid ID> ::= this
                    return "this";

                case (int)RuleConstants.RULE_VALIDID:
                    //<Valid ID> ::= <Type>
                    return CreateObject(token.Tokens[0]);

                case (int)RuleConstants.RULE_QUALIFIEDID:
                    //<Qualified ID> ::= <Valid ID> <Member List>
                    Expression memb = CreateExpression(token.Tokens[1]);
                    if (memb == null)
                    {
                        return CreateObject(token.Tokens[0]);
                    }
                    else
                    {
                        Expression vid = CreateExpression(token.Tokens[0]);
                        return new BinaryOperator(vid, memb, binops.dot);
                    }

                case (int)RuleConstants.RULE_MEMBERLIST_MEMBERNAME:
                    //<Member List> ::= <Member List> MemberName
                    Expression memblst = CreateExpression(token.Tokens[0]);
                    Expression rhsmem = CreateExpression(token.Tokens[1]);
                    return memblst == null ? rhsmem : new BinaryOperator(memblst, rhsmem, binops.dot);

                case (int)RuleConstants.RULE_MEMBERLIST:
                    //<Member List> ::= 
                    return null;

                case (int)RuleConstants.RULE_LITERAL_TRUE:
                    //<Literal> ::= true
                    return CreateObject(token.Tokens[0]);

                case (int)RuleConstants.RULE_LITERAL_FALSE:
                    //<Literal> ::= false
                    return CreateObject(token.Tokens[0]);

                case (int)RuleConstants.RULE_LITERAL_DECLITERAL:
                    //<Literal> ::= DecLiteral
                    return CreateObject(token.Tokens[0]);

                case (int)RuleConstants.RULE_LITERAL_HEXLITERAL:
                    //<Literal> ::= HexLiteral
                    return CreateObject(token.Tokens[0]);

                case (int)RuleConstants.RULE_LITERAL_REALLITERAL:
                    //<Literal> ::= RealLiteral
                    return CreateObject(token.Tokens[0]);

                case (int)RuleConstants.RULE_LITERAL_CHARLITERAL:
                    //<Literal> ::= CharLiteral
                    return CreateObject(token.Tokens[0]);

                case (int)RuleConstants.RULE_LITERAL_STRINGLITERAL:
                    //<Literal> ::= StringLiteral
                    return CreateObject(token.Tokens[0]);

                case (int)RuleConstants.RULE_LITERAL_NULL:
                    //<Literal> ::= null
                    var nullLit = new Literal();
                    nullLit.lineNum = (token.Tokens[0] as TerminalToken).Location.LineNr;
                    return nullLit;

                case (int)RuleConstants.RULE_TYPE_INT:
                    //<Type> ::= int
                    return "int";

                case (int)RuleConstants.RULE_TYPE_DOUBLE:
                    //<Type> ::= double
                    return "double";

                case (int)RuleConstants.RULE_TYPE_CHAR:
                    //<Type> ::= char
                    return "char";

                case (int)RuleConstants.RULE_TYPE_STRING:
                    //<Type> ::= String
                    return "String";

                case (int)RuleConstants.RULE_TYPE_VOID:
                    //<Type> ::= void
                    return "void";

                case (int)RuleConstants.RULE_TYPE_BOOL:
                    //<Type> ::= bool
                    return "bool";

                case (int)RuleConstants.RULE_TYPE_LIST_LT_GT:
                    //<Type> ::= List '<' <Qualified ID> '>'
                    return "List<" + (string)CreateObject(token.Tokens[2]) + ">";

                case (int)RuleConstants.RULE_EXPRESSIONOPT:
                    //<Expression Opt> ::= <Expression>
                    return CreateObject(token.Tokens[0]);

                case (int)RuleConstants.RULE_EXPRESSIONOPT2:
                    //<Expression Opt> ::= 
                    return null;

                case (int)RuleConstants.RULE_EXPRESSIONLIST:
                    //<Expression List> ::= <Expression>
                    Expression e_elem = (Expression)CreateObject(token.Tokens[0]);
                    var elist = new List<Expression>();
                    elist.Add(e_elem);
                    return elist;

                case (int)RuleConstants.RULE_EXPRESSIONLIST_COMMA:
                    //<Expression List> ::= <Expression> ',' <Expression List>
                    Expression e_elem2 = (Expression)CreateObject(token.Tokens[0]);
                    var elist2 = (List<Expression>)CreateObject(token.Tokens[2]);
                    elist2.Add(e_elem2);
                    return elist2;

                case (int)RuleConstants.RULE_EXPRESSION_EQ:
                    //<Expression> ::= <Conditional Exp> '=' <Expression>
                    Identifier ida = (Identifier)CreateExpression(token.Tokens[0]);
                    Expression rhsa = CreateExpression(token.Tokens[2]);
                    var asin = new Assignment(ida, rhsa);
                    asin.lineNum = (token.Tokens[1] as TerminalToken).Location.LineNr;
                    return asin;

                case (int)RuleConstants.RULE_EXPRESSION_PLUSEQ:
                    //<Expression> ::= <Conditional Exp> '+=' <Expression>
                    //todo: Create a new object using the stored tokens.
                    return null;

                case (int)RuleConstants.RULE_EXPRESSION_MINUSEQ:
                    //<Expression> ::= <Conditional Exp> '-=' <Expression>
                    //todo: Create a new object using the stored tokens.
                    return null;

                case (int)RuleConstants.RULE_EXPRESSION_TIMESEQ:
                    //<Expression> ::= <Conditional Exp> '*=' <Expression>
                    //todo: Create a new object using the stored tokens.
                    return null;

                case (int)RuleConstants.RULE_EXPRESSION_DIVEQ:
                    //<Expression> ::= <Conditional Exp> '/=' <Expression>
                    //todo: Create a new object using the stored tokens.
                    return null;

                case (int)RuleConstants.RULE_EXPRESSION:
                    //<Expression> ::= <Conditional Exp>
                    return CreateObject(token.Tokens[0]);

                case (int)RuleConstants.RULE_CONDITIONALEXP_QUESTION_COLON:
                    //<Conditional Exp> ::= <Or Exp> '?' <Or Exp> ':' <Conditional Exp>
                    //todo: Create a new object using the stored tokens.
                    return null;

                case (int)RuleConstants.RULE_CONDITIONALEXP:
                    //<Conditional Exp> ::= <Or Exp>
                    return CreateObject(token.Tokens[0]);

                case (int)RuleConstants.RULE_OREXP_PIPEPIPE:
                    //<Or Exp> ::= <Or Exp> '||' <And Exp>
                    Expression lhs_or = CreateExpression(token.Tokens[0]);
                    Expression rhs_or = CreateExpression(token.Tokens[2]);
                    return new BinaryOperator(lhs_or, rhs_or, binops.or);

                case (int)RuleConstants.RULE_OREXP:
                    //<Or Exp> ::= <And Exp>
                    return CreateObject(token.Tokens[0]);

                case (int)RuleConstants.RULE_ANDEXP_AMPAMP:
                    //<And Exp> ::= <And Exp> '&&' <Equality Exp>
                    Expression lhs_and = CreateExpression(token.Tokens[0]);
                    Expression rhs_and = CreateExpression(token.Tokens[2]);
                    return new BinaryOperator(lhs_and, rhs_and, binops.and);

                case (int)RuleConstants.RULE_ANDEXP:
                    //<And Exp> ::= <Equality Exp>
                    return CreateObject(token.Tokens[0]);

                case (int)RuleConstants.RULE_EQUALITYEXP_EQEQ:
                    //<Equality Exp> ::= <Equality Exp> '==' <Compare Exp>
                    Expression lhs_eq = CreateExpression(token.Tokens[0]);
                    Expression rhs_eq = CreateExpression(token.Tokens[2]);
                    return new BinaryOperator(lhs_eq, rhs_eq, binops.eq);

                case (int)RuleConstants.RULE_EQUALITYEXP_EXCLAMEQ:
                    //<Equality Exp> ::= <Equality Exp> '!=' <Compare Exp>
                    Expression lhs_neq = CreateExpression(token.Tokens[0]);
                    Expression rhs_neq = CreateExpression(token.Tokens[2]);
                    return new BinaryOperator(lhs_neq, rhs_neq, binops.neq);

                case (int)RuleConstants.RULE_EQUALITYEXP:
                    //<Equality Exp> ::= <Compare Exp>
                    return CreateObject(token.Tokens[0]);

                case (int)RuleConstants.RULE_COMPAREEXP_LT:
                    //<Compare Exp> ::= <Compare Exp> '<' <Add Exp>
                    Expression lhs_lt = CreateExpression(token.Tokens[0]);
                    Expression rhs_lt = CreateExpression(token.Tokens[2]);
                    return new BinaryOperator(lhs_lt, rhs_lt, binops.lt);

                case (int)RuleConstants.RULE_COMPAREEXP_GT:
                    //<Compare Exp> ::= <Compare Exp> '>' <Add Exp>
                    Expression lhs_gt = CreateExpression(token.Tokens[0]);
                    Expression rhs_gt = CreateExpression(token.Tokens[2]);
                    return new BinaryOperator(lhs_gt, rhs_gt, binops.gt);

                case (int)RuleConstants.RULE_COMPAREEXP_LTEQ:
                    //<Compare Exp> ::= <Compare Exp> '<=' <Add Exp>
                    Expression lhs_lte = CreateExpression(token.Tokens[0]);
                    Expression rhs_lte = CreateExpression(token.Tokens[2]);
                    return new BinaryOperator(lhs_lte, rhs_lte, binops.lte);

                case (int)RuleConstants.RULE_COMPAREEXP_GTEQ:
                    //<Compare Exp> ::= <Compare Exp> '>=' <Add Exp>
                    Expression lhs_gte = CreateExpression(token.Tokens[0]);
                    Expression rhs_gte = CreateExpression(token.Tokens[2]);
                    return new BinaryOperator(lhs_gte, rhs_gte, binops.gte);

                case (int)RuleConstants.RULE_COMPAREEXP:
                    //<Compare Exp> ::= <Add Exp>
                    return CreateObject(token.Tokens[0]);

                case (int)RuleConstants.RULE_ADDEXP_PLUS:
                    //<Add Exp> ::= <Add Exp> '+' <Mult Exp>
                    Expression lhs_add = CreateExpression(token.Tokens[0]);
                    int lineNumadd = (token.Tokens[1] as TerminalToken).Location.LineNr;
                    Expression rhs_add = CreateExpression(token.Tokens[2]);
                    var bop = new BinaryOperator(lhs_add, rhs_add, binops.add);
                    bop.lineNum = lineNumadd;
                    return bop;

                case (int)RuleConstants.RULE_ADDEXP_MINUS:
                    //<Add Exp> ::= <Add Exp> '-' <Mult Exp>
                    Expression lhs_sub = CreateExpression(token.Tokens[0]);
                    int lineNumsub = (token.Tokens[1] as TerminalToken).Location.LineNr;
                    Expression rhs_sub = CreateExpression(token.Tokens[2]);
                    var bopsub = new BinaryOperator(lhs_sub, rhs_sub, binops.sub);
                    bopsub.lineNum = lineNumsub;
                    return bopsub;

                case (int)RuleConstants.RULE_ADDEXP:
                    //<Add Exp> ::= <Mult Exp>
                    return CreateObject(token.Tokens[0]);

                case (int)RuleConstants.RULE_MULTEXP_TIMES:
                    //<Mult Exp> ::= <Mult Exp> '*' <Unary Exp>
                    Expression lhs_mul = CreateExpression(token.Tokens[0]);
                    Expression rhs_mul = CreateExpression(token.Tokens[2]);
                    return new BinaryOperator(lhs_mul, rhs_mul, binops.mul);

                case (int)RuleConstants.RULE_MULTEXP_DIV:
                    //<Mult Exp> ::= <Mult Exp> '/' <Unary Exp>
                    Expression lhs_div = CreateExpression(token.Tokens[0]);
                    Expression rhs_div = CreateExpression(token.Tokens[2]);
                    return new BinaryOperator(lhs_div, rhs_div, binops.div);

                case (int)RuleConstants.RULE_MULTEXP_PERCENT:
                    //<Mult Exp> ::= <Mult Exp> '%' <Unary Exp>
                    Expression lhs_mod = CreateExpression(token.Tokens[0]);
                    Expression rhs_mod = CreateExpression(token.Tokens[2]);
                    return new BinaryOperator(lhs_mod, rhs_mod, binops.mod);

                case (int)RuleConstants.RULE_MULTEXP:
                    //<Mult Exp> ::= <Unary Exp>
                    return CreateObject(token.Tokens[0]);

                case (int)RuleConstants.RULE_UNARYEXP_EXCLAM:
                    //<Unary Exp> ::= '!' <Unary Exp>
                    Expression inner_not = CreateExpression(token.Tokens[1]);
                    var unop = new UnaryOperator(inner_not, unops.not);
                    unop.lineNum = (token.Tokens[0] as TerminalToken).Location.LineNr;
                    return unop;

                case (int)RuleConstants.RULE_UNARYEXP_MINUS:
                    //<Unary Exp> ::= '-' <Unary Exp>
                    Expression inner_minus = CreateExpression(token.Tokens[1]);
                    var unop2 = new UnaryOperator(inner_minus, unops.negate);
                    unop2.lineNum = (token.Tokens[0] as TerminalToken).Location.LineNr;
                    return unop2;

                case (int)RuleConstants.RULE_UNARYEXP_PLUSPLUS:
                    //<Unary Exp> ::= '++' <Unary Exp>
                    //todo: Create a new object using the stored tokens.
                    return null;

                case (int)RuleConstants.RULE_UNARYEXP_MINUSMINUS:
                    //<Unary Exp> ::= '--' <Unary Exp>
                    //todo: Create a new object using the stored tokens.
                    return null;

                case (int)RuleConstants.RULE_UNARYEXP:
                    //<Unary Exp> ::= <Object Exp>
                    return CreateObject(token.Tokens[0]);

                case (int)RuleConstants.RULE_OBJECTEXP:
                    //<Object Exp> ::= <Method Exp>
                    return CreateObject(token.Tokens[0]);

                case (int)RuleConstants.RULE_METHODEXP:
                    //<Method Exp> ::= <Method Exp> <Method>
                    Identifier otest = (Identifier) CreateObject(token.Tokens[0]);
                    local_method_ident = otest;
                    return CreateObject(token.Tokens[1]);

                case (int)RuleConstants.RULE_METHODEXP2:
                    //<Method Exp> ::= <Primary Exp>
                    return CreateObject(token.Tokens[0]);

                case (int)RuleConstants.RULE_PRIMARYEXP_NEW_LPAREN_RPAREN:
                    //<Primary Exp> ::= new <Valid ID> '(' <Arg List Opt> ')'
                    //todo: Create a new object using the stored tokens.
                    return null;

                case (int)RuleConstants.RULE_PRIMARYEXP:
                    //<Primary Exp> ::= <Primary>
                    return CreateObject(token.Tokens[0]);

                case (int)RuleConstants.RULE_PRIMARYEXP_LPAREN_RPAREN:
                    //<Primary Exp> ::= '(' <Expression> ')'
                    return CreateObject(token.Tokens[1]);

                case (int)RuleConstants.RULE_PRIMARY:
                    //<Primary> ::= <Valid ID>
                    return CreateObject(token.Tokens[0]);

                case (int)RuleConstants.RULE_PRIMARY_LPAREN_RPAREN:
                    //<Primary> ::= <Valid ID> '(' <Arg List Opt> ')'
                    Identifier fxnidf = (Identifier)CreateExpression(token.Tokens[0]);
                    List<Expression> concParamsf = (List<Expression>)CreateObject(token.Tokens[2]);
                    var inv = new FunctionInvocation(fxnidf, concParamsf);
                    inv.lineNum = (token.Tokens[1] as TerminalToken).Location.LineNr;
                    return inv;

                case (int)RuleConstants.RULE_PRIMARY2:
                    //<Primary> ::= <Literal>
                    return CreateObject(token.Tokens[0]);

                case (int)RuleConstants.RULE_ARGLISTOPT:
                    //<Arg List Opt> ::= <Arg List>
                    return CreateObject(token.Tokens[0]);

                case (int)RuleConstants.RULE_ARGLISTOPT2:
                    //<Arg List Opt> ::= 
                    return new List<Expression>();

                case (int)RuleConstants.RULE_ARGLIST_COMMA:
                    //<Arg List> ::= <Arg List> ',' <Argument>
                    List<Expression> arglist = (List<Expression>)CreateObject(token.Tokens[0]);
                    arglist.Add(CreateExpression(token.Tokens[2]));
                    return arglist;

                case (int)RuleConstants.RULE_ARGLIST:
                    //<Arg List> ::= <Argument>
                    List<Expression> argexp = new List<Expression>();
                    argexp.Add(CreateExpression(token.Tokens[0]));
                    return argexp;

                case (int)RuleConstants.RULE_ARGUMENT:
                    //<Argument> ::= <Expression>
                    return CreateObject(token.Tokens[0]);

                case (int)RuleConstants.RULE_STMLIST:
                    //<Stm List> ::= <Stm List> <Statement>
                    var more_stmts = (List<Statement>)CreateObject(token.Tokens[0]);
                    var stm = (Statement)CreateObject(token.Tokens[1]);
                    more_stmts.Add(stm);
                    return more_stmts;

                case (int)RuleConstants.RULE_STMLIST2:
                    //<Stm List> ::= <Statement>
                    var stmts = new List<Statement>();
                    stmts.Add((Statement)CreateObject(token.Tokens[0]));
                    return stmts;

                case (int)RuleConstants.RULE_STATEMENT_SEMI:
                    //<Statement> ::= <Local Var Decl> ';'
                    return CreateObject(token.Tokens[0]);

                case (int)RuleConstants.RULE_STATEMENT_IF_LPAREN_RPAREN:
                    //<Statement> ::= if '(' <Expression> ')' <Statement>
                    Expression cond = CreateExpression(token.Tokens[2]);
                    var tblock = (List<Statement>)CreateObject(token.Tokens[4]) ?? new List<Statement>();
                    int condline = (token.Tokens[0] as TerminalToken).Location.LineNr;
                    var condt = new Conditional(tblock, new List<Statement>(), cond);
                    condt.lineNum = condline;
                    return condt;

                case (int)RuleConstants.RULE_STATEMENT_IF_LPAREN_RPAREN_ELSE:
                    //<Statement> ::= if '(' <Expression> ')' <Then Stm> else <Statement>
                    Expression condelse = CreateExpression(token.Tokens[2]);
                    var teblock = (List<Statement>)CreateObject(token.Tokens[4]) ?? new List<Statement>();
                    var eblock = (List<Statement>)CreateObject(token.Tokens[6]) ?? new List<Statement>();
                    var tecond = new Conditional(teblock, eblock, condelse);
                    tecond.lineNum = (token.Tokens[0] as TerminalToken).Location.LineNr;
                    return tecond;

                case (int)RuleConstants.RULE_STATEMENT_FOR_LPAREN_SEMI_SEMI_RPAREN:
                    //<Statement> ::= for '(' <For Init Opt> ';' <For Condition Opt> ';' <For Iterator Opt> ')' <Statement>
                    var decls = (LocalVarDecl)CreateObject(token.Tokens[2]);
                    var forconds = (Expression)CreateObject(token.Tokens[4]);
                    var postloops = (List<Statement>)CreateObject(token.Tokens[6]);
                    var floopbodys = (List<Statement>)CreateObject(token.Tokens[8]) ?? new List<Statement>();
                    var floops = new ForLoop(floopbodys, decls, forconds, postloops);
                    floops.lineNum = (token.Tokens[0] as TerminalToken).Location.LineNr;
                    return floops;

                case (int)RuleConstants.RULE_STATEMENT_FOREACH_LPAREN_IDENTIFIER_IN_RPAREN:
                    //<Statement> ::= foreach '(' <Valid ID> Identifier in <Expression> ')' <Statement>
                    //todo: Create a new object using the stored tokens.
                    return null;

                case (int)RuleConstants.RULE_STATEMENT_WHILE_LPAREN_RPAREN:
                    //<Statement> ::= while '(' <Expression> ')' <Statement>
                    Expression condwhile = CreateExpression(token.Tokens[2]);
                    List<Statement> bodywhile = (List<Statement>)CreateObject(token.Tokens[4]); //todo: determine if this is okay to do
                    var wloop = new WhileLoop(bodywhile, condwhile);
                    wloop.lineNum = (token.Tokens[0] as TerminalToken).Location.LineNr;
                    return wloop;

                case (int)RuleConstants.RULE_STATEMENT:
                    //<Statement> ::= <Normal Stm>
                    return CreateObject(token.Tokens[0]);

                case (int)RuleConstants.RULE_THENSTM_IF_LPAREN_RPAREN_ELSE:
                    //<Then Stm> ::= if '(' <Expression> ')' <Then Stm> else <Then Stm>
                    Expression condelset = CreateExpression(token.Tokens[2]);
                    var teblockt = (List<Statement>)CreateObject(token.Tokens[4]) ?? new List<Statement>();
                    var eblockt = (List<Statement>)CreateObject(token.Tokens[6]) ?? new List<Statement>();
                    var tecondt = new Conditional(teblockt, eblockt, condelset);
                    tecondt.lineNum = (token.Tokens[0] as TerminalToken).Location.LineNr;
                    return tecondt;

                case (int)RuleConstants.RULE_THENSTM_FOR_LPAREN_SEMI_SEMI_RPAREN:
                    //<Then Stm> ::= for '(' <For Init Opt> ';' <For Condition Opt> ';' <For Iterator Opt> ')' <Then Stm>
                    var decl = (LocalVarDecl)CreateObject(token.Tokens[2]);
                    var forcond = (Expression)CreateObject(token.Tokens[4]);
                    var postloop = (List<Statement>)CreateObject(token.Tokens[6]);
                    var floopbody = (List<Statement>)CreateObject(token.Tokens[8]) ?? new List<Statement>();
                    var floop = new ForLoop(floopbody, decl, forcond, postloop);
                    floop.lineNum = (token.Tokens[0] as TerminalToken).Location.LineNr;
                    return floop;

                case (int)RuleConstants.RULE_THENSTM_FOREACH_LPAREN_IDENTIFIER_IN_RPAREN:
                    //<Then Stm> ::= foreach '(' <Valid ID> Identifier in <Expression> ')' <Then Stm>
                    //todo: Create a new object using the stored tokens.
                    return null;

                case (int)RuleConstants.RULE_THENSTM_WHILE_LPAREN_RPAREN:
                    //<Then Stm> ::= while '(' <Expression> ')' <Then Stm>
                    Expression condwhilet = CreateExpression(token.Tokens[2]);
                    List<Statement> bodywhilet = (List<Statement>)CreateObject(token.Tokens[4]); //todo: determine if this is okay to do
                    var wloop2 = new WhileLoop(bodywhilet, condwhilet);
                    wloop2.lineNum = (token.Tokens[0] as TerminalToken).Location.LineNr;
                    return wloop2;

                case (int)RuleConstants.RULE_THENSTM:
                    //<Then Stm> ::= <Normal Stm>
                    return CreateObject(token.Tokens[0]);

                case (int)RuleConstants.RULE_NORMALSTM_BREAK_SEMI:
                    //<Normal Stm> ::= break ';'
                    var br = new Break();
                    br.lineNum = (token.Tokens[0] as TerminalToken).Location.LineNr;
                    return br;

                case (int)RuleConstants.RULE_NORMALSTM_CONTINUE_SEMI:
                    //<Normal Stm> ::= continue ';'
                    var ct = new Continue();
                    ct.lineNum = (token.Tokens[0] as TerminalToken).Location.LineNr;
                    return ct;

                case (int)RuleConstants.RULE_NORMALSTM_RETURN_SEMI:
                    //<Normal Stm> ::= return <Expression Opt> ';'
                    Expression exp = CreateExpression(token.Tokens[1]);
                    var ret = new Return(exp);
                    ret.lineNum = (token.Tokens[0] as TerminalToken).Location.LineNr;
                    return ret;

                case (int)RuleConstants.RULE_NORMALSTM_SEMI:
                    //<Normal Stm> ::= <Statement Exp> ';'
                    return CreateObject(token.Tokens[0]);

                case (int)RuleConstants.RULE_NORMALSTM_SEMI2:
                    //<Normal Stm> ::= ';'
                    //todo: Create a new object using the stored tokens.
                    return null;

                case (int)RuleConstants.RULE_NORMALSTM:
                    //<Normal Stm> ::= <Block>
                    return CreateObject(token.Tokens[0]);

                case (int)RuleConstants.RULE_BLOCK_LBRACE_RBRACE:
                    //<Block> ::= '{' <Stm List> '}'
                    return CreateObject(token.Tokens[1]);

                case (int)RuleConstants.RULE_BLOCK_LBRACE_RBRACE2:
                    //<Block> ::= '{' '}'
                    //todo: Create a new object using the stored tokens.
                    return null;

                case (int)RuleConstants.RULE_VARIABLEDECS:
                    //<Variable Decs> ::= <Variable Declarator>
                    return CreateObject(token.Tokens[0]);

                case (int)RuleConstants.RULE_VARIABLEDECS_COMMA:
                    //<Variable Decs> ::= <Variable Decs> ',' <Variable Declarator>
                    //todo: Create a new object using the stored tokens.
                    return null;

                case (int)RuleConstants.RULE_VARIABLEDECLARATOR_IDENTIFIER:
                    //<Variable Declarator> ::= Identifier
                    Identifier ideclnu = (Identifier)CreateObject(token.Tokens[0]);
                    var inNull = new Literal();
                    inNull.lineNum = ideclnu.lineNum;
                    var newdecl = new LocalVarDecl(ideclnu, inNull);
                    newdecl.lineNum = ideclnu.lineNum;
                    return newdecl;

                case (int)RuleConstants.RULE_VARIABLEDECLARATOR_IDENTIFIER_EQ:
                    //<Variable Declarator> ::= Identifier '=' <Variable Initializer>
                    Identifier idecl = (Identifier)CreateObject(token.Tokens[0]);
                    Expression rdecl = CreateExpression(token.Tokens[2]);
                    var bigdecl = new LocalVarDecl(idecl, rdecl);
                    bigdecl.lineNum = (token.Tokens[1] as TerminalToken).Location.LineNr;
                    return bigdecl;

                case (int)RuleConstants.RULE_VARIABLEINITIALIZER:
                    //<Variable Initializer> ::= <Expression>
                    return CreateObject(token.Tokens[0]);

                case (int)RuleConstants.RULE_VARIABLEINITIALIZER2:
                    //<Variable Initializer> ::= <Array Initializer>
                    return CreateObject(token.Tokens[0]);

                case (int)RuleConstants.RULE_ARRAYINITIALIZER_LBRACE_RBRACE:
                    //<Array Initializer> ::= '{' <Variable Initializer List Opt> '}'
                    //todo: Create a new object using the stored tokens.
                    return null;

                case (int)RuleConstants.RULE_ARRAYINITIALIZER_LBRACE_COMMA_RBRACE:
                    //<Array Initializer> ::= '{' <Variable Initializer List> ',' '}'
                    //todo: Create a new object using the stored tokens.
                    return null;

                case (int)RuleConstants.RULE_VARIABLEINITIALIZERLISTOPT:
                    //<Variable Initializer List Opt> ::= <Variable Initializer List>
                    //todo: Create a new object using the stored tokens.
                    return null;

                case (int)RuleConstants.RULE_VARIABLEINITIALIZERLISTOPT2:
                    //<Variable Initializer List Opt> ::= 
                    //todo: Create a new object using the stored tokens.
                    return null;

                case (int)RuleConstants.RULE_VARIABLEINITIALIZERLIST:
                    //<Variable Initializer List> ::= <Variable Initializer>
                    //todo: Create a new object using the stored tokens.
                    return null;

                case (int)RuleConstants.RULE_VARIABLEINITIALIZERLIST_COMMA:
                    //<Variable Initializer List> ::= <Variable Initializer List> ',' <Variable Initializer>
                    //todo: Create a new object using the stored tokens.
                    return null;

                case (int)RuleConstants.RULE_FORINITOPT:
                    //<For Init Opt> ::= <Local Var Decl>
                    return CreateObject(token.Tokens[0]);

                case (int)RuleConstants.RULE_FORINITOPT2:
                    //<For Init Opt> ::= <Statement Exp List>
                    //todo: Create a new object using the stored tokens.
                    return null;

                case (int)RuleConstants.RULE_FORINITOPT3:
                    //<For Init Opt> ::= 
                    return null;

                case (int)RuleConstants.RULE_FORITERATOROPT:
                    //<For Iterator Opt> ::= <Statement Exp List>
                    return CreateObject(token.Tokens[0]);

                case (int)RuleConstants.RULE_FORITERATOROPT2:
                    //<For Iterator Opt> ::= 
                    return null;

                case (int)RuleConstants.RULE_FORCONDITIONOPT:
                    //<For Condition Opt> ::= <Expression>
                    return CreateObject(token.Tokens[0]);

                case (int)RuleConstants.RULE_FORCONDITIONOPT2:
                    //<For Condition Opt> ::= 
                    return null;

                case (int)RuleConstants.RULE_STATEMENTEXPLIST_COMMA:
                    //<Statement Exp List> ::= <Statement Exp List> ',' <Statement Exp>
                    var stmtlst = (List<Statement>)CreateObject(token.Tokens[0]);
                    Statement stmtexp = (Statement)CreateObject(token.Tokens[2]);
                    stmtlst.Add(stmtexp);
                    return stmtlst;

                case (int)RuleConstants.RULE_STATEMENTEXPLIST:
                    //<Statement Exp List> ::= <Statement Exp>
                    var stmtexplst = new List<Statement>();
                    Statement sexp = (Statement)CreateObject(token.Tokens[0]);
                    stmtexplst.Add(sexp);
                    return stmtexplst;

                case (int)RuleConstants.RULE_LOCALVARDECL:
                    //<Local Var Decl> ::= <Qualified ID> <Variable Decs>
                    string typeDecl = (string)CreateObject(token.Tokens[0]);
                    LocalVarDecl declNoType = (LocalVarDecl)CreateObject(token.Tokens[1]);
                    declNoType.addType(typeDecl);
                    return declNoType;

                case (int)RuleConstants.RULE_STATEMENTEXP_LPAREN_RPAREN:
                    //<Statement Exp> ::= <Qualified ID> '(' <Arg List Opt> ')'
                    Identifier fxnid = (Identifier)CreateExpression(token.Tokens[0]);
                    List<Expression> concParams = (List<Expression>)CreateObject(token.Tokens[2]);
                    var inv2 = new FunctionInvocation(fxnid, concParams);
                    inv2.lineNum = (token.Tokens[1] as TerminalToken).Location.LineNr;
                    return inv2;

                case (int)RuleConstants.RULE_STATEMENTEXP_LPAREN_RPAREN2:
                    //<Statement Exp> ::= <Qualified ID> '(' <Arg List Opt> ')' <Methods Opt> <Assign Tail>
                    Identifier fxnid2 = (Identifier)CreateExpression(token.Tokens[0]);
                    List<Expression> concParams2 = (List<Expression>)CreateObject(token.Tokens[2]);
                    object o = CreateObject(token.Tokens[4]);
                    return null;

                case (int)RuleConstants.RULE_STATEMENTEXP_LBRACKET_RBRACKET:
                    //<Statement Exp> ::= <Qualified ID> '[' <Expression List> ']' <Methods Opt> <Assign Tail>
                    //todo: Create a new object using the stored tokens.
                    return null;

                case (int)RuleConstants.RULE_STATEMENTEXP_PLUSPLUS:
                    //<Statement Exp> ::= <Qualified ID> '++' <Methods Opt> <Assign Tail>
                    //todo: Create a new object using the stored tokens.
                    return null;

                case (int)RuleConstants.RULE_STATEMENTEXP_MINUSMINUS:
                    //<Statement Exp> ::= <Qualified ID> '--' <Methods Opt> <Assign Tail>
                    //todo: Create a new object using the stored tokens.
                    return null;

                case (int)RuleConstants.RULE_STATEMENTEXP:
                    //<Statement Exp> ::= <Qualified ID> <Assign Tail>
                    Identifier id = (Identifier)CreateExpression(token.Tokens[0]);
                    local_ident = id; //Set context for use in <Assign Tail>
                    Statement rhsexp = (Statement)CreateObject(token.Tokens[1]);
                    return rhsexp;

                case (int)RuleConstants.RULE_ASSIGNTAIL_PLUSPLUS:
                    //<Assign Tail> ::= '++'
                    //todo: Create a new object using the stored tokens.
                    return null;

                case (int)RuleConstants.RULE_ASSIGNTAIL_MINUSMINUS:
                    //<Assign Tail> ::= '--'
                    //todo: Create a new object using the stored tokens.
                    return null;

                case (int)RuleConstants.RULE_ASSIGNTAIL_EQ:
                    //<Assign Tail> ::= '=' <Expression>
                    var rhs = CreateExpression(token.Tokens[1]);
                    var assignExp = new Assignment(local_ident, rhs);
                    assignExp.lineNum = (token.Tokens[0] as TerminalToken).Location.LineNr;
                    return assignExp;

                case (int)RuleConstants.RULE_ASSIGNTAIL_PLUSEQ:
                    //<Assign Tail> ::= '+=' <Expression>
                    var rhses = CreateExpression(token.Tokens[1]);
                    var realrhs = new BinaryOperator(local_ident, rhses, binops.add);
                    var assignplusexp = new Assignment(local_ident, realrhs);
                    assignplusexp.lineNum = (token.Tokens[0] as TerminalToken).Location.LineNr;
                    return assignplusexp;

                case (int)RuleConstants.RULE_ASSIGNTAIL_MINUSEQ:
                    //<Assign Tail> ::= '-=' <Expression>
                    var rhsesm = CreateExpression(token.Tokens[1]);
                    var realrhsm = new BinaryOperator(local_ident, rhsesm, binops.sub);
                    var assignplusexpm = new Assignment(local_ident, realrhsm);
                    assignplusexpm.lineNum = (token.Tokens[0] as TerminalToken).Location.LineNr;
                    return assignplusexpm;

                case (int)RuleConstants.RULE_ASSIGNTAIL_TIMESEQ:
                    //<Assign Tail> ::= '*=' <Expression>
                    var rhsest = CreateExpression(token.Tokens[1]);
                    var realrhst = new BinaryOperator(local_ident, rhsest, binops.mul);
                    var assignplusexpt = new Assignment(local_ident, realrhst);
                    assignplusexpt.lineNum = (token.Tokens[0] as TerminalToken).Location.LineNr;
                    return assignplusexpt;

                case (int)RuleConstants.RULE_ASSIGNTAIL_DIVEQ:
                    //<Assign Tail> ::= '/=' <Expression>
                    var rhsesd = CreateExpression(token.Tokens[1]);
                    var realrhsd = new BinaryOperator(local_ident, rhsesd, binops.div);
                    var assignplusexpd = new Assignment(local_ident, realrhsd);
                    assignplusexpd.lineNum = (token.Tokens[0] as TerminalToken).Location.LineNr;
                    return assignplusexpd;

                case (int)RuleConstants.RULE_METHODSOPT:
                    //<Methods Opt> ::= <Methods Opt> <Method>
                    //todo: Create a new object using the stored tokens.
                    return null;

                case (int)RuleConstants.RULE_METHODSOPT2:
                    //<Methods Opt> ::= 
                    //todo: Create a new object using the stored tokens.
                    return null;

                case (int)RuleConstants.RULE_METHOD_MEMBERNAME:
                    //<Method> ::= MemberName
                    return CreateObject(token.Tokens[0]);

                case (int)RuleConstants.RULE_METHOD_MEMBERNAME_LPAREN_RPAREN:
                    //<Method> ::= MemberName '(' <Arg List Opt> ')'
                    return null;

                case (int)RuleConstants.RULE_METHOD_LBRACKET_RBRACKET:
                    //<Method> ::= '[' <Expression List> ']'
                    var expressions = (List<Expression>)CreateObject(token.Tokens[1]);
                    //We don't want to support comma seperated access yet so just take the first element only.
                    if (local_method_ident == null)
                    {
                        errorReporter.Fatal("Local_method_ident was not set for index operation");
                        badParse = true;
                        return null;
                    }
                    var indexop = new IndexOperation(local_method_ident, expressions[0]);
                    indexop.lineNum = (token.Tokens[0] as TerminalToken).Location.LineNr;
                    return indexop;

                case (int)RuleConstants.RULE_METHOD_PLUSPLUS:
                    //<Method> ::= '++'
                    //todo: Create a new object using the stored tokens.
                    return null;

                case (int)RuleConstants.RULE_METHOD_MINUSMINUS:
                    //<Method> ::= '--'
                    //todo: Create a new object using the stored tokens.
                    return null;

                case (int)RuleConstants.RULE_COMPILATIONUNIT:
                    //<Compilation Unit> ::= <Program Items>
                    return CreateObject(token.Tokens[0]);

                case (int)RuleConstants.RULE_PROGRAMITEMS:
                    //<Program Items> ::= <Program Items> <Program Item>
                    ProgramNode prgrm = (ProgramNode)CreateObject(token.Tokens[0]);
                    object oitem = CreateObject(token.Tokens[1]);
                    if (oitem is FunctionDecl)
                    {
                        prgrm.Add((FunctionDecl)oitem);
                    }
                    else if (oitem is ClassDecl)
                    {
                        prgrm.Add((ClassDecl)oitem);
                    }
                    else
                    {
                        errorReporter.Fatal("Unexpected program item type in parser!");
                    }
                    return prgrm;

                case (int)RuleConstants.RULE_PROGRAMITEMS2:
                    //<Program Items> ::= 
                    return new ProgramNode(new List<FunctionDecl>(), new List<ClassDecl>());

                case (int)RuleConstants.RULE_PROGRAMITEM:
                    //<Program Item> ::= <Method Dec>
                    return CreateObject(token.Tokens[0]);

                case (int)RuleConstants.RULE_PROGRAMITEM2:
                    //<Program Item> ::= <Class Decl>
                    return CreateObject(token.Tokens[0]);

                case (int)RuleConstants.RULE_METHODDEC_IDENTIFIER_LPAREN_RPAREN:
                    //<Method Dec> ::= <Qualified ID> Identifier '(' <Formal Param List Opt> ')' <Block>
                    string retType = (string)CreateObject(token.Tokens[0]);
                    Identifier func_id = (Identifier)CreateObject(token.Tokens[1]);
                    var fParams = (List<FormalParam>)CreateObject(token.Tokens[3]) ?? new List<FormalParam>();
                    var block = (List<Statement>)CreateObject(token.Tokens[5]) ?? new List<Statement>();
                    return new FunctionDecl(fParams, func_id, retType, block);

                case (int)RuleConstants.RULE_FORMALPARAMLISTOPT:
                    //<Formal Param List Opt> ::= <Formal Param List>
                    return CreateObject(token.Tokens[0]);

                case (int)RuleConstants.RULE_FORMALPARAMLISTOPT2:
                    //<Formal Param List Opt> ::= 
                    return new List<FormalParam>();

                case (int)RuleConstants.RULE_FORMALPARAMLIST:
                    //<Formal Param List> ::= <Formal Param>
                    FormalParam fparam = (FormalParam)CreateObject(token.Tokens[0]);
                    var fparamlst = new List<FormalParam>();
                    fparamlst.Add(fparam);
                    return fparamlst;

                case (int)RuleConstants.RULE_FORMALPARAMLIST_COMMA:
                    //<Formal Param List> ::= <Formal Param List> ',' <Formal Param>
                    List<FormalParam> fparamsublist = (List<FormalParam>)CreateObject(token.Tokens[0]);
                    FormalParam fparamsub = (FormalParam)CreateObject(token.Tokens[2]);
                    fparamsublist.Add(fparamsub);
                    return fparamsublist;

                case (int)RuleConstants.RULE_FORMALPARAM_IDENTIFIER:
                    //<Formal Param> ::= <Qualified ID> Identifier
                    string fparamtype = (string)CreateObject(token.Tokens[0]);
                    Identifier fparamid = (Identifier)CreateObject(token.Tokens[1]);
                    return new FormalParam(fparamid, fparamtype);

                case (int)RuleConstants.RULE_CLASSDECL_CLASS_IDENTIFIER_LPAREN_RPAREN_LBRACE_RBRACE:
                    //<Class Decl> ::= class Identifier '(' <Formal Param List Opt> ')' '{' <Class Item Decs Opt> '}'
                    Identifier idclass = CreateExpression(token.Tokens[1]) as Identifier;
                    List<FormalParam> classparams = (List<FormalParam>)CreateObject(token.Tokens[3]);
                    return new ClassDecl(idclass, classparams);

                case (int)RuleConstants.RULE_CLASSITEMDECSOPT:
                    //<Class Item Decs Opt> ::= <Class Item Decs Opt> <Class Item>
                    //todo: Create a new object using the stored tokens.
                    return null;

                case (int)RuleConstants.RULE_CLASSITEMDECSOPT2:
                    //<Class Item Decs Opt> ::= 
                    //todo: Create a new object using the stored tokens.
                    return null;

                case (int)RuleConstants.RULE_CLASSITEM:
                    //<Class Item> ::= <Method Dec>
                    //todo: Create a new object using the stored tokens.
                    return null;

            }
            throw new RuleException("Unknown rule");
        }

        private void TokenErrorEvent(LALRParser parser, TokenErrorEventArgs args)
        {
            string message = "Token error with input: '" + args.Token.ToString() + "'";
            errorReporter.Error(message);
        }

        private void ParseErrorEvent(LALRParser parser, ParseErrorEventArgs args)
        {
            string message = "Parse error caused by unexpected symbol: '" + args.UnexpectedToken.ToString() + "'";
            if (args.UnexpectedToken != null)
            {
                message += "\n\t at " + args.UnexpectedToken.Location.ToString();
            }
            errorReporter.Error(message);
        }

    }
}
