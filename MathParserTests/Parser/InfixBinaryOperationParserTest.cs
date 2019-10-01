using MathParser.LanguageModel;
using MathParser.Lexer;
using MathParser.Parser;
using MathParserTests.Mocking;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace MathParserTests.Parser
{
    [TestClass]
    public class InfixBinaryOperationParserTest
    {
        [TestMethod]
        public void Parse_MatchingInfixExpression_ReturnsInfixExpression()
        {
            //set up
            var parser = new MockInfixBinaryOperatorParser{ TermParser = new ValueParser() };

            var tokens = new MockTokenStream(
                new NumberToken(1),
                new DelimiterToken(parser.OperatorDelimiter),
                new NumberToken(2)
            );

            //act
            var expression = parser.Parse(tokens);

            //test
            if (expression is MockBinaryOperation operation)
            {
                Assert.IsTrue(operation.Left is Number { Value: 1 });
                Assert.IsTrue(operation.Right is Number { Value: 2 });
            }
            else
                Assert.Fail("The returned expression was not a binary operation.");
        }

        [TestMethod]
        public void Parse_NonMatchingInfixExpression_ReturnsNull()
        {
            //set up
            var parser = new MockInfixBinaryOperatorParser { TermParser = new ValueParser() };
            var tokens = new MockTokenStream();

            //act
            var expression = parser.Parse(tokens);

            //test
            Assert.IsNull(expression);
        }

        [TestMethod]
        public void Parse_MatchingTermButNotMatchingInfix_ReturnsTerm()
        {
            //set up
            var parser = new MockInfixBinaryOperatorParser { TermParser = new ValueParser() };
            var tokens = new MockTokenStream(new NumberToken(0));

            //act
            var expression = parser.Parse(tokens);

            //test
            Assert.IsInstanceOfType(expression, typeof(Number));
        }
    }

    class MockInfixBinaryOperatorParser : InfixBinaryOperationParser
    {
        public override string OperatorDelimiter => "*";

        public override Expression CreateOperationExpression(Expression left, Expression right) =>
            new MockBinaryOperation(left, right);
    }
}
