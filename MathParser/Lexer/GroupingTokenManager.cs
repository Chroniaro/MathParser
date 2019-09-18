using System;
using System.Collections.Generic;
using System.Text;

namespace MathParser.Lexer
{
    public class GroupingTokenManager
    {
        public IDictionary<string, string> LeftToRightMap { get; }
        public IDictionary<string, string> RightToLeftMap { get; }

        public GroupingTokenManager()
        {
            LeftToRightMap = new Dictionary<string, string>(10);
            RightToLeftMap = new Dictionary<string, string>(10);
        }

        public GroupingTokenManager UseGroupingPair(string leftToken, string rightToken)
        {
            LeftToRightMap.Add(leftToken, rightToken);
            RightToLeftMap.Add(rightToken, leftToken);

            return this;
        }

        public LeftGroupingToken CreateLeft(string token)
        {
            return new LeftGroupingToken(token, LeftToRightMap[token]);
        }

        public RightGroupingToken CreateRight(string token)
        {
            return new RightGroupingToken(token, RightToLeftMap[token]);
        }
    }
}
