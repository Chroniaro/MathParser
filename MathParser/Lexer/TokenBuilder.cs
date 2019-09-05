using MathParser.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace MathParser.Lexer
{
    internal class TokenBuilder
    {
        private readonly IEnumerator<char> source;
        private readonly Queue<char> toProcess;
        
        public StringBuilder Token { get; }

        public TokenBuilder(IEnumerator<char> source)
        {
            this.source = source;
            toProcess = new Queue<char>(10);
            Token = new StringBuilder(10);
        }

        public char? NextChar()
        {
            char nextChar;
            if (toProcess.Count > 0)
                nextChar = toProcess.Dequeue();
            else if (source.MoveNext())
                nextChar = source.Current;
            else
                return null;

            Token.Append(nextChar);
            return nextChar;
        }

        public void RollBack(int newLength)
        {
            int start = newLength;
            int length = Token.Length - newLength;

            Token.ForEachCharInRange(start, length, toProcess.Enqueue);
            Token.Remove(start, length);
        }

        public string PopToken()
        {
            string token = Token.ToString();
            Token.Clear();
            return token;
        }
    }
}
