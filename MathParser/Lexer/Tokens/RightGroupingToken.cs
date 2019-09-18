using System;
using System.Collections.Generic;
using System.Text;

namespace MathParser.Lexer
{
    public class RightGroupingToken : DelimiterToken
    {
        public string Opposite { get; }

        public RightGroupingToken(string content, string opposite) :
            base(content)
        {
            Opposite = opposite;
        }
    }
}
