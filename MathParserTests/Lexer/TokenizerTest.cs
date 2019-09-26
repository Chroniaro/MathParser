using MathParser.Lexer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MathParserTests.Lexer
{
    [TestClass]
    public class TokenizerTest
    {
        private IEnumerator<Token> GetTokenStream(string source, params ILexer[] lexers)
        {
            return new Tokenizer()
                .ReadFromSource(source)
                .UseLexers(lexers)
                .GetEnumerator();
        }

        [TestMethod]
        public void Current_BeforeMoveNext_ThrowsInvalidOperation()
        {
            //set up
            using var tokenStream = GetTokenStream("");
            
            //test
            Assert.ThrowsException<InvalidOperationException>(() => tokenStream.Current);
        }

        [TestMethod]
        public void EmptyString_NoTokens()
        {
            //set up
            var tokenStream = GetTokenStream("");

            //test
            Assert.IsFalse(tokenStream.MoveNext());
        }

        [TestMethod]
        public void MoveNext_NoLexersAccept_ThrowsInvalidDataException()
        {
            //set up
            var tokenStream = GetTokenStream("x");

            //test
            Assert.ThrowsException<InvalidDataException>(() => tokenStream.MoveNext());
        }

        [TestMethod]
        public void MoveNext_LexerAccepts_ProducesLexedToken()
        {
            //set up
            var tokenStream = GetTokenStream("foo", new MockTokenLexer() { Token = "foo" });

            //act
            tokenStream.MoveNext();

            //test
            Assert.IsTrue(tokenStream.Current is MockToken{ Content: "foo" });
        }

        public void MoveNext_TwoLexersMatch_FirstLexerPrioritized()
        {
            //set up
            var tokenStream = GetTokenStream("foo",
                new MockTokenLexer() { Name = "first", Token = "foo" },
                new MockTokenLexer() { Name = "second", Token = "foo" }
            );

            //act
            tokenStream.MoveNext();

            //test
            Assert.IsTrue(tokenStream.Current is MockToken{ ProducedBy: "first" });
        }

        public void MoveNext_FirstLexerFailsMatch_ResetsPositionForSecondLexer()
        {
            //set up
            var tokenStream = GetTokenStream("foo",
                new MockTokenLexer() { Token = "fob" },
                new MockTokenLexer() { Token = "foo" }
            );

            //act
            tokenStream.MoveNext();

            //test
            Assert.IsTrue(tokenStream.Current is MockToken{ Content: "foo" });
        }
    }
}
