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
        private MockCharacterSetLexer GetLexer(string chars) =>
            new MockCharacterSetLexer()
                .UseCharacters(chars);

        private TokenBuilder GetTokenBuilder(string input) =>
            new TokenBuilder(input.GetEnumerator());

        private void TestLexing(string input, string chars, string firstToken)
        {
            //set up
            var lexer = GetLexer(chars);
            var tokenBuilder = GetTokenBuilder(input);

            //act
            var token = lexer.Lex(tokenBuilder);

            //test
            Assert.AreEqual(firstToken, token.Content);
        }

        [TestMethod]
        [DataRow("abc", "a")]
        [DataRow("xyz", "y")]
        public void Lex_WithSingleIncludedChar_ReturnsTokenOfTheChar(string chars, string singleChar)
        {
            TestLexing(singleChar, chars, singleChar);
        }

        [TestMethod]
        [DataRow("xyz", "xxyzzzxy")]
        public void Lex_WithMultipleIncludedChars_GroupsIntoOneToken(string chars, string multipleChars)
        {
            TestLexing(multipleChars, chars, multipleChars);
        }

        [TestMethod]
        [DataRow("xyz", "xyyzxABC", "xyyzx")]
        public void Lex_IncludedCharsThenNonIncludedChars_ReturnsOneTokenWithIncludedChars(string chars, string input, string expectedToken)
        {
            TestLexing(input, chars, expectedToken);
        }

        [TestMethod]
        [DataRow("xyz", "ABC")]
        public void Lex_WithNonIncludedChar_ReturnsNull(string chars, string input)
        {
            //set up
            var lexer = GetLexer(chars);
            var tokenBuilder = GetTokenBuilder(input);

            //act
            var token = lexer.Lex(tokenBuilder);

            //test
            Assert.IsNull(token);
        }
    }
}
