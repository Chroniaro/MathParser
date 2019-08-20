using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Tests.Lexer
{
    public class CharacterStreamTest
    {
        public void TestNext()
        {

            var t = new Thing();
            t.DoThing();

            var str = "foo bar";
            var bytes = Encoding.UTF8.GetBytes(str);
            var stream = new MemoryStream(bytes);
            var streamReader = new StreamReader(stream);
            var charStream = new CharacterStream(streamReader);

        }
    }
}
