using MathParser.LanguageModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace MathParser.Lexer
{
    public interface IEvaluatable
    {
        public Expression GetValue();
    }
}
