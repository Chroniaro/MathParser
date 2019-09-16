using MathParser.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MathParser.Lexer
{
    public class TokenStream : IEnumerator<Token>
    {
        private readonly TokenBuilder tokenBuilder;
        private readonly List<ILexer> lexers;

        #region Constructors
        public TokenStream(IEnumerator<char> source)
        {
            tokenBuilder = new TokenBuilder(source);

            lexers = new List<ILexer>
            {
                new NumberLexer()
            };
        }

        public TokenStream(IEnumerable<char> source) : this(source.GetEnumerator()) { }

        public TokenStream(StreamReader source) : this(source.Chars()) { }
        #endregion

        #region Current
        private Token? current = null;

        public Token Current 
        { 
            get
            {
                if (current == null)
                    throw new InvalidOperationException("Cannot access current before calling MoveNext");

                return current!;
            }
        }

        object IEnumerator.Current => Current;
        #endregion

        public bool MoveNext()
        {
            foreach (var lexer in lexers)
                if (lexer.Lex(tokenBuilder) is Token token)
                {
                    current = token;
                    return true;
                }
            return false;
        }

        public void Reset()
        {
            throw new InvalidOperationException();
        }

        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
