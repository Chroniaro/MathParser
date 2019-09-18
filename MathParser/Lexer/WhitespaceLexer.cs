﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MathParser.Lexer
{
    class WhitespaceLexer : CharacterSetLexer
    {
        public new ISet<char> IncludedCharacters => base.IncludedCharacters;

        protected override Token ConstructToken(string chars) =>
            new WhitespaceToken(chars);

        public new WhitespaceLexer UseCharacters(params char[] chars) =>
            (WhitespaceLexer)base.UseCharacters(chars);

        public new WhitespaceLexer UseCharactersInRange(char first, char last) =>
            (WhitespaceLexer)base.UseCharactersInRange(first, last);

        public WhitespaceLexer UseDefaultWhitespaceCharacters() =>
            (WhitespaceLexer) UseCharacters(' ', '\t', '\n');
    }
}
