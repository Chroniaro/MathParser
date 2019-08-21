using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MathParserTests
{
    static class CustomAssertions
    {
        public static void AssertEqual<T>(IEnumerable<T> expected, IEnumerable<T> actual)
        {
            Assert.AreEqual(expected.Count(), actual.Count());
            foreach ((T e, T a) in expected.Zip(actual))
            {
                Assert.AreEqual(e, a);
            }
        }
    }
}
