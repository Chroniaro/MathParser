using MathParser.Lexer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace MathParserTests.Lexer
{
    [TestClass]
    public class DelimiterLexerTest
    {
        private void TestLexing(string[] options, string delimiter)
        {
            //set up
            var delimiterLexer = new DelimiterLexer()
                .UseDelimiters(options);
            var tokenBuilder = new TokenBuilder(delimiter.GetEnumerator());

            //act
            var token = (DelimiterToken)delimiterLexer.Lex(tokenBuilder);

            //test
            Assert.AreEqual(delimiter, token.Content);
            Assert.IsFalse(tokenBuilder.MoveNext());
        }

        [TestMethod]
        [DataRow(new string[] { "+", "-", "foo" }, "-")]
        [DataRow(new string[] { "flip", "flop", "fling"}, "fling")]
        [DataRow(new string[] { "z", "y", "x" }, "z")]
        public void Lex_DelimiterInOptions_RecognizesDelimiter(string[] options, string delimiter)
        {
            TestLexing(options, delimiter);
        }

        [TestMethod]
        [DataRow(new string[] { "++", "+", "++++", "+++" }, "++++" )]
        [DataRow(new string[] { "foo", "soup", "foobar", "foobarbaz" }, "foobar" )]
        public void Lex_DelimiterBeginningIsAlsoAnOption_ChoosesLongerDelimiter(string[] options, string delimiter)
        {
            TestLexing(options, delimiter);
        }

        [TestMethod]
        [DataRow(new string[] { "foo", "bar"}, "baz")]
        public void Lex_StringContainsNoDelimiters_ReturnsNull(string[] options, string testString)
        {
            //set up
            var delimiterLexer = new DelimiterLexer()
                .UseDelimiters(options);
            var tokenBuilder = new TokenBuilder(testString.GetEnumerator());

            //act
            var token = delimiterLexer.Lex(tokenBuilder);

            //test
            Assert.IsNull(token);
        }

        private class CustomDelimiterToken : DelimiterToken
        {
            public CustomDelimiterToken(string content) :
                base(content)
            { }
        }

        [TestMethod]
        [DataRow(new string[] { "foo", "bar" }, "baz")]
        public void Lex_CustomTokenType_CallsCorrectConstructor(string[] otherOptions, string delimiter)
        {
            //set up
            var delimiterLexer = new DelimiterLexer()
                .UseDelimiters(otherOptions)
                .UseDelimiters(content => new CustomDelimiterToken(content), delimiter);
            var tokenBuilder = new TokenBuilder(delimiter.GetEnumerator());

            //act
            var token = delimiterLexer.Lex(tokenBuilder);

            //test
            Assert.IsTrue(token is CustomDelimiterToken);
            Assert.AreEqual(delimiter, token.Content);
            Assert.IsFalse(tokenBuilder.MoveNext());
        }
    }
}
