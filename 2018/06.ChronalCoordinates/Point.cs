using System.Linq;

namespace ChronalCoordinates
{
    class Point
    {
        public static Point Parse(string str)
        {
            int[] values = str.Split(',')
                .Select(int.Parse)
                .ToArray();

            return new Point(values[0], values[1]);
        }

        public Point(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public int X { get; }

        public int Y { get; }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as Point);
        }

        private bool Equals(Point other)
        {
            return other != null && 
                this.X == other.X &&
                this.Y == other.Y;
        }

        public override int GetHashCode()
        {
            return this.X ^ this.Y;
        }

        public override string ToString()
        {
            return this.X + "," + this.Y;
        }
    }
}