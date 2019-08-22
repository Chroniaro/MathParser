using MathParser.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace MathParserTests
{
    [TestClass]
    public class StringBuilderUtilTest
    {
        [TestMethod]
        public void Range_NonChunky_ReturnsCorrectCharacters()
        {
            //set up
            var builder = new StringBuilder("01234567");

            //test
            int index = 2;
            foreach ((var chunk, int i) in builder.Range(2, 3))
            {
                Assert.AreEqual(index.ToString()[0], chunk.Span[i]);
                ++index;
            }
            Assert.AreEqual(5, index);
        }

        [TestMethod]
        public void Range_Chunky_ReturnsCorrectCharacters()
        {
            //set up
            var builder = new StringBuilder(0);
            for (int i = 0; i < 20; ++i)
                builder.Append("foo");

            var rangeEnumerator = builder.Range(6, 1).GetEnumerator();

            //test
            Assert.IsTrue(rangeEnumerator.MoveNext());
            (var chunk, int index) = rangeEnumerator.Current;
            Assert.AreEqual('f', chunk.Span[index]);
            Assert.IsFalse(rangeEnumerator.MoveNext());
        }

        private void TestMove(string initial, int startIndex, int length, string final, string moved)
        {
            //set up
            var builder = new StringBuilder(initial);

            //act
            var movedActual = builder.Move(new StringBuilder(), startIndex, length);

            //test
            Assert.AreEqual(final, builder.ToString());
            Assert.AreEqual(moved, movedActual.ToString());
        }

        [TestMethod]
        [DataRow("")]
        [DataRow("foo")]
        public void Move_ZeroLengthSegment_NoChange(string content)
        {
            TestMove(content, 0, 0, content, "");
        }

        [TestMethod]
        [DataRow("foobar", 1, 3, "far", "oob")]
        public void Move_InternalSegment_MovesSegmentToNewStringBuilder(string initial, int startIndex, int length, string final, string moved)
        {
            TestMove(initial, startIndex, length, final, moved);
        }

        [TestMethod]
        [DataRow("foobar", 0, 3, "bar", "foo")]
        [DataRow("foobar", 3, 3, "foo", "bar")]
        public void Move_EdgeSegment_MovesSegmentToNewStringBuilder(string initial, int startIndex, int length, string final, string moved)
        {
            TestMove(initial, startIndex, length, final, moved);
        }

        [TestMethod]
        public void Move_ToStringBuilderWithData_AppendsToEnd()
        {
            //set up
            var from = new StringBuilder("foobar");
            var to = new StringBuilder("potato");

            //act
            from.Move(to, 1, 2);

            //test
            Assert.AreEqual("fbar", from.ToString());
            Assert.AreEqual("potatooo", to.ToString());
        }

        [TestMethod]
        public void Move_Chunky_MovesSegmentToNewStringBuilder()
        {
            //set up
            var stringBuilder = new StringBuilder(0);
            for (int i = 0; i < 10; ++i)
                stringBuilder.Append("foo");

            //act
            var moved = stringBuilder.Move(new StringBuilder(), 7, 16);

            //test
            Assert.AreEqual("foofoofofoofoo", stringBuilder.ToString());
            Assert.AreEqual("oofoofoofoofoofo", moved.ToString());
        }

        [TestMethod]
        [DataRow("foo", -1, 2)]
        [DataRow("foo", 1, 5)]
        [DataRow("foo", 3, 5)]
        public void Move_OutOfBoundsRange_ThrowsIndexOutOfRangeException(string content, int startIndex, int length)
        {
            //set up
            var stringBuilder = new StringBuilder(content);

            //test
            Assert.ThrowsException<IndexOutOfRangeException>(() => stringBuilder.Move(new StringBuilder(), startIndex, length));
        }

        [TestMethod]
        [DataRow("foo", -1, -2)]
        [DataRow("foo", 1, -1)]
        [DataRow("foo", 3, -2)]
        public void Move_NegativeLength_ThrowsArgumentException(string content, int startIndex, int length)
        {
            //set up
            var stringBuilder = new StringBuilder(content);

            //test
            Assert.ThrowsException<ArgumentException>(() => stringBuilder.Move(new StringBuilder(), startIndex, length));
        }
    }
}
