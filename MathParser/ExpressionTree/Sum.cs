using System;
using System.Collections.Generic;
using System.Text;

namespace MathParser.ExpressionTree
{
    public class Sum : Expression
    {
        public List<Expression> Terms { get; } = new List<Expression>();

        public override string ToString()
        {
            return "(" + String.Join(" + ", Terms) + ")";
        }
    }
}
