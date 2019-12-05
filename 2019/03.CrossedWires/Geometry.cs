namespace CrossedWires
{
    using System;
    using System.Linq;

    static class Geometry
    {
        public static readonly int[] Origin = { 0, 0 };

        public static bool PointsEqual(int[] a, int[] b)
        {
            return a[0] == b[0] && a[1] == b[1];
        }

        public static int Distance(int[] a = null, int[] b = null)
        {
            if (a == null)
                a = Origin.ToArray();

            if (b == null)
                b = Origin.ToArray();

            return Math.Abs(a[0] - b[0]) + Math.Abs(a[1] - b[1]);
        }

        public static int[] Move(int[] position, int[] direction, int distance)
        {
            position[direction[0]] += direction[1] * distance;

            return position;
        }
    }
}