namespace ChronalCoordinates
{
    using System;
    using System.Linq;

    static class Utils
    {
        public static int Distance(Point first, Point second)
        {
            return Math.Abs(first.X - second.X) + Math.Abs(first.Y - second.Y);
        }

        public static Point FindNearestPoint(Point from, Point[] points)
        {
            int[] distances = points.Select(point => Distance(from, point))
                .ToArray();

            int minIndex = 0;
            bool unique = true;
            for (int i = 1; i < distances.Length; i++)
            {
                if (distances[i] < distances[minIndex])
                {
                    minIndex = i;
                    unique = true;
                }
                else if (distances[i] == distances[minIndex])
                {
                    unique = false;
                }
            }

            if (!unique)
            {
                return null;
            }

            return points[minIndex];
        }
    }
}