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
            string testString = "1 + 2 * 3 * 4 + 5 * 6 + 7  + 8";

            var tokenizer = new Tokenizer()
                .UseDefaultLexers()
                .ReadFromSource(testString);

            var parser = IInfixParser.CreateFromOrderOfOperations(
                new ValueParser(),
                new ProductParser(),
                new SumParser()
            );

            Console.WriteLine(parser.Parse(tokenizer.GetTokenStream()));

            //foreach (var token in tokenizer)
            //    Console.WriteLine(token);
        }
    }
}
