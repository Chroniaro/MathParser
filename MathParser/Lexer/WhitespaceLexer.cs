using System;
using System.Collections.Generic;
using System.Text;

namespace MathParser.Lexer
{
    class WhitespaceLexer : AbstractLexer
    {
        private ISet<char> WhitespaceCharacters { get; }

        public WhitespaceLexer()
        {
            WhitespaceCharacters = new HashSet<char>(4);
        }

        public WhitespaceLexer UseWhitespaceCharacters(params char[] whitespaceChars)
        {
            WhitespaceCharacters.UnionWith(whitespaceChars);
            return this;
        }

        public WhitespaceLexer UseDefaultWhitespaceCharacters() =>
            UseWhitespaceCharacters(' ', '\t', '\n');

        public bool IsWhitespace(char c) => WhitespaceCharacters.Contains(c);

        private bool IsNextWhitespace(TokenBuilder builder) =>
            builder.MoveNext() && IsWhitespace(builder.Current);

        public override Token? Lex(TokenBuilder builder)
        {
            if (!IsNextWhitespace(builder))
                return null;

            while (IsNextWhitespace(builder))
                ; // DO NOTHING

            return new WhiteSpaceToken(builder.CollectPreceding());
        }
    }
}
