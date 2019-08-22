using System;
using System.Collections.Generic;
using System.Text;

namespace MathParser.Lexer
{
    public class NumberToken : Token
    {
        public double Value { get; }

        public NumberToken(double value) : base(value.ToString())
        {
            Value = value;
        }
    }
}
