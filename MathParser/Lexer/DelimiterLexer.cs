using rm.Trie;
using System.Text;

namespace MathParser.Lexer
{
    class DelimiterLexer : AbstractLexer
    {
        public delegate DelimiterToken TokenConstructor(string content);

        public TrieMap<TokenConstructor> Delimiters { get; }

        public DelimiterLexer()
        {
            Delimiters = new TrieMap<TokenConstructor>();
        }

        public DelimiterLexer UseDelimiter(TokenConstructor constructor, string delimiter)
        {
            Delimiters.Add(delimiter, constructor);
            return this;
        }

        public DelimiterLexer UseDelimiters(TokenConstructor constructor, params string[] delimiters)
        {
            foreach (string delimiter in delimiters)
                UseDelimiter(constructor, delimiter);

            return this;
        }

        public DelimiterLexer UseDelimiters(params string[] delimiters) =>
            UseDelimiters(s => new DelimiterToken(s), delimiters);

        public DelimiterLexer UseGroupingDelimiterPairs(GroupingDelimiterPairManager groupingDelimiters)
        {
            foreach (var leftToken in groupingDelimiters.LeftToRightMap.Keys)
                UseDelimiter(groupingDelimiters.CreateLeft, leftToken);

            foreach (var rightToken in groupingDelimiters.RightToLeftMap.Keys)
                UseDelimiter(groupingDelimiters.CreateRight, rightToken);

            return this;
        }

        public DelimiterLexer UseGroupingDelimiterPairs(params (string, string)[] pairs) =>
            UseGroupingDelimiterPairs(
                new GroupingDelimiterPairManager()
                    .UseGroupingPairs(pairs)
            );

        public DelimiterLexer UseDefaultDelimiters()
        {
            UseDelimiters("+", "-", "*", "/");

            UseGroupingDelimiterPairs(
                ("(", ")"),
                ("[", "]"),
                ("{", "}")
            );

            return this;
        }

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
