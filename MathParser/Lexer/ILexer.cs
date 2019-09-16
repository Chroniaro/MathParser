using System;
using System.Collections.Generic;
using System.Text;

namespace MathParser.Lexer
{
    public interface ILexer
    {
        Token? Lex(TokenBuilder builder);
    }
}
