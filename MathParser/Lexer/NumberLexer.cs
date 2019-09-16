﻿using System;
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
                if (LexNumber(builder) is double value)
                    return new NumberToken(value);
                else
                    return null;
            }
            catch (ArgumentOutOfRangeException) // Builder ran out of characters unexpectedly
            {
                return null;
            }
        }

        private double? LexNumber(TokenBuilder builder)
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
            double value = LexDigits(builder, out int digits);

            if (builder.MoveNext() && builder.Current == '.')
            {
                double valueAfterDecimal = LexDigits(builder, out int digitsAfterDecimal);
                for (int i = 0; i < digitsAfterDecimal; ++i)
                    valueAfterDecimal /= 10;

                value += valueAfterDecimal;
                digits += digitsAfterDecimal;
            }

            if (digits == 0)
                return null;

            return value;
        }

        private double LexDigits(TokenBuilder builder, out int numberOfDigits)
        {
            double value = 0;
            numberOfDigits = 0;

            while (builder.MoveNext())
            {
                if (AsDigit(builder.Current) is int digit)
                {
                    value = 10 * value + digit;
                    ++numberOfDigits;
                }
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
