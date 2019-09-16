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

        public override bool Equals(object? obj)
        {
            if (obj is NumberToken token)
                return Value == token.Value;
            else
                return false;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}
