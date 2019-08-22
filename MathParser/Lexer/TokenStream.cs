using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MathParser.Lexer
{
    public class TokenStream : IEnumerator<Token>
    {
        private readonly IEnumerator<char> source;

        /**
         * Characters pulled source which do not become part of a token should
         * be added to this queue to be reprocessed in future iterations
         */
        private readonly Queue<char> toProcess;

        #region Constructors
        public TokenStream(IEnumerator<char> source)
        {
            this.source = source;

            toProcess = new Queue<char>(5);
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

        private char? NextChar()
        {
            if (toProcess.Count > 0)
                return toProcess.Dequeue();

            if (source.MoveNext())
                return source.Current;

            return null;
        }

        public bool MoveNext()
        {
            var content = new StringBuilder(5);

            return true;
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
