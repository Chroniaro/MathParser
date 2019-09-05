using System;
using System.Collections.Generic;
using System.Text;

namespace MathParser.Lexer
{
    internal abstract class Lexer
    {
        public abstract Token Lex(TokenBuilder builder);
    }
}
