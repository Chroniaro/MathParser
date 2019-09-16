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
            using var stream = new Tokenizer()
                .ReadFromSource("")
                .GetEnumerator();

            //test;
            Assert.ThrowsException<InvalidOperationException>(() => stream.Current);
        }

        [TestMethod]
        public void EmptyString_NoTokens()
        {
            var tokenStream = new Tokenizer()
                .ReadFromSource("")
                .GetEnumerator();

            Assert.IsFalse(tokenStream.MoveNext());
        }
    }
}
