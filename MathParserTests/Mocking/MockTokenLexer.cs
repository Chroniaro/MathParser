using MathParser.Lexer;
using System;
using System.Collections.Generic;
using System.Text;

namespace MathParserTests.Mocking
{
    internal class MockTokenLexer : AbstractLexer
    {
        public string Name { get; set; } = "MockTokenLexer";
        public string Token { get; set; } = null;

        protected virtual Token MakeToken(string token) =>
            new MockToken(Name, token);

        public override Token Lex(TokenBuilder builder)
        {
            if (Token != null && builder.TryMatch(Token))
                return MakeToken(Token);
            else
                return null;
        }

        public override string ToString()
        {
            return Name + "[" + Token + "]";
        }
    }
}
