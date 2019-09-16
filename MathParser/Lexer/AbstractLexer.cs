using System;
using System.Collections.Generic;
using System.Text;

namespace MathParser.Lexer
{
    public abstract class AbstractLexer : ILexer
    {
        public abstract Token? Lex(TokenBuilder builder);

        public override bool Equals(object? obj)
        {
            if (obj == null)
                return false;

            return GetType() == obj.GetType();
        }

        public override int GetHashCode()
        {
            return GetType().GetHashCode();
        }
    }
}
