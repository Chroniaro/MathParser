using System;
using System.Collections.Generic;
using System.Text;

namespace MathParser.Lexer
{
    public class LeftGroupingToken : DelimiterToken
    {
        public string Opposite { get; }

        public LeftGroupingToken(string content, string opposite) :
            base(content)
        {
            Opposite = opposite;
        }
    }
}
