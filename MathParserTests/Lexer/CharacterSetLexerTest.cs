using MathParser.Lexer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace MathParserTests.Lexer
{
    [TestClass]
    public class CharacterSetLexerTest
    {
        private void TestLexing(string input, string firstWhitespace)
        {
            //set up
            var whitespaceLexer = new WhitespaceLexer()
                .UseDefaultWhitespaceCharacters();
            var tokenBuilder = new TokenBuilder(input.GetEnumerator());

            //act
            var token = whitespaceLexer.Lex(tokenBuilder);

            //test
            Assert.IsTrue(token is WhitespaceToken);
            Assert.AreEqual(token.Content, firstWhitespace);
        }

        [TestMethod]
        [DataRow(" ")]
        [DataRow("\t")]
        [DataRow("\n")]
        public void Lex_WithSingleWhitespaceChar_ReturnsTokenOfTheWhitespaceChar(string whitespaceChar)
        {
            TestLexing(whitespaceChar, whitespaceChar);
        }

        [TestMethod]
        [DataRow("  ")]
        [DataRow("\t\n  \t")]
        public void Lex_WithMultipleWhitespaceChars_GroupsWhitespaceIntoOneToken(string whitespaceChars)
        {
            TestLexing(whitespaceChars, whitespaceChars);
        }

        [TestMethod]
        [DataRow(" \tfoo", " \t")]
        [DataRow("\na", "\n")]
        public void Lex_WithWhitespaceFollowedByNonWhitespace_ReturnsOneTokenWithWhitespace(string input, string firstWhitespace)
        {
            TestLexing(input, firstWhitespace);
        }

        [TestMethod]
        [DataRow("foo")]
        public void Lex_WithNonWhitespace_ReturnsNull(string input)
        {
            //set up
            var whitespaceLexer = new WhitespaceLexer()
                .UseDefaultWhitespaceCharacters();
            var tokenBuilder = new TokenBuilder(input.GetEnumerator());

            //act
            var token = whitespaceLexer.Lex(tokenBuilder);

            //test
            Assert.IsNull(token);
        }
    }
}
