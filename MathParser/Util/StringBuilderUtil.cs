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
                throw new ArgumentException("Length cannot be negative");

            if (startIndex < 0)
                throw new IndexOutOfRangeException("Starting index cannot be negative");

            if (startIndex + length > from.Length)
                throw new IndexOutOfRangeException("Range extends beyond end of StringBuilder");

            foreach ((var chunk, int i) in from.Range(startIndex, length))
                to.Append(chunk.Span[i]);

            from.Remove(startIndex, length);

            return to;
        }
    }
}
