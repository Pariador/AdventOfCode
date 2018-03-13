namespace Explosives
{
    public class Marker
    {
        private const char Open = '(';

        private const char Divider = 'x';

        private const char Close = ')';

        public static Marker FindMarker(string str, int start = 0)
        {
            for (int i = start; i < str.Length; i++)
            {
                Marker marker = ReadMarker(str, i);

                if (marker != null)
                {
                    return marker;
                }
            }

            return null;
        }

        public static Marker ReadMarker(string str, int start)
        {
            Utils.ValidateIndex(start, str.Length);

            if (str[start] != Marker.Open)
            {
                return null;
            }

            int? sequenceLength = ReadNumber(str, start + 1, Marker.Divider, out int? stopIndex);
            if (sequenceLength == null)
            {
                return null;
            }

            int? repeatCount = ReadNumber(str, ((int)stopIndex) + 1, Marker.Close, out stopIndex);
            if (repeatCount == null)
            {
                return null;
            }

            int sequenceStart = ((int)stopIndex) + 1;

            return new Marker
            (
                start,
                length: sequenceStart - start,
                sequenceStart: sequenceStart,
                sequenceLength: (int)sequenceLength,
                repeatCount: (int)repeatCount
            );
        }

        private static int? ReadNumber(string str, int start, char stop, out int? stopIndex)
        {
            int index = start;
            do
            {
                if (index >= str.Length || !char.IsDigit(str[index]))
                {
                    stopIndex = null;
                    return null;
                }

                index++;
            }
            while (str[index] != stop);

            stopIndex = index;
            return int.Parse(str.Substring(start, index - (start)));
        }

        public Marker(int start, int length, int sequenceStart, int sequenceLength, int repeatCount)
        {
            this.Start = start;
            this.Length = length;
            this.SequenceStart = sequenceStart;
            this.SequenceLength = sequenceLength;
            this.RepeatCount = repeatCount;
        }

        public int Start { get; }

        public int Length { get; }

        public int SequenceStart { get; }

        public int SequenceLength { get; }

        public int RepeatCount { get; }
    }
}