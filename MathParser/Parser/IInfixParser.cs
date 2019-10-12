using System;
using System.Collections.Generic;
using System.Text;

namespace MathParser.Parser
{
    interface IInfixParser : IParser
    {
        public IParser? TermParser { get; set; }

        public static IParser CreateFromOrderOfOperations(IParser lowestTermParser, params IInfixParser[] operations)
        {
            var termParser = lowestTermParser;

            foreach (var operation in operations)
            {
                operation.TermParser = termParser;
                termParser = operation;
            }

            return termParser;
        }
    }
}
