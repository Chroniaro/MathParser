using System;
using System.Collections.Generic;
using System.Text;

namespace MathParser.Util
{
    public static class StringBuilderUtil
    {
        public static IEnumerable<(ReadOnlyMemory<char>, int)> Range(this StringBuilder builder, int startIndex, int length)
        {
            if (length <= 0)
                yield break;

            int indexInChunk = startIndex;
            int remainingLength = length;

            foreach (var chunk in builder.GetChunks())
            {
                int chunkLength = chunk.Length;
                if (indexInChunk < chunkLength)
                {
                    for (int i = indexInChunk; i < chunkLength; ++i, --remainingLength)
                    {
                        if (remainingLength <= 0)
                            yield break;

                        yield return (chunk, i);
                    }

                    indexInChunk = 0;
                }
                else
                    indexInChunk -= chunkLength;
            }
        }

        public static StringBuilder Move(this StringBuilder from, StringBuilder to, int startIndex, int length)
        {
            if (length < 0)
                throw new ArgumentException("Length cannot be negative (recieved " + length + ")");



            return to;
        }
    }
}
