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
            Assert.AreEqual(testString.Substring(0, chars), builder.CollectPreceding());
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
        public void ForgetProcedeing_WithNoProcedeing_DoesNotThrowExceptions()
        {
            //set up
            using var builder = new TokenBuilder("".GetEnumerator());

            //act
            builder.ForgetPreceding();
            builder.ForgetPreceding();

            //Nothing to assert, this is just to test that no exceptions were thrown
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

        [TestMethod]
        public void TryMatchNextChar_NextCharMatches_True()
        {
            //set up
            using var builder = new TokenBuilder("x".GetEnumerator());

            //test
            Assert.IsTrue(builder.TryMatchNext('x'));
        }

        [TestMethod]
        public void TryMatchNextChar_NextCharDoesNotMatch_False()
        {
            //set up
            using var builder = new TokenBuilder("x".GetEnumerator());

            //test
            Assert.IsFalse(builder.TryMatchNext('y'));
        }

        [TestMethod]
        public void TryMatchNextChar_NoNextChar_False()
        {
            //set up
            using var builder = new TokenBuilder("".GetEnumerator());

            //test
            Assert.IsFalse(builder.TryMatchNext('x'));
        }

        [TestMethod]
        public void TryMatchCurrentChar_CurrentCharMatches_True()
        {
            //set up
            using var builder = new TokenBuilder("x".GetEnumerator());
            builder.MoveNext();

            //test
            Assert.IsTrue(builder.TryMatchCurrent('x'));
        }

        [TestMethod]
        public void TryMatchNextChar_CurrentCharDoesNotMatch_False()
        {
            //set up
            using var builder = new TokenBuilder("x".GetEnumerator());
            builder.MoveNext();

            //test
            Assert.IsFalse(builder.TryMatchNext('y'));
        }

        [TestMethod]
        public void TryMatchNextChar_NoCurrent_False()
        {
            //set up
            using var builder = new TokenBuilder("".GetEnumerator());

            //test
            Assert.IsFalse(builder.TryMatchCurrent('x'));
        }

        [TestMethod]
        [DataRow("foobar", "foo")]
        public void TryMatchString_StartsWithMatchingString_True(string data, string token)
        {
            //set up
            using var builder = new TokenBuilder(data.GetEnumerator());

            //test
            Assert.IsTrue(builder.TryMatch(token));
        }

        [TestMethod]
        [DataRow("potato", "soup")]
        [DataRow("foo", "foobar")]
        public void TryMatchString_DoesNotStartWithMatchingString_False(string data, string token)
        {
            //set up
            using var builder = new TokenBuilder(data.GetEnumerator());

            //test
            Assert.IsFalse(builder.TryMatch(token));
        }
    }
}
