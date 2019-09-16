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
                Assert.AreEqual(expectedOutput, token.Value);
        }

        [TestMethod]
        [DataRow("0", 0)]
        [DataRow("5", 5)]
        [DataRow("020", 20)]
        [DataRow("123", 123)]
        public void SimpleIntegerString_NumberTokenWithCorrectValue(string input, double expectedOutput)
        {
            TestLexing(input, expectedOutput);
        }

        [TestMethod]
        [DataRow("-0", 0)]
        [DataRow("+0", 0)]
        [DataRow("+020", 20)]
        [DataRow("-123", -123)]
        [DataRow("+87", 87)]
        public void SignedIntegerString_NumberTokenWithCorrectValue(string input, double expectedOutput)
        {
            TestLexing(input, expectedOutput);
        }
    }
}
