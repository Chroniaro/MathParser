using System;
using System.Collections.Generic;
using System.Text;

namespace MathParser.Lexer
{
    class DelimiterLexer : AbstractLexer
    {
        private static readonly string[] DEFAULT_DELIMITERS =
        {
            "+", "-", "*", "/"
        };

        public SortedSet<string> Delimiters { get; }

        public DelimiterLexer()
        {
            Delimiters = new SortedSet<string>();
        }

        public DelimiterLexer UseDelimiters(params string[] delimiters)
        {
            foreach (string delimiter in delimiters)
                Delimiters.Add(delimiter);

            return this;
        }

        public DelimiterLexer UseDefaultDelimiters => UseDelimiters(DEFAULT_DELIMITERS);

        public override Token? Lex(TokenBuilder tokenBuilder)
        {
            var delimiters = Delimiters.GetEnumerator();
            string? lastMatch = null;
            var delimiterBuilder = new StringBuilder(5);
            while (FindNextMatch(tokenBuilder, delimiterBuilder, delimiters) is string match)
                lastMatch = match;

            if (lastMatch == null)
                return null;
            else
                return new DelimiterToken(lastMatch);
        }

        private string? FindNextMatch(TokenBuilder tokenBuilder, StringBuilder matchBuilder, IEnumerator<string> delimiters)
        {
            try
            {
                tokenBuilder.MoveNext();
                matchBuilder.Append(tokenBuilder.Current);

                string match = matchBuilder.ToString();
                do
                    delimiters.MoveNext();
                while (string.Compare(delimiters.Current, match) < 0);

                while (string.Compare(delimiters.Current, match) > 0)
                {
                    tokenBuilder.MoveNext();
                    matchBuilder.Append(tokenBuilder.Current);
                    match = matchBuilder.ToString();
                }

                if (match == delimiters.Current)
                    return match;
                else
                    return null;
            }
            catch (ArgumentOutOfRangeException)
            {
                return null;
            }
        }
    }
}
