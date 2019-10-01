using System;
using System.Collections.Generic;
using System.Text;
using MathParser.Lexer;

namespace MathParserTests.Mocking
{
    internal class MockSkippableTokenLexer : MockTokenLexer
    {
        protected override Token MakeToken(string token) =>
            new MockSkippableToken(Name, token);
    }
}
