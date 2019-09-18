using System;
using System.Collections.Generic;
using System.Text;

namespace MathParser.Lexer
{
    public abstract class CharacterClassLexer : AbstractLexer
    {
        protected abstract bool IsInCharacterClass(char c);

        protected abstract Token ConstructToken(string chars);

        private bool IncludeNext(TokenBuilder builder) =>
            builder.MoveNext() && IsInCharacterClass(builder.Current);

        public override Token? Lex(TokenBuilder builder)
        {
            if (!IncludeNext(builder))
                return null;

            while (IncludeNext(builder))
                ; // DO NOTHING

            return ConstructToken(builder.CollectPreceding());
        }
    }
}
