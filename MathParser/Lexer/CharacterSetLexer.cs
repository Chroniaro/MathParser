using System;
using System.Collections.Generic;
using System.Text;

namespace MathParser.Lexer
{
    public abstract class CharacterSetLexer : CharacterClassLexer
    {
        protected ISet<char> IncludedCharacters { get; }

        public CharacterSetLexer()
        {
            IncludedCharacters = new HashSet<char>();
        }

        protected CharacterSetLexer UseCharacters(params char[] chars)
        {
            IncludedCharacters.UnionWith(chars);
            return this;
        }

        protected CharacterSetLexer UseCharactersInRange(char first, char last)
        {
            for (char c = first; c <= last; ++c)
                IncludedCharacters.Add(c);

            return this;
        }

        protected override bool IsInCharacterClass(char c) =>
            IncludedCharacters.Contains(c);
    }
}
