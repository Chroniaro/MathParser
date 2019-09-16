using System;
using System.Collections.Generic;
using System.Text;

namespace MathParser.Lexer
{
    internal class NumberLexer : AbstractLexer
    {
        public override Token? Lex(TokenBuilder builder)
        {
            try
            {
                if (Eval(builder) is double value)
                    return new NumberToken(value);
                else
                    return null;
            }
            catch (ArgumentOutOfRangeException) // Builder ran out of characters unexpectedly
            {
                return null;
            }
        }

        private double? Eval(TokenBuilder builder)
        {
            bool negative = IsNegative(builder);

            if (LexNumberUnsigned(builder) is double value)
                return negative ? -value : value;
            else
                return null;
        }

        private bool IsNegative(TokenBuilder builder)
        {
            builder.MoveNext();

            if (builder.Current == '-')
                return true;
            else if (builder.Current != '+')
                builder.StepBack();

            return false;
        }

        private double? LexNumberUnsigned(TokenBuilder builder)
        {
            builder.MoveNext();

            if (AsDigit(builder.Current) is int firstDigit)
                return LexNumberTail(firstDigit, builder);
            else
                return null;
        }

        private double LexNumberTail(int firstDigit, TokenBuilder builder)
        {
            double value = firstDigit;

            while (builder.MoveNext())
            {
                if (AsDigit(builder.Current) is int digit)
                    value = 10 * value + digit;
                else
                {
                    builder.StepBack();
                    break;
                }
            }

            return value;
        }

        private int? AsDigit(char c)
        {
            int value = c - '0';
            if (value < 0 || value > 9)
                return null;
            return value;
        }
    }
}
