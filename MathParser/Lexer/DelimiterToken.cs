using System;
using System.Collections.Generic;
using System.Text;

namespace MathParser.Lexer
{
    public class DelimiterToken : Token
    {
        public DelimiterToken(string content) : base(content)
        { }
    }
}
