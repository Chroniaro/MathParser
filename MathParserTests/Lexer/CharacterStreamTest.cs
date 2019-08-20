using MathParser;
using MathParser.Lexer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MathParserTests.Lexer
{
    [TestClass]
    public class CharacterStreamTest
    {
        [TestMethod]
        public void Next_UTF8String_ReturnsEachCharThenNull()
        {
            //set up
            var encoding = Encoding.UTF8;
            string str = "f\no§b";
            var bytes = encoding.GetBytes(str);
            var stream = new MemoryStream(bytes);
            var reader = new StreamReader(stream, encoding);
            var charStream = new CharacterStream(reader);

            //test
            Assert.AreEqual('f', charStream.Next());
            Assert.AreEqual('\n', charStream.Next());
            Assert.AreEqual('o', charStream.Next());
            Assert.AreEqual('§', charStream.Next());
            Assert.AreEqual('b', charStream.Next());
            Assert.AreEqual(null, charStream.Next());
            Assert.AreEqual(null, charStream.Next()); //This could continue
        }
    }
}
