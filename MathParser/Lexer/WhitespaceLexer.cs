using System;
using System.Collections.Generic;
using System.Text;

namespace MathParser.Lexer
{
    public class WhitespaceLexer : CharacterSetLexer
    {
        public new ISet<char> IncludedCharacters => base.IncludedCharacters;

        protected override Token ConstructToken(string chars) =>
            new WhitespaceToken(chars);

        public new WhitespaceLexer UseCharacters(IEnumerable<char> chars) =>
            (WhitespaceLexer)base.UseCharacters(chars);

        public new WhitespaceLexer UseCharacters(params char[] chars) =>
            UseCharacters((IEnumerable<char>)chars);

        public new WhitespaceLexer UseCharactersInRange(char first, char last) =>
            (WhitespaceLexer)base.UseCharactersInRange(first, last);

        public WhitespaceLexer UseDefaultWhitespaceCharacters() =>
            (WhitespaceLexer) UseCharacters(' ', '\t', '\n', '\r');
    }
}
