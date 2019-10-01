using System;
using System.Collections.Generic;
using System.Text;
using MathParser.LanguageModel;
using MathParser.Lexer;
using MathParser.Util;

namespace MathParser.Parser
{
    public class ValueParser : IParser
    {
        public Expression? Parse(ITokenStream tokens)
        {
            if (!tokens.MoveNext())
                return null;

            if (tokens.Current is IEvaluatable valueToken)
            {
                tokens.MoveNext();
                return valueToken.GetValue();
            }
            else
                return null;
        }
    }
}
