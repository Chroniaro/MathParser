using System;
using System.Collections.Generic;
using System.Text;

namespace MathParser.Lexer
{
    public class WhiteSpaceToken : DelimiterToken
    {
        public WhiteSpaceToken(string value) : base(value)
        { }
    }
}
