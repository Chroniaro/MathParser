using MathParser.Lexer;
using System;
using System.Collections.Generic;
using System.Text;

namespace MathParserTests.Lexer
{
    internal class MockTokenLexer : AbstractLexer
    {
        public string Name { get; set; } = "MockTokenLexer";
        public string Token { get; set; } = null;

        public override Token Lex(TokenBuilder builder)
        {
            if (Token != null && builder.TryMatch(Token))
                return new MockToken(Name, Token);
            else
                return null;
        }

        public override string ToString()
        {
            return Name + "[" + Token + "]";
        }
    }
}
