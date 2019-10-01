using System;
using System.Collections.Generic;
using System.Text;
using MathParser.LanguageModel;

namespace MathParser.Parser
{
    public class ProductParser : InfixBinaryOperationParser
    {
        public override string OperatorDelimiter => "*";

        public override Expression CreateOperationExpression(Expression left, Expression right) =>
            new Product(left, right);
    }
}
