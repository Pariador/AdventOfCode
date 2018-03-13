namespace Explosives
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Text.RegularExpressions;

    public static class Program
    {
        public static void Main()
        {
            string file = ReadFile("input.txt");

            //non-recursive decompression
            var decompressed = Decompression.Decompress(file);

            Console.WriteLine($"Basic decompression file length: {CalcLength(file)}");
            Console.WriteLine($"Recursive decompression file length: {CalcRecursiveLength(file)}");
        }

        public static long CalcRecursiveLength(string compressed)
        {
            long length = compressed.Length;

            Marker marker = Marker.FindMarker(compressed);
            while (marker != null)
            {
                long decompressedSequenceLength = CalcRecursiveLength(compressed.Substring(marker.SequenceStart, marker.SequenceLength));

                length += (decompressedSequenceLength * marker.RepeatCount) - marker.Length - marker.SequenceLength;

                marker = Marker.FindMarker(compressed, marker.SequenceStart + marker.SequenceLength);
            }

            return length;
        }

        public static int CalcLength(string compressed)
        {
            int length = compressed.Length;

            Marker marker = Marker.FindMarker(compressed);
            while (marker != null)
            {
                length += (marker.SequenceLength * (marker.RepeatCount - 1)) - marker.Length;

                marker = Marker.FindMarker(compressed, marker.SequenceStart + marker.SequenceLength);
            }

            return length;
        }

        public static string ReadFile(string fileName)
        {
            using (var reader = new StreamReader(fileName))
            {
                return reader.ReadToEnd();
            }
        }
    }
}