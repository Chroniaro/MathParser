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
        private void TestMove(string initial, int startIndex, int length, string final, string moved)
        {
            //set up
            var builder = new StringBuilder(initial);

            //act
            var movedActual = builder.Move(new StringBuilder(), startIndex, length);

            //test
            Assert.AreEqual(final, builder.ToString());
            Assert.AreEqual(moved, movedActual);
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
            Assert.AreEqual("oofoofoofoofoofo", moved);
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
