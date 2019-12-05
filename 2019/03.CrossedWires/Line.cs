namespace CrossedWires
{
    class Line
    {
        public Line(int[] from, int[] to, int originLength)
        {
            this.From = from;
            this.To = to;
            this.OriginLength = originLength;
        }

        public int[] From { get; }

        public int[] To { get; }

        public int OriginLength { get; }

        public bool IsHorizontal
        {
            get 
            {
                return this.From[1] == this.To[1];
            }
        }
    }
}