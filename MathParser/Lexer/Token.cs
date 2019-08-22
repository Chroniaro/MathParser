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
    }
}
