using MathParser.Lexer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace MathParserTests.Lexer
{
    [TestClass]
    public class NumberLexerTest
    {
        private void TestLexing(string input, double? expectedOutput)
        {
            //set up
            var numberLexer = new NumberLexer();
            var tokenBuilder = new TokenBuilder(input.GetEnumerator());

            //act
            var token = (NumberToken) numberLexer.Lex(tokenBuilder);

            //test
            if (expectedOutput == null)
                Assert.AreEqual(null, token);
            else
                Assert.AreEqual(expectedOutput.Value, token.Value);

            Assert.IsFalse(tokenBuilder.MoveNext());
        }

        [TestMethod]
        [DataRow("0", 0)]
        [DataRow("5", 5)]
        [DataRow("020", 20)]
        [DataRow("123", 123)]
        public void Lex_SimpleInteger(string input, double expectedOutput)
        {
            TestLexing(input, expectedOutput);
        }

        [TestMethod]
        [DataRow("0.0", 0)]
        [DataRow("12.7", 12.7)]
        [DataRow("41.523", 41.523)]
        public void Lex_NumberWithInternalDecimalPoint(string input, double expectedOutput)
        {
            TestLexing(input, expectedOutput);
        }

        [TestMethod]
        [DataRow("0.", 0)]
        [DataRow("142.", 142)]
        public void Lex_IntegerWithTerminatingDecimalPoint(string input, double expectedOutput)
        {
            TestLexing(input, expectedOutput);
        }

        [TestMethod]
        [DataRow(".0", 0)]
        [DataRow(".123", .123)]
        [DataRow(".00025", .00025)]
        public void Lex_NumberWithLeadingDecimalPoint(string input, double expectedOutput)
        {
            TestLexing(input, expectedOutput);
        }
    }
}
