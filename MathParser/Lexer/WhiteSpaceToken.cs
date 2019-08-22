using System;
using System.Collections.Generic;
using System.Text;

namespace MathParser.Lexer
{
    public class WhiteSpaceToken : Token
    {
        public WhiteSpaceToken(string value) : base(value)
        {
        }
    }
}
