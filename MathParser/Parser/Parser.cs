using MathParser.ExpressionTree;
using MathParser.Lexer;
using System;
using System.Collections.Generic;
using System.Text;

namespace MathParser.Parser
{
    public class Parser
    {
        public Expression Parse(TokenStream tokens)
        {
            GoToNext(tokens);
            return ParseSum(tokens);
        }

        private Expression ParseSum(TokenStream tokens)
        {
            var sum = new Sum();
            var firstExpression = ParseProduct(tokens);
            var result = firstExpression;

            while (true)
            {
                if (tokens.Current is DelimiterToken token)
                {
                    if (token.Content == "+")
                    {
                        if (result == firstExpression)
                        {
                            sum.Terms.Add(firstExpression);
                            result = sum;
                        }

                        GoToNext(tokens);

                        sum.Terms.Add(ParseProduct(tokens));
                    }
                    else
                        return result;
                }
                else if (tokens.Current is WhitespaceToken)
                {
                    if (!tokens.MoveNext())
                        return result;
                }
                else
                    return result;
            }
        }

        private Expression ParseProduct(TokenStream tokens)
        {
            var product = new Product();
            var firstExpression = ParseExpression(tokens);
            var result = firstExpression;

            while (true)
            {
                if (!tokens.MoveNext())
                    return result;

                if (tokens.Current is WhitespaceToken)
                    continue;

                if (tokens.Current is DelimiterToken token)
                {
                    if (token.Content == "*")
                    {
                        if (result == firstExpression)
                        {
                            product.Terms.Add(firstExpression);
                            result = product;
                        }

                        GoToNext(tokens);

                        product.Terms.Add(ParseExpression(tokens));
                    }
                    else
                        return result;
                }
            }
        }

        private Expression ParseExpression(TokenStream tokens)
        {
            if (tokens.Current is LeftGroupingToken leftToken)
            {
                string expectedRightToken = leftToken.Opposite;

                var innerExpression = Parse(tokens);

                if (tokens.Current is RightGroupingToken rightToken)
                {
                    if (expectedRightToken == rightToken.Content)
                        return innerExpression;
                    else
                        throw new FormatException("Expected " + expectedRightToken + " but got " + rightToken.Content);
                }
                else
                    throw new FormatException("Expeted " + expectedRightToken + " at end of expression " + innerExpression);
            }
            else if (tokens.Current is NumberToken number)
                return new RealNumber() { Value = number.Value };
            else
                throw new FormatException("Expected expression but found " + tokens.Current);
        }

        private void GoToNext(TokenStream tokens)
        {
            while (true)
            {
                if (!tokens.MoveNext())
                    throw new FormatException("Ran out of tokens unexpectedly.");

                if (!(tokens.Current is WhitespaceToken))
                    break;
            }
        }
    }
}
