using MathParser.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MathParser.Lexer
{
    internal class TokenBuilder : IEnumerator<char>
    {
        private readonly IEnumerator<char> source;

        private readonly List<char> toProcess;
        private int currentIndex;

        public char Current => toProcess[currentIndex];

        object? IEnumerator.Current => Current;

        public TokenBuilder(IEnumerator<char> source)
        {
            this.source = source;
            toProcess = new List<char>(10);
            currentIndex = -1;
        }

        private bool LoadMoreChars()
        {
            if (source.MoveNext())
            {
                toProcess.Add(source.Current);
                return true;
            }
            else
                return false;
        }

        public bool MoveNext()
        {
            ++currentIndex;
            if (currentIndex >= toProcess.Count)
                return LoadMoreChars();
            else
                return true;
        }

        public bool StepBack()
        {
            if (currentIndex >= 0)
            {
                --currentIndex;
                return true;
            }
            else
                return false;
        }

        public void Reset()
        {
            currentIndex = -1;
        }

        public void RollBack(int newLength)
        {
            currentIndex = newLength - 1;
        }

        public void PopToken()
        {
            toProcess.RemoveRange(0, currentIndex);
            currentIndex = -1;
        }

        public string BuildToken()
        {
            var builder = new StringBuilder(currentIndex);
            foreach (char c in toProcess.Take(currentIndex))
                builder.Append(c);

            PopToken();

            return builder.ToString();
        }

        #region IDisposable Support

        protected virtual void Dispose(bool disposing)
        {}

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
