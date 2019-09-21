using MathParser.Lexer;
using MathParser.Parser;
using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("MathParserTests")]
namespace MathParser
{
    class Program
    {
        static void Main(string[] args)
        {
            string testString = "(1 + 2) *  3 ";

            var tokenizer = new Tokenizer()
                .UseDefaultLexers()
                .ReadFromSource(testString);

            var parser = new Parser.Parser();
            Console.WriteLine(parser.Parse(tokenizer.GetTokenStream()));

        }
    }
}
