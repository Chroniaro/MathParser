using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

[assembly: InternalsVisibleTo("Tests")]
namespace MathParser.Lexer
{
    internal static class StreamReaderCharsEnumerator
    {
        public static IEnumerable<char> Chars(this StreamReader reader)
        {
            while (true)
            {
                int next = reader.Read();
                if (next == -1)
                    yield break;
                else
                    yield return (char)next;
            }
        }
    }
}
