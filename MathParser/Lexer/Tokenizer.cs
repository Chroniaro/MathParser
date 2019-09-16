using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MathParser.Lexer
{
    public class Tokenizer : IEnumerable<Token>
    {
        private static readonly ILexer[] DEFAULT_LEXERS =
        {
            new NumberLexer()
        };

        public IEnumerator<char>? Source { get; set; }
        public ISet<ILexer> Lexers { get; }

        public Tokenizer()
        {
            Source = null;
            Lexers = new HashSet<ILexer>();
        }

        public Tokenizer ReadFromSource(IEnumerator<char> source)
        {
            Source = source;
            return this;
        }

        public Tokenizer ReadFromSource(IEnumerable<char> source) =>
            ReadFromSource(source.GetEnumerator());

        public Tokenizer ReadFromSource(StreamReader reader) =>
            ReadFromSource(reader.Chars());

        public Tokenizer UseLexer(ILexer lexer)
        {
            Lexers.Add(lexer);
            return this;
        }

        public Tokenizer UseLexers(IEnumerable<ILexer> lexers)
        {
            foreach (var lexer in lexers)
                UseLexer(lexer);

            return this;
        }

        public Tokenizer UseLexers(params ILexer[] lexers) =>
            UseLexers((IEnumerable<ILexer>)lexers);

        public Tokenizer UseDefaultLexers() =>
            UseLexers(DEFAULT_LEXERS);

        public IEnumerator<Token> GetEnumerator()
        {
            if (Source == null)
                throw new InvalidOperationException("Source must be set before a Tokenizer can be enumerated.");

            return new TokenStream(Source, Lexers);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
