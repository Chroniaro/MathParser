using MathParser.LanguageModel;
using MathParser.Lexer;
using MathParser.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace MathParser.Parser
{
    public interface IParser
    {
        public Expression? Parse(ITokenStream tokens);
    }
}
