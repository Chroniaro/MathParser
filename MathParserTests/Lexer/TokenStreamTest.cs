using MathParser.Lexer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace MathParserTests.Lexer
{
    [TestClass]
    public class TokenStreamTest
    {
        [TestMethod]
        public void Current_BeforeMoveNext_ThrowsInvalidOperation()
        {
            //set up
            using var stream = new TokenStream("");

            //test;
            Assert.ThrowsException<InvalidOperationException>(() => stream.Current);
        }

        private void TestLexing(string input, params Token[] output)
        {
            //set up
            var expected = ((IEnumerable<Token>)output).GetEnumerator();
            using var tokenStream = new TokenStream(input);

            //test
            CustomAssert.ProduceEqualValues(expected, tokenStream);
        }

        [TestMethod]
        public void EmptyString_NoTokens()
        {
            TestLexing("", new Token[] { });
        }

        [TestMethod]
        [DataRow("0", 0)]
        [DataRow("5", 5)]
        [DataRow("020", 20)]
        [DataRow("123", 123)]
        public void SimpleIntegerString_NumberTokenWithCorrectValue(string input, double expectedOutput)
        {
            TestLexing(input, new NumberToken(expectedOutput));
        }

        [TestMethod]
        [DataRow("+0", 0)]
        [DataRow("-0", 5)]
        [DataRow("+020", 20)]
        [DataRow("-123", -123)]
        [DataRow("+87", 87)]
        public void SignedIntegerString_NumberTokenWithCorrectValue(string input, double expectedOutput)
        {
            TestLexing(input, new NumberToken(expectedOutput));
        }
    }
}
