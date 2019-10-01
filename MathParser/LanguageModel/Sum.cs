using System;
using System.Collections.Generic;
using System.Text;

namespace MathParser.LanguageModel
{
    class Sum : BinaryOperation
    {
        public Sum(Expression left, Expression right) :
            base(left, right)
        { }
    }
}
