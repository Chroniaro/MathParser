using System;
using System.Collections.Generic;
using System.Text;

namespace MathParser.Lexer
{
    internal class NumberLexer : ILexer
    {
        public Token? Lex(TokenBuilder builder)
        {
            if (Eval(builder) is double value)
                return new NumberToken(value);
            else
                return null;
        }

        private double? Eval(TokenBuilder builder)
        {
            bool negative = false;

            if (!builder.MoveNext())
                return null;

            if (builder.Current == '-')
                negative = true;
            else if (builder.Current != '+')
                builder.StepBack();

            if (!builder.MoveNext())
                return null;

            if (AsDigit(builder.Current) is int firstDigit)
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
                builder.StepBack();
                builder.PopToken();

                if (negative)
                    value = -value;

                return value;
            }
            else
                return null;
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
