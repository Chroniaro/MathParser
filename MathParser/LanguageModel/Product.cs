using System;
using System.Collections.Generic;
using System.Text;

namespace MathParser.LanguageModel
{
    public class Product : BinaryOperation
    {
        public Product(Expression left, Expression right) :
            base(left, right)
        { }
    }
}
