using MathParser.Lexer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace MathParserTests.Lexer
{
    [TestClass]
    public class TokenBuilderTests
    {
        [TestMethod]
        [DataRow("asdffoo")]
        public void NextChar_ReturnsEachCharThenNull(string testString)
        {
            //set up
            var builder = new TokenBuilder(testString.GetEnumerator());

            //test
            foreach (char c in testString)
                Assert.AreEqual(c, builder.NextChar());
            Assert.AreEqual(null, builder.NextChar());
        }

        [TestMethod]
        [DataRow("asdffoo", 4)]
        [DataRow("potato", 0)]
        public void PopToken_ReturnsStringOfProcessedChars(string testString, int chars)
        {
            //set up
            var builder = new TokenBuilder(testString.GetEnumerator());

            //act
            for (int i = 0; i < chars; i++)
                builder.NextChar();

            //test
            Assert.AreEqual(testString.Substring(0, chars), builder.PopToken());
        }

        [TestMethod]
        [DataRow("soup", 2)]
        public void PopToken_ClearsProcessedChars(string testString, int chars)
        {
            //set up
            var builder = new TokenBuilder(testString.GetEnumerator());

            //act
            for (int i = 0; i < chars; i++)
                builder.NextChar();

            builder.PopToken();

            //test
            Assert.AreEqual("", builder.PopToken());
        }

        [TestMethod]
        [DataRow("floppypotato", 6, 4)]
        public void RollBack_RemovesProcessedCharsFromCurrentToken(string testString, 
            int initiallyProcessedChars, int rollBackLength)
        {
            //set up
            var builder = new TokenBuilder(testString.GetEnumerator());

            //act
            for (int i = 0; i < initiallyProcessedChars; i++)
                builder.NextChar();

            builder.RollBack(rollBackLength);

            //test
            Assert.AreEqual(testString.Substring(0, rollBackLength), builder.PopToken());
        }

        [TestMethod]
        public void NextCharAndPopToken_AfterRollBack_IncludeRolledBackCharsBeforeContinuing()
        {
            //set up
            var builder = new TokenBuilder("asdfjk".GetEnumerator());

            //act
            for (int i = 0; i < 4; i++)
                builder.NextChar();

            builder.RollBack(2);
            builder.PopToken();

            //test
            char?[] expected = { 'd', 'f', 'j', 'k', null };
            foreach (char? c in expected)
                Assert.AreEqual(c, builder.NextChar());
            Assert.AreEqual("dfjk", builder.PopToken());
        }
    }
}
