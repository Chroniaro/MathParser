using System;
using System.Collections.Generic;
using System.Text;

namespace MathParser.LanguageModel
{
    public abstract class BinaryOperation : Expression
    {
        public Expression Left { get; set; }

        public Expression Right { get; set; }

        public BinaryOperation(Expression left, Expression right)
        {
            Left = left;
            Right = right;
        }

        public override string ToString()
        {
            return GetType().Name + "(" + Left + ", " + Right + ")";
        }
    }
}
