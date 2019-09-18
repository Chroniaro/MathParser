using MathParser.Lexer;
using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("MathParserTests")]
namespace MathParser
{
    class Program
    {
        static void Main(string[] args)
        {
            string testString = "(1 +  2)/3 * [4x_i-7]";

            var tokenizer = new Tokenizer()
                .UseDefaultLexers()
                .ReadFromSource(testString);

            foreach (var token in tokenizer)
                if (!(token is WhitespaceToken))
                    Console.WriteLine(token);
        }
    }
}
