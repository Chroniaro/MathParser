using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

[assembly: InternalsVisibleTo("Tests")]
namespace MathParser.Lexer
{
    internal class CharacterStream
    {
        private readonly StreamReader source;

        public CharacterStream(StreamReader source)
        {
            this.source = source;
        }

        public char? Next()
        {
            int next = source.Read();

            if (next == -1)
                return null;

            return (char)next;
        }
    }
}
