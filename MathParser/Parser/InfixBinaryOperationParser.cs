using System;
using System.Collections.Generic;
using System.Text;
using MathParser.LanguageModel;
using MathParser.Lexer;
using MathParser.Util;

namespace MathParser.Parser
{
    public abstract class InfixBinaryOperationParser : IInfixParser
    {
        public abstract string OperatorDelimiter { get; }

        public IParser? TermParser { get; set; }

        public abstract Expression CreateOperationExpression(Expression left, Expression right);

        private bool TryParseTerm(ITokenStream tokens, out Expression? term)
        {
            if (TermParser == null)
                throw new ArgumentNullException("Term parser must be set before parsing");

            term = TermParser.Parse(tokens);
            if (term == null)
            {
                tokens.Reset();
                return false;
            }
            else
            {
                tokens.ForgetPreceding();
                return true;
            }
        }

        private bool MatchesOperation(ITokenStream tokens)
        {
            if (!tokens.MoveNext())
                return false;

            if (tokens.Current is DelimiterToken delimiterToken)
                if (delimiterToken.Content == OperatorDelimiter)
                    return true;

            return false;
        }

        public Expression? Parse(ITokenStream tokens)
        {
            if (!TryParseTerm(tokens, out var left))
                return null;

            while (MatchesOperation(tokens))
                if (TryParseTerm(tokens, out var right))
                    left = CreateOperationExpression(left!, right!);
                else
                    break;

            return left;
        }
    }
}
