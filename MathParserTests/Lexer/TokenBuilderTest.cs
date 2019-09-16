using MathParser.Lexer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace MathParserTests.Lexer
{
    [TestClass]
    public class TokenBuilderTest
    {
        [TestMethod]
        [DataRow("asdffoo")]
        public void NextChar_ReturnsEachCharThenNull(string testString)
        {
            //set up
            using var builder = new TokenBuilder(testString.GetEnumerator());

            //test
            CustomAssert.ProduceEqualValues(testString.GetEnumerator(), builder);
        }

        [TestMethod]
        [DataRow("asdffoo", 4)]
        [DataRow("potato", 0)]
        [DataRow("foo", 3)]
        public void BuildToken_ReturnsStringOfProcessedChars(string testString, int chars)
        {
            //set up
            using var builder = new TokenBuilder(testString.GetEnumerator());

            //act
            for (int i = 0; i <= chars; i++)
                builder.MoveNext();

            //test
            Assert.AreEqual(testString.Substring(0, chars), builder.BuildToken());
        }

        [TestMethod]
        [DataRow("soup", 2)]
        [DataRow("foo", 3)]
        [DataRow("zero", 0)]
        public void ForgetPreceding_ClearsProcessedChars(string testString, int chars)
        {
            //set up
            using var builder = new TokenBuilder(testString.GetEnumerator());

            //act
            for (int i = 0; i <= chars; i++)
                builder.MoveNext();

            builder.ForgetPreceding();

            //test
            CustomAssert.ProduceEqualValues(testString.Substring(chars).GetEnumerator(), builder);
        }

        [TestMethod]
        [DataRow("floppypotato", 6, 4)]
        public void RollBack_RemovesProcessedCharsFromCurrentToken(string testString, 
            int initiallyProcessedChars, int rollBackLength)
        {
            //set up
            using var builder = new TokenBuilder(testString.GetEnumerator());

            //act
            for (int i = 0; i < initiallyProcessedChars; i++)
                builder.MoveNext();

            builder.RollBack(rollBackLength);

            //test
            Assert.AreEqual(testString.Substring(0, rollBackLength - 1), builder.BuildToken());
        }

        [TestMethod]
        public void NextCharAndBuildToken_AfterRollBack_IncludeRolledBackCharsBeforeContinuing()
        {
            //set up
            using var builder = new TokenBuilder("asdfjk".GetEnumerator());

            //act
            for (int i = 0; i < 4; i++)
                builder.MoveNext();

            builder.RollBack(3);
            builder.ForgetPreceding();

            //test
            string expected = "dfjk";
            CustomAssert.ProduceEqualValues(expected.GetEnumerator(), builder);
            Assert.AreEqual(expected, builder.BuildToken());
        }

        [TestMethod]
        public void StepBack_AfterMoveForward_ReturnsSameCharacterAgain()
        {
            //set up
            using var builder = new TokenBuilder("ab".GetEnumerator());
            builder.MoveNext();
            builder.MoveNext();

            //act
            builder.StepBack();

            //test
            Assert.AreEqual('a', builder.Current);
        }

        [TestMethod]
        public void Current_AfterPastEnd_ThrowsOutOfRangeException()
        {
            //set up
            using var builder = new TokenBuilder("".GetEnumerator());
            builder.MoveNext();

            //test
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => builder.Current);
        }
    }
}
