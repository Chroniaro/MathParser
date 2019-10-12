using MathParser.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MathParser.Lexer
{
    internal class TokenStream : AbstractLookbackEnumerator<Token>, ITokenStream
    {
        private readonly TokenBuilder tokenBuilder;
        private readonly IEnumerable<ILexer> lexers;

        internal TokenStream(IEnumerator<char> source, IEnumerable<ILexer> lexers)
        {
            tokenBuilder = new TokenBuilder(source);
            this.lexers = lexers;
        }

        protected override void LoadMoreValues()
        {
            foreach (var lexer in lexers)
                if (lexer.Lex(tokenBuilder) is Token token)
                {
                    tokenBuilder.ForgetPreceding();

                    if (token is ISkippable)
                    {
                        LoadMoreValues();
                        return;
                    }
                    else
                    {
                        LoadedValues.Add(token);
                        return;
                    }
                }
                else
                    tokenBuilder.Reset();

            if (tokenBuilder.MoveNext())
                throw new InvalidDataException("Failed to create token at character " + tokenBuilder.Current);
        }

        #region IDisposable Support
        private bool disposedValue = false;

        protected override void Dispose(bool disposing)
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
        #endregion
    }
}
