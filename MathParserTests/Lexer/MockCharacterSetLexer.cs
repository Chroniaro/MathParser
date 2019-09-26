using MathParser.Lexer;
using System;
using System.Collections.Generic;
using System.Text;

namespace MathParserTests.Lexer
{
    internal class MockCharacterSetLexer : CharacterSetLexer
    {
        public string Name { get; set; } = "MockCharacterSetLexer";

        protected override Token ConstructToken(string chars)
        {
            return new MockToken(Name, chars);
        }

        public new MockCharacterSetLexer UseCharacters(IEnumerable<char> chars) =>
            (MockCharacterSetLexer)base.UseCharacters(chars);

        public override string ToString()
        {
            return Name;
        }
    }
}
