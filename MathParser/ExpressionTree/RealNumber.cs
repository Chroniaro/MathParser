using System;
using System.Collections.Generic;
using System.Text;

namespace MathParser.ExpressionTree
{
    public class RealNumber : Expression
    {
        public double Value { get; set; }

        public override string ToString()
        {
            return "(" + Value + ")";
        }
    }
}
