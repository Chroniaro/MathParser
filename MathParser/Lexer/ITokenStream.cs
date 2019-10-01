using MathParser.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace MathParser.Lexer
{
    public interface ITokenStream : ILookbackEnumerator<Token>
    { }
}
