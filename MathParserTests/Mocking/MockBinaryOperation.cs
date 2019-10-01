using MathParser.LanguageModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace MathParserTests.Mocking
{
    public class MockBinaryOperation : BinaryOperation
    {
        public MockBinaryOperation(Expression left, Expression right) : 
            base(left, right)
        { }
    }
}
