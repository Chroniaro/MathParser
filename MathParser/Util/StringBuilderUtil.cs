using System;
using System.Collections.Generic;
using System.Text;

namespace MathParser.Util
{
    public static class StringBuilderUtil
    {
        public delegate void CharConsumer(char c);

        //cannot be enumerator because Span cannot be used inside an enumerator
        public static void ForEachCharInRange(this StringBuilder builder, int startIndex, int length, CharConsumer consume)
        {
            int indexInChunk = startIndex;
            int remainingLength = length;

            foreach (var chunk in builder.GetChunks())
            {
                int chunkLength = chunk.Length;
                if (indexInChunk < chunkLength)
                {
                    var span = chunk.Span;

                    for (int i = indexInChunk; i < chunkLength; ++i, --remainingLength)
                    {
                        if (remainingLength <= 0)
                            return;

                        consume(span[i]);
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

            from.ForEachCharInRange(startIndex, length, c => to.Append(c));
            from.Remove(startIndex, length);

            return to;
        }
    }
}
