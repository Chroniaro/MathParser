using rm.Trie;
using System.Text;

namespace MathParser.Lexer
{
    public class DelimiterLexer : AbstractLexer
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
            groupingDelimiters.AddTo(this);
            return this;
        }

        public DelimiterLexer UseGroupingDelimiterPairs(params (string, string)[] pairs) =>
            UseGroupingDelimiterPairs(
                new GroupingDelimiterPairManager()
                    .UseGroupingPairs(pairs)
            );

        public DelimiterLexer UseDefaultDelimiters() =>
            this
                .UseDelimiters("+", "-", "*", "/")
                .UseGroupingDelimiterPairs(
                    ("(", ")"),
                    ("[", "]")
                );

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
