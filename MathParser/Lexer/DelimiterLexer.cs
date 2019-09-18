using rm.Trie;
using System;
using System.Collections.Generic;
using System.Text;

namespace MathParser.Lexer
{
    class DelimiterLexer : AbstractLexer
    {
        public delegate DelimiterToken TokenConstructor(string content);

        private static readonly TokenConstructor DEFAULT_CONSTRUCTOR = content => new DelimiterToken(content);

        private static readonly string[] DEFAULT_DELIMITERS =
        {
            "+", "-", "*", "/"
        };

        public TrieMap<TokenConstructor> Delimiters { get; }

        public DelimiterLexer()
        {
            Delimiters = new TrieMap<TokenConstructor>();
        }

        public DelimiterLexer UseDelimiters(TokenConstructor constructor, params string[] delimiters)
        {
            foreach (string delimiter in delimiters)
                Delimiters.Add(delimiter, constructor);

            return this;
        }

        public DelimiterLexer UseDelimiters(params string[] delimiters) =>
            UseDelimiters(DEFAULT_CONSTRUCTOR, delimiters);

        public DelimiterLexer UseDefaultDelimiters => UseDelimiters(DEFAULT_DELIMITERS);

        public override Token? Lex(TokenBuilder tokenBuilder)
        {
            (TokenConstructor, string)? lastFullDelimiter = null;

            var delimiterBuilder = new StringBuilder(5);
            var trieNode = Delimiters.GetRootTrieNode();

            while (tokenBuilder.MoveNext())
            {
                char c = tokenBuilder.Current;
                if (!trieNode.HasChild(c))
                    break;

                trieNode = trieNode.GetChild(c);
                delimiterBuilder.Append(c);

                if (trieNode.HasValue())
                    lastFullDelimiter = (trieNode.Value, delimiterBuilder.ToString());
            }

            if (lastFullDelimiter is (TokenConstructor constructor, string delimiter))
                return constructor(delimiter);
            else
                return null;
        }
    }
}
