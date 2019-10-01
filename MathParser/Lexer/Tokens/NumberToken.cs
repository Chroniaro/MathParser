using MathParser.LanguageModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace MathParser.Lexer
{
    public class NumberToken : Token, IEvaluatable
    {
        public double Value { get; }

        public Expression GetValue() =>  new Number(Value);

        public NumberToken(double value) : base(value.ToString())
        {
            Value = value;
        }

        public override bool Equals(object? obj)
        {
            if (obj is NumberToken numberObj)
                return Value == numberObj.Value;
            else
                return false;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}
