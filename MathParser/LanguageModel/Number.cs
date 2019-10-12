using System;
using System.Collections.Generic;
using System.Text;

namespace MathParser.LanguageModel
{
    public class Number : Expression
    {
        public double Value { get; set; }

        public Number(double value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
