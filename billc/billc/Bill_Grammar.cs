
using System;
using System.IO;
using System.Runtime.Serialization;
using com.calitha.goldparser.lalr;
using com.calitha.commons;
using System.Collections.Generic;
using billc.TreeNodes;

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
        RULE_METHOD_PLUSPLUS = 132, // <Method> ::= '++'
        RULE_METHOD_MINUSMINUS = 133, // <Method> ::= '--'
        RULE_COMPILATIONUNIT = 134, // <Compilation Unit> ::= <Program Items>
        RULE_PROGRAMITEMS = 135, // <Program Items> ::= <Program Items> <Program Item>
        RULE_PROGRAMITEMS2 = 136, // <Program Items> ::= 
        RULE_PROGRAMITEM = 137, // <Program Item> ::= <Method Dec>
        RULE_PROGRAMITEM2 = 138, // <Program Item> ::= <Class Decl>
        RULE_METHODDEC_IDENTIFIER_LPAREN_RPAREN = 139, // <Method Dec> ::= <Qualified ID> Identifier '(' <Formal Param List Opt> ')' <Block>
        RULE_FORMALPARAMLISTOPT = 140, // <Formal Param List Opt> ::= <Formal Param List>
        RULE_FORMALPARAMLISTOPT2 = 141, // <Formal Param List Opt> ::= 
        RULE_FORMALPARAMLIST = 142, // <Formal Param List> ::= <Formal Param>
        RULE_FORMALPARAMLIST_COMMA = 143, // <Formal Param List> ::= <Formal Param List> ',' <Formal Param>
        RULE_FORMALPARAM_IDENTIFIER = 144, // <Formal Param> ::= <Qualified ID> Identifier
        RULE_CLASSDECL_CLASS_IDENTIFIER_LPAREN_RPAREN_LBRACE_RBRACE = 145, // <Class Decl> ::= class Identifier '(' <Formal Param List Opt> ')' '{' <Class Item Decs Opt> '}'
        RULE_CLASSITEMDECSOPT = 146, // <Class Item Decs Opt> ::= <Class Item Decs Opt> <Class Item>
        RULE_CLASSITEMDECSOPT2 = 147, // <Class Item Decs Opt> ::= 
        RULE_CLASSITEM = 148  // <Class Item> ::= <Method Dec>
    };

    public class MyParser
    {
        private LALRParser parser;

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
                Object obj = CreateObject(token);
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
                    //todo: Create a new object that corresponds to the symbol
                    return new Literal(int.Parse(token.Text));

                case (int)SymbolConstants.SYMBOL_DOUBLE:
                    //double
                    //todo: Create a new object that corresponds to the symbol
                    return new Literal(double.Parse(token.Text));

                case (int)SymbolConstants.SYMBOL_ELSE:
                    //else
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_FALSE:
                    //false
                    //todo: Create a new object that corresponds to the symbol
                    return null;

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
                    //todo: Create a new object that corresponds to the symbol
                    return token.Text;

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
                    //todo: Create a new object that corresponds to the symbol
                    return null;

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
                    //todo: Create a new object that corresponds to the symbol
                    return null;

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
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_THIS:
                    //this
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_TRUE:
                    //true
                    //todo: Create a new object that corresponds to the symbol
                    return null;

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
                    //todo: Create a new object using the stored tokens.
                    return CreateObject(token.Tokens[0]);

                case (int)RuleConstants.RULE_VALIDID_THIS:
                    //<Valid ID> ::= this
                    //todo: Create a new object using the stored tokens.
                    return "this";

                case (int)RuleConstants.RULE_VALIDID:
                    //<Valid ID> ::= <Type>
                    //todo: Create a new object using the stored tokens.
                    return CreateObject(token.Tokens[0]);

                case (int)RuleConstants.RULE_QUALIFIEDID:
                    //<Qualified ID> ::= <Valid ID> <Member List>
                    //todo: Create a new object using the stored tokens.
                    string s = (string)CreateObject(token.Tokens[0]) + (string)CreateObject(token.Tokens[1]);
                    return s;

                case (int)RuleConstants.RULE_MEMBERLIST_MEMBERNAME:
                    //<Member List> ::= <Member List> MemberName
                    //todo: Create a new object using the stored tokens.
                    return null;

                case (int)RuleConstants.RULE_MEMBERLIST:
                    //<Member List> ::= 
                    //todo: Create a new object using the stored tokens.
                    return "";

                case (int)RuleConstants.RULE_LITERAL_TRUE:
                    //<Literal> ::= true
                    //todo: Create a new object using the stored tokens.
                    return new Literal(true);

                case (int)RuleConstants.RULE_LITERAL_FALSE:
                    //<Literal> ::= false
                    //todo: Create a new object using the stored tokens.
                    return new Literal(false);

                case (int)RuleConstants.RULE_LITERAL_DECLITERAL:
                    //<Literal> ::= DecLiteral
                    //todo: Create a new object using the stored tokens.
                    return CreateObject(token.Tokens[0]);

                case (int)RuleConstants.RULE_LITERAL_HEXLITERAL:
                    //<Literal> ::= HexLiteral
                    //todo: Create a new object using the stored tokens.
                    return null;

                case (int)RuleConstants.RULE_LITERAL_REALLITERAL:
                    //<Literal> ::= RealLiteral
                    //todo: Create a new object using the stored tokens.
                    return null;

                case (int)RuleConstants.RULE_LITERAL_CHARLITERAL:
                    //<Literal> ::= CharLiteral
                    //todo: Create a new object using the stored tokens.
                    return null;

                case (int)RuleConstants.RULE_LITERAL_STRINGLITERAL:
                    //<Literal> ::= StringLiteral
                    //todo: Create a new object using the stored tokens.
                    return null;

                case (int)RuleConstants.RULE_LITERAL_NULL:
                    //<Literal> ::= null
                    //todo: Create a new object using the stored tokens.
                    return null;

                case (int)RuleConstants.RULE_TYPE_INT:
                    //<Type> ::= int
                    //todo: Create a new object using the stored tokens.
                    return "int";

                case (int)RuleConstants.RULE_TYPE_DOUBLE:
                    //<Type> ::= double
                    //todo: Create a new object using the stored tokens.
                    return "double";

                case (int)RuleConstants.RULE_TYPE_CHAR:
                    //<Type> ::= char
                    //todo: Create a new object using the stored tokens.
                    return "char";

                case (int)RuleConstants.RULE_TYPE_STRING:
                    //<Type> ::= String
                    //todo: Create a new object using the stored tokens.
                    return "String";

                case (int)RuleConstants.RULE_TYPE_VOID:
                    //<Type> ::= void
                    //todo: Create a new object using the stored tokens.
                    return "void";

                case (int)RuleConstants.RULE_TYPE_BOOL:
                    //<Type> ::= bool
                    //todo: Create a new object using the stored tokens.
                    return "bool";

                case (int)RuleConstants.RULE_TYPE_LIST_LT_GT:
                    //<Type> ::= List '<' <Qualified ID> '>'
                    //todo: Create a new object using the stored tokens.
                    return null;

                case (int)RuleConstants.RULE_EXPRESSIONOPT:
                    //<Expression Opt> ::= <Expression>
                    //todo: Create a new object using the stored tokens.
                    return null;

                case (int)RuleConstants.RULE_EXPRESSIONOPT2:
                    //<Expression Opt> ::= 
                    //todo: Create a new object using the stored tokens.
                    return null;

                case (int)RuleConstants.RULE_EXPRESSIONLIST:
                    //<Expression List> ::= <Expression>
                    //todo: Create a new object using the stored tokens.
                    return null;

                case (int)RuleConstants.RULE_EXPRESSIONLIST_COMMA:
                    //<Expression List> ::= <Expression> ',' <Expression List>
                    //todo: Create a new object using the stored tokens.
                    return null;

                case (int)RuleConstants.RULE_EXPRESSION_EQ:
                    //<Expression> ::= <Conditional Exp> '=' <Expression>
                    //todo: Create a new object using the stored tokens.
                    return null;

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
                    //todo: Create a new object using the stored tokens.
                    return CreateObject(token.Tokens[0]);

                case (int)RuleConstants.RULE_CONDITIONALEXP_QUESTION_COLON:
                    //<Conditional Exp> ::= <Or Exp> '?' <Or Exp> ':' <Conditional Exp>
                    //todo: Create a new object using the stored tokens.
                    return null;

                case (int)RuleConstants.RULE_CONDITIONALEXP:
                    //<Conditional Exp> ::= <Or Exp>
                    //todo: Create a new object using the stored tokens.
                    return CreateObject(token.Tokens[0]);

                case (int)RuleConstants.RULE_OREXP_PIPEPIPE:
                    //<Or Exp> ::= <Or Exp> '||' <And Exp>
                    //todo: Create a new object using the stored tokens.
                    return null;

                case (int)RuleConstants.RULE_OREXP:
                    //<Or Exp> ::= <And Exp>
                    //todo: Create a new object using the stored tokens.
                    return CreateObject(token.Tokens[0]);

                case (int)RuleConstants.RULE_ANDEXP_AMPAMP:
                    //<And Exp> ::= <And Exp> '&&' <Equality Exp>
                    //todo: Create a new object using the stored tokens.
                    return null;

                case (int)RuleConstants.RULE_ANDEXP:
                    //<And Exp> ::= <Equality Exp>
                    //todo: Create a new object using the stored tokens.
                    return CreateObject(token.Tokens[0]);

                case (int)RuleConstants.RULE_EQUALITYEXP_EQEQ:
                    //<Equality Exp> ::= <Equality Exp> '==' <Compare Exp>
                    //todo: Create a new object using the stored tokens.
                    return null;

                case (int)RuleConstants.RULE_EQUALITYEXP_EXCLAMEQ:
                    //<Equality Exp> ::= <Equality Exp> '!=' <Compare Exp>
                    //todo: Create a new object using the stored tokens.
                    return null;

                case (int)RuleConstants.RULE_EQUALITYEXP:
                    //<Equality Exp> ::= <Compare Exp>
                    //todo: Create a new object using the stored tokens.
                    return CreateObject(token.Tokens[0]);

                case (int)RuleConstants.RULE_COMPAREEXP_LT:
                    //<Compare Exp> ::= <Compare Exp> '<' <Add Exp>
                    //todo: Create a new object using the stored tokens.
                    return null;

                case (int)RuleConstants.RULE_COMPAREEXP_GT:
                    //<Compare Exp> ::= <Compare Exp> '>' <Add Exp>
                    //todo: Create a new object using the stored tokens.
                    return null;

                case (int)RuleConstants.RULE_COMPAREEXP_LTEQ:
                    //<Compare Exp> ::= <Compare Exp> '<=' <Add Exp>
                    //todo: Create a new object using the stored tokens.
                    return null;

                case (int)RuleConstants.RULE_COMPAREEXP_GTEQ:
                    //<Compare Exp> ::= <Compare Exp> '>=' <Add Exp>
                    //todo: Create a new object using the stored tokens.
                    return null;

                case (int)RuleConstants.RULE_COMPAREEXP:
                    //<Compare Exp> ::= <Add Exp>
                    //todo: Create a new object using the stored tokens.
                    return CreateObject(token.Tokens[0]);

                case (int)RuleConstants.RULE_ADDEXP_PLUS:
                    //<Add Exp> ::= <Add Exp> '+' <Mult Exp>
                    //todo: Create a new object using the stored tokens.
                    Expression lhs_add = (Expression)CreateObject(token.Tokens[0]);
                    Expression rhs_add = (Expression)CreateObject(token.Tokens[2]);
                    return new BinaryOperator(lhs_add, rhs_add, binops.add);

                case (int)RuleConstants.RULE_ADDEXP_MINUS:
                    //<Add Exp> ::= <Add Exp> '-' <Mult Exp>
                    //todo: Create a new object using the stored tokens.
                    return null;

                case (int)RuleConstants.RULE_ADDEXP:
                    //<Add Exp> ::= <Mult Exp>
                    //todo: Create a new object using the stored tokens.
                    return CreateObject(token.Tokens[0]);

                case (int)RuleConstants.RULE_MULTEXP_TIMES:
                    //<Mult Exp> ::= <Mult Exp> '*' <Unary Exp>
                    //todo: Create a new object using the stored tokens.
                    return null;

                case (int)RuleConstants.RULE_MULTEXP_DIV:
                    //<Mult Exp> ::= <Mult Exp> '/' <Unary Exp>
                    //todo: Create a new object using the stored tokens.
                    return null;

                case (int)RuleConstants.RULE_MULTEXP_PERCENT:
                    //<Mult Exp> ::= <Mult Exp> '%' <Unary Exp>
                    //todo: Create a new object using the stored tokens.
                    return null;

                case (int)RuleConstants.RULE_MULTEXP:
                    //<Mult Exp> ::= <Unary Exp>
                    //todo: Create a new object using the stored tokens.
                    return CreateObject(token.Tokens[0]);

                case (int)RuleConstants.RULE_UNARYEXP_EXCLAM:
                    //<Unary Exp> ::= '!' <Unary Exp>
                    //todo: Create a new object using the stored tokens.
                    return null;

                case (int)RuleConstants.RULE_UNARYEXP_MINUS:
                    //<Unary Exp> ::= '-' <Unary Exp>
                    //todo: Create a new object using the stored tokens.
                    return null;

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
                    //todo: Create a new object using the stored tokens.
                    return CreateObject(token.Tokens[0]);

                case (int)RuleConstants.RULE_OBJECTEXP:
                    //<Object Exp> ::= <Method Exp>
                    //todo: Create a new object using the stored tokens.
                    return CreateObject(token.Tokens[0]);

                case (int)RuleConstants.RULE_METHODEXP:
                    //<Method Exp> ::= <Method Exp> <Method>
                    //todo: Create a new object using the stored tokens.
                    return null;

                case (int)RuleConstants.RULE_METHODEXP2:
                    //<Method Exp> ::= <Primary Exp>
                    //todo: Create a new object using the stored tokens.
                    return CreateObject(token.Tokens[0]);

                case (int)RuleConstants.RULE_PRIMARYEXP_NEW_LPAREN_RPAREN:
                    //<Primary Exp> ::= new <Valid ID> '(' <Arg List Opt> ')'
                    //todo: Create a new object using the stored tokens.
                    return null;

                case (int)RuleConstants.RULE_PRIMARYEXP:
                    //<Primary Exp> ::= <Primary>
                    //todo: Create a new object using the stored tokens.
                    return CreateObject(token.Tokens[0]);

                case (int)RuleConstants.RULE_PRIMARYEXP_LPAREN_RPAREN:
                    //<Primary Exp> ::= '(' <Expression> ')'
                    //todo: Create a new object using the stored tokens.
                    return CreateObject(token.Tokens[1]);

                case (int)RuleConstants.RULE_PRIMARY:
                    //<Primary> ::= <Valid ID>
                    //todo: Create a new object using the stored tokens.
                    return CreateObject(token.Tokens[0]);

                case (int)RuleConstants.RULE_PRIMARY_LPAREN_RPAREN:
                    //<Primary> ::= <Valid ID> '(' <Arg List Opt> ')'
                    //todo: Create a new object using the stored tokens.
                    return null;

                case (int)RuleConstants.RULE_PRIMARY2:
                    //<Primary> ::= <Literal>
                    //todo: Create a new object using the stored tokens.
                    return CreateObject(token.Tokens[0]);

                case (int)RuleConstants.RULE_ARGLISTOPT:
                    //<Arg List Opt> ::= <Arg List>
                    //todo: Create a new object using the stored tokens.
                    return null;

                case (int)RuleConstants.RULE_ARGLISTOPT2:
                    //<Arg List Opt> ::= 
                    //todo: Create a new object using the stored tokens.
                    return null;

                case (int)RuleConstants.RULE_ARGLIST_COMMA:
                    //<Arg List> ::= <Arg List> ',' <Argument>
                    //todo: Create a new object using the stored tokens.
                    return null;

                case (int)RuleConstants.RULE_ARGLIST:
                    //<Arg List> ::= <Argument>
                    //todo: Create a new object using the stored tokens.
                    return CreateObject(token.Tokens[0]);

                case (int)RuleConstants.RULE_ARGUMENT:
                    //<Argument> ::= <Expression>
                    //todo: Create a new object using the stored tokens.
                    return CreateObject(token.Tokens[0]);

                case (int)RuleConstants.RULE_STMLIST:
                    //<Stm List> ::= <Stm List> <Statement>
                    //todo: Create a new object using the stored tokens.

                    return null;

                case (int)RuleConstants.RULE_STMLIST2:
                    //<Stm List> ::= <Statement>
                    //todo: Create a new object using the stored tokens.
                    var stmts = new List<Statement>();
                    stmts.Add((Statement)CreateObject(token.Tokens[0]));
                    return stmts;

                case (int)RuleConstants.RULE_STATEMENT_SEMI:
                    //<Statement> ::= <Local Var Decl> ';'
                    //todo: Create a new object using the stored tokens.
                    return null;

                case (int)RuleConstants.RULE_STATEMENT_IF_LPAREN_RPAREN:
                    //<Statement> ::= if '(' <Expression> ')' <Statement>
                    //todo: Create a new object using the stored tokens.
                    return null;

                case (int)RuleConstants.RULE_STATEMENT_IF_LPAREN_RPAREN_ELSE:
                    //<Statement> ::= if '(' <Expression> ')' <Then Stm> else <Statement>
                    //todo: Create a new object using the stored tokens.
                    return null;

                case (int)RuleConstants.RULE_STATEMENT_FOR_LPAREN_SEMI_SEMI_RPAREN:
                    //<Statement> ::= for '(' <For Init Opt> ';' <For Condition Opt> ';' <For Iterator Opt> ')' <Statement>
                    //todo: Create a new object using the stored tokens.
                    return null;

                case (int)RuleConstants.RULE_STATEMENT_FOREACH_LPAREN_IDENTIFIER_IN_RPAREN:
                    //<Statement> ::= foreach '(' <Valid ID> Identifier in <Expression> ')' <Statement>
                    //todo: Create a new object using the stored tokens.
                    return null;

                case (int)RuleConstants.RULE_STATEMENT_WHILE_LPAREN_RPAREN:
                    //<Statement> ::= while '(' <Expression> ')' <Statement>
                    //todo: Create a new object using the stored tokens.
                    return null;

                case (int)RuleConstants.RULE_STATEMENT:
                    //<Statement> ::= <Normal Stm>
                    //todo: Create a new object using the stored tokens.
                    return CreateObject(token.Tokens[0]);

                case (int)RuleConstants.RULE_THENSTM_IF_LPAREN_RPAREN_ELSE:
                    //<Then Stm> ::= if '(' <Expression> ')' <Then Stm> else <Then Stm>
                    //todo: Create a new object using the stored tokens.
                    return null;

                case (int)RuleConstants.RULE_THENSTM_FOR_LPAREN_SEMI_SEMI_RPAREN:
                    //<Then Stm> ::= for '(' <For Init Opt> ';' <For Condition Opt> ';' <For Iterator Opt> ')' <Then Stm>
                    //todo: Create a new object using the stored tokens.
                    return null;

                case (int)RuleConstants.RULE_THENSTM_FOREACH_LPAREN_IDENTIFIER_IN_RPAREN:
                    //<Then Stm> ::= foreach '(' <Valid ID> Identifier in <Expression> ')' <Then Stm>
                    //todo: Create a new object using the stored tokens.
                    return null;

                case (int)RuleConstants.RULE_THENSTM_WHILE_LPAREN_RPAREN:
                    //<Then Stm> ::= while '(' <Expression> ')' <Then Stm>
                    //todo: Create a new object using the stored tokens.
                    return null;

                case (int)RuleConstants.RULE_THENSTM:
                    //<Then Stm> ::= <Normal Stm>
                    //todo: Create a new object using the stored tokens.
                    return null;

                case (int)RuleConstants.RULE_NORMALSTM_BREAK_SEMI:
                    //<Normal Stm> ::= break ';'
                    //todo: Create a new object using the stored tokens.
                    return null;

                case (int)RuleConstants.RULE_NORMALSTM_CONTINUE_SEMI:
                    //<Normal Stm> ::= continue ';'
                    //todo: Create a new object using the stored tokens.
                    return null;

                case (int)RuleConstants.RULE_NORMALSTM_RETURN_SEMI:
                    //<Normal Stm> ::= return <Expression Opt> ';'
                    //todo: Create a new object using the stored tokens.
                    return null;

                case (int)RuleConstants.RULE_NORMALSTM_SEMI:
                    //<Normal Stm> ::= <Statement Exp> ';'
                    //todo: Create a new object using the stored tokens.
                    return CreateObject(token.Tokens[0]);

                case (int)RuleConstants.RULE_NORMALSTM_SEMI2:
                    //<Normal Stm> ::= ';'
                    //todo: Create a new object using the stored tokens.
                    return null;

                case (int)RuleConstants.RULE_NORMALSTM:
                    //<Normal Stm> ::= <Block>
                    //todo: Create a new object using the stored tokens.
                    return null;

                case (int)RuleConstants.RULE_BLOCK_LBRACE_RBRACE:
                    //<Block> ::= '{' <Stm List> '}'
                    //todo: Create a new object using the stored tokens.
                    return CreateObject(token.Tokens[1]);

                case (int)RuleConstants.RULE_BLOCK_LBRACE_RBRACE2:
                    //<Block> ::= '{' '}'
                    //todo: Create a new object using the stored tokens.
                    return null;

                case (int)RuleConstants.RULE_VARIABLEDECS:
                    //<Variable Decs> ::= <Variable Declarator>
                    //todo: Create a new object using the stored tokens.
                    return null;

                case (int)RuleConstants.RULE_VARIABLEDECS_COMMA:
                    //<Variable Decs> ::= <Variable Decs> ',' <Variable Declarator>
                    //todo: Create a new object using the stored tokens.
                    return null;

                case (int)RuleConstants.RULE_VARIABLEDECLARATOR_IDENTIFIER:
                    //<Variable Declarator> ::= Identifier
                    //todo: Create a new object using the stored tokens.
                    return null;

                case (int)RuleConstants.RULE_VARIABLEDECLARATOR_IDENTIFIER_EQ:
                    //<Variable Declarator> ::= Identifier '=' <Variable Initializer>
                    //todo: Create a new object using the stored tokens.
                    return null;

                case (int)RuleConstants.RULE_VARIABLEINITIALIZER:
                    //<Variable Initializer> ::= <Expression>
                    //todo: Create a new object using the stored tokens.
                    return null;

                case (int)RuleConstants.RULE_VARIABLEINITIALIZER2:
                    //<Variable Initializer> ::= <Array Initializer>
                    //todo: Create a new object using the stored tokens.
                    return null;

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
                    //todo: Create a new object using the stored tokens.
                    return null;

                case (int)RuleConstants.RULE_FORINITOPT2:
                    //<For Init Opt> ::= <Statement Exp List>
                    //todo: Create a new object using the stored tokens.
                    return null;

                case (int)RuleConstants.RULE_FORINITOPT3:
                    //<For Init Opt> ::= 
                    //todo: Create a new object using the stored tokens.
                    return null;

                case (int)RuleConstants.RULE_FORITERATOROPT:
                    //<For Iterator Opt> ::= <Statement Exp List>
                    //todo: Create a new object using the stored tokens.
                    return null;

                case (int)RuleConstants.RULE_FORITERATOROPT2:
                    //<For Iterator Opt> ::= 
                    //todo: Create a new object using the stored tokens.
                    return null;

                case (int)RuleConstants.RULE_FORCONDITIONOPT:
                    //<For Condition Opt> ::= <Expression>
                    //todo: Create a new object using the stored tokens.
                    return null;

                case (int)RuleConstants.RULE_FORCONDITIONOPT2:
                    //<For Condition Opt> ::= 
                    //todo: Create a new object using the stored tokens.
                    return null;

                case (int)RuleConstants.RULE_STATEMENTEXPLIST_COMMA:
                    //<Statement Exp List> ::= <Statement Exp List> ',' <Statement Exp>
                    //todo: Create a new object using the stored tokens.
                    return null;

                case (int)RuleConstants.RULE_STATEMENTEXPLIST:
                    //<Statement Exp List> ::= <Statement Exp>
                    //todo: Create a new object using the stored tokens.
                    return null;

                case (int)RuleConstants.RULE_LOCALVARDECL:
                    //<Local Var Decl> ::= <Qualified ID> <Variable Decs>
                    //todo: Create a new object using the stored tokens.
                    return null;

                case (int)RuleConstants.RULE_STATEMENTEXP_LPAREN_RPAREN:
                    //<Statement Exp> ::= <Qualified ID> '(' <Arg List Opt> ')'
                    //todo: Create a new object using the stored tokens.
                    return null;

                case (int)RuleConstants.RULE_STATEMENTEXP_LPAREN_RPAREN2:
                    //<Statement Exp> ::= <Qualified ID> '(' <Arg List Opt> ')' <Methods Opt> <Assign Tail>
                    //todo: Create a new object using the stored tokens.
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
                    //todo: Create a new object using the stored tokens.
                    string id = (string)CreateObject(token.Tokens[0]);
                    Expression rhs = (Expression)CreateObject(token.Tokens[1]);
                    return new Assignment(id, rhs);

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
                    //todo: Create a new object using the stored tokens.
                    return CreateObject(token.Tokens[1]);

                case (int)RuleConstants.RULE_ASSIGNTAIL_PLUSEQ:
                    //<Assign Tail> ::= '+=' <Expression>
                    //todo: Create a new object using the stored tokens.
                    return null;

                case (int)RuleConstants.RULE_ASSIGNTAIL_MINUSEQ:
                    //<Assign Tail> ::= '-=' <Expression>
                    //todo: Create a new object using the stored tokens.
                    return null;

                case (int)RuleConstants.RULE_ASSIGNTAIL_TIMESEQ:
                    //<Assign Tail> ::= '*=' <Expression>
                    //todo: Create a new object using the stored tokens.
                    return null;

                case (int)RuleConstants.RULE_ASSIGNTAIL_DIVEQ:
                    //<Assign Tail> ::= '/=' <Expression>
                    //todo: Create a new object using the stored tokens.
                    return null;

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
                    //todo: Create a new object using the stored tokens.
                    return null;

                case (int)RuleConstants.RULE_METHOD_MEMBERNAME_LPAREN_RPAREN:
                    //<Method> ::= MemberName '(' <Arg List Opt> ')'
                    //todo: Create a new object using the stored tokens.
                    return null;

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
                    //todo: Create a new object using the stored tokens.
                    ProgramNode prgrm = new ProgramNode(new List<FunctionDecl>(), new List<ClassDecl>());
                    foreach (Token t in token.Tokens)
                    {
                        object o = CreateObject(t);
                        if (o != null)
                        {
                            if (o.GetType() == typeof(FunctionDecl))
                            {
                                var func = (FunctionDecl)o;
                                prgrm.addFunc(func);
                            }
                            else
                            {
                                //TODO
                                Console.WriteLine("Not impld");
                            }
                        }
                    }
                    return prgrm;

                case (int)RuleConstants.RULE_PROGRAMITEMS2:
                    //<Program Items> ::= 
                    return null;

                case (int)RuleConstants.RULE_PROGRAMITEM:
                    //<Program Item> ::= <Method Dec>
                    return CreateObject(token.Tokens[0]);

                case (int)RuleConstants.RULE_PROGRAMITEM2:
                    //<Program Item> ::= <Class Decl>
                    //todo: Create a new object using the stored tokens.
                    return null;

                case (int)RuleConstants.RULE_METHODDEC_IDENTIFIER_LPAREN_RPAREN:
                    //<Method Dec> ::= <Qualified ID> Identifier '(' <Formal Param List Opt> ')' <Block>
                    //todo: Create a new object using the stored tokens.
                    string retType = (string)CreateObject(token.Tokens[0]);
                    string func_id = (string)CreateObject(token.Tokens[1]);
                    var fParams = (List<FormalParam>)CreateObject(token.Tokens[3]) ?? new List<FormalParam>();
                    var block = (List<Statement>)CreateObject(token.Tokens[5]) ?? new List<Statement>();
                    return new FunctionDecl(fParams, func_id, retType, block);

                case (int)RuleConstants.RULE_FORMALPARAMLISTOPT:
                    //<Formal Param List Opt> ::= <Formal Param List>
                    //todo: Create a new object using the stored tokens.
                    return null;

                case (int)RuleConstants.RULE_FORMALPARAMLISTOPT2:
                    //<Formal Param List Opt> ::= 
                    //todo: Create a new object using the stored tokens.
                    return null;

                case (int)RuleConstants.RULE_FORMALPARAMLIST:
                    //<Formal Param List> ::= <Formal Param>
                    //todo: Create a new object using the stored tokens.
                    return null;

                case (int)RuleConstants.RULE_FORMALPARAMLIST_COMMA:
                    //<Formal Param List> ::= <Formal Param List> ',' <Formal Param>
                    //todo: Create a new object using the stored tokens.
                    return null;

                case (int)RuleConstants.RULE_FORMALPARAM_IDENTIFIER:
                    //<Formal Param> ::= <Qualified ID> Identifier
                    //todo: Create a new object using the stored tokens.
                    return null;

                case (int)RuleConstants.RULE_CLASSDECL_CLASS_IDENTIFIER_LPAREN_RPAREN_LBRACE_RBRACE:
                    //<Class Decl> ::= class Identifier '(' <Formal Param List Opt> ')' '{' <Class Item Decs Opt> '}'
                    //todo: Create a new object using the stored tokens.
                    return null;

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
            //todo: Report message to UI?
            Console.Error.WriteLine(message);
        }

        private void ParseErrorEvent(LALRParser parser, ParseErrorEventArgs args)
        {
            string message = "Parse error caused by token: '" + args.UnexpectedToken.ToString() + "'";
            //todo: Report message to UI?
            Console.Error.WriteLine(message);
        }

    }
}
