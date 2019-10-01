using MathParser.Lexer;
using MathParser.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace MathParserTests.Mocking
{
    internal class MockTokenStream : AbstractLookbackEnumerator<Token>, ITokenStream
    {
        public MockTokenStream(params Token[] tokens)
        {
            LoadedValues.AddRange(tokens);
        }

        protected override void LoadMoreValues()
        { }
    }
}
