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
        [TestMethod]
        [DataRow(new string[] {
            "+", "-", "*", "/", "foo", "--"
        })]
        public void Lex_RecognizesDelimiters(string[] delimiters)
        {
            //set up
            var lexer = new DelimiterLexer().UseDelimiters(delimiters);

            foreach (string target in delimiters)
            {
                var tokenizer = new Tokenizer()
                    .ReadFromSource(target)
                    .UseLexer(lexer);

                var tokenStream = tokenizer.GetEnumerator();

                //act
                tokenStream.MoveNext();
                var token = tokenStream.Current;

                //test
                Assert.IsTrue(token is DelimiterToken);
                Assert.AreEqual(target, token.Content);
                Assert.IsFalse(tokenStream.MoveNext());
            }
        }
    }
}
