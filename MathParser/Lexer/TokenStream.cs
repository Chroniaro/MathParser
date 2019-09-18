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
        private readonly IEnumerable<ILexer> lexers;

        internal TokenStream(IEnumerator<char> source, IEnumerable<ILexer> lexers)
        {
            tokenBuilder = new TokenBuilder(source);
            this.lexers = lexers;
        }

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
                    tokenBuilder.ForgetPreceding();
                    current = token;
                    return true;
                }
                else
                    tokenBuilder.Reset();

            if (tokenBuilder.MoveNext())
                throw new InvalidDataException("Failed to create token at character " + tokenBuilder.Current);
            else
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
                    tokenBuilder.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
