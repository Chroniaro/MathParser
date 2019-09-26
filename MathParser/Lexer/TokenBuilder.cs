using MathParser.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MathParser.Lexer
{
    public class TokenBuilder : LookbackEnumerator<char>
    {
        private readonly IEnumerator<char> source;

        public TokenBuilder(IEnumerator<char> source)
        {
            this.source = source;
        }

        protected override void LoadMoreValues()
        {
            if (source.MoveNext())
                LoadedValues.Add(source.Current);
        }

        public bool TryMatchCurrent(char c)
        {
            if (!HasCurrent)
                return false;

            return Current == c;
        }

        public bool TryMatchNext(char c)
        {
            if (MoveNext())
                return Current == c;
            else
                return false;
        }

        public bool TryMatch(string token)
        {
            foreach (char c in token)
                if (!TryMatchNext(c))
                    return false;

            return true;
        }

        public string CollectPreceding()
        {
            var builder = new StringBuilder(CurrentIndex);
            foreach (char c in LoadedValues.Take(CurrentIndex))
                builder.Append(c);

            ForgetPreceding();

            return builder.ToString();
        }
    }
}
