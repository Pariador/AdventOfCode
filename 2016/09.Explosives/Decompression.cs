namespace Explosives
{
    using System.Text;

    public static class Decompression
    {
        public static string Decompress(string compressed)
        {
            StringBuilder decompressed = new StringBuilder();

            Marker marker = null;
            for (int i = 0; i < compressed.Length; i = marker == null ? i + 1 : marker.SequenceStart + marker.SequenceLength)
            {
                marker = Marker.ReadMarker(compressed, i);

                if (marker == null)
                {
                    decompressed.Append(compressed[i]);
                    continue;
                }

                decompressed.Append(Decompress(compressed, marker));
            }

            return decompressed.ToString();
        }

        public static string Decompress(string str, Marker marker)
        {
            StringBuilder builder = new StringBuilder();

            string sequence = str.Substring(marker.SequenceStart, marker.SequenceLength);

            for (int i = 0; i < marker.RepeatCount; i++)
            {
                builder.Append(sequence);
            }

            return builder.ToString();
        }
    }
}