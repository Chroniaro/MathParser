using MathParser.Lexer;
using System;
using System.Collections.Generic;
using System.Text;

namespace MathParserTests.Mocking
{
    internal class MockSkippableToken : MockToken, ISkippable
    {
        public MockSkippableToken(string producer, string content) 
            : base(producer, content)
        { }
    }
}
