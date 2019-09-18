using System;
using System.Collections.Generic;
using System.Text;

namespace MathParser.Lexer
{
    public abstract class Token
    {
        public string Content { get; }

        public Token(string content)
        {
            Content = content;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null)
                return false;

            if (obj.GetType() != GetType())
                return false;

            return Content == ((Token)obj).Content;
        }

        public override int GetHashCode()
        {
            return GetType().GetHashCode() ^ Content.GetHashCode();
        }
    }
}
