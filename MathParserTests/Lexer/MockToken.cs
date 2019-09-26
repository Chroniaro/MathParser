using MathParser.Lexer;
using System;
using System.Collections.Generic;
using System.Text;

namespace MathParserTests.Lexer
{
    internal class MockToken : Token
    {
        public string ProducedBy { get; }

        public MockToken(string producer, string content) :
            base(content)
        {
            ProducedBy = producer;
        }
    }
}
