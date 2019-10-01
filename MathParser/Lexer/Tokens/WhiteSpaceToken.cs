﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MathParser.Lexer
{
    public class WhitespaceToken : DelimiterToken, ISkippable
    {
        public WhitespaceToken(string value) : base(value)
        { }
    }
}
