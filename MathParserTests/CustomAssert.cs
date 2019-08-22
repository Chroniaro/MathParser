using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MathParserTests
{
    static class CustomAssert
    {
        public static void ProduceEqualValues<T>(IEnumerator<T> expected, IEnumerator<T> actual)
        {
            while (expected.MoveNext())
            {
                Assert.IsTrue(actual.MoveNext());
                Assert.AreEqual(expected.Current, actual.Current);
            }

            Assert.IsFalse(actual.MoveNext());
        }
    }
}
