using System;
using System.Collections.Generic;
using System.Text;

namespace MathParser.Lexer
{
    public class IdentifierToken : Token
    {
        public IdentifierToken(string value) : base(value)
        {
        }
    }
}
