using System;
using System.Collections.Generic;
using System.Text;

namespace MathParser.Lexer
{
    public class IdentifierLexer : CharacterSetLexer
    {
        public new ISet<char> IncludedCharacters => base.IncludedCharacters;

        protected override Token ConstructToken(string chars) =>
            new IdentifierToken(chars);

        public new IdentifierLexer UseCharacters(params char[] chars) =>
            (IdentifierLexer)base.UseCharacters(chars);

        public new IdentifierLexer UseCharactersInRange(char first, char last) =>
            (IdentifierLexer)base.UseCharactersInRange(first, last);

        public IdentifierLexer UseDefaultIdentifierCharacters() =>
            (IdentifierLexer)this
                .UseCharactersInRange('0', '9')
                .UseCharactersInRange('a', 'z')
                .UseCharactersInRange('A', 'z')
                .UseCharacters('$', '_');
    }
}
