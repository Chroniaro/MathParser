using MathParser;
using MathParser.Lexer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MathParserTests.Lexer
{
    [TestClass]
    public class StreamReaderCharsEnumeratorTest
    {
        [TestMethod]
        [DataRow("")]
        [DataRow("foo")]
        [DataRow("f\no§b")]
        public void Chars_StreamFromUTF8StringBytes_EqualToCharactersOfString(string data)
        {
            //set up
            var encoding = Encoding.UTF8;
            var bytes = encoding.GetBytes(data);
            var stream = new MemoryStream(bytes);
            var reader = new StreamReader(stream, encoding);

            //act
            var chars = reader.Chars();

            //test
            CustomAssert.ProduceEqualValues(data.GetEnumerator(), chars.GetEnumerator());
        }
    }
}
