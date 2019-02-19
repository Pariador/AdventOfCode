namespace ChronalCoordinates
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Collections.Generic;

    static class Program
    {
        static void Main()
        {
            string file = "input.txt";
            const int MaxDistance = 10000;

            Point[] points = File.ReadAllLines(file)
                     .Select(Point.Parse)
                     .ToArray();

            Border border = Border.FindBorder(points);

            Point[] finite = RemoveInfinitePoints(points, border);

            int maxAreaCloseToPoint = finite.Select(point => FindAreaCloseTo(point, points, border))
                .Max();

            int areaCloseToAllPoints = FindAreaCloseToAll(points, border, MaxDistance);

            Console.WriteLine($"Max size area close to point: {maxAreaCloseToPoint}");
            Console.WriteLine($"Area size closest to all points: {areaCloseToAllPoints}");
        }

        static Point[] RemoveInfinitePoints(Point[] points, Border border = null)
        {
            var result = new List<Point>(points);

            if (border == null)
            {
                border = Border.FindBorder(points);
            }

            for (int x = border.TopLeft.X; x <= border.BotRight.X; x++)
            {
                Point top = new Point(x, border.TopLeft.Y);
                Point bot = new Point(x, border.BotRight.Y);

                Point nearestToTop = Utils.FindNearestPoint(top, points);
                Point nearestToBot = Utils.FindNearestPoint(bot, points);

                if (nearestToTop != null)
                {
                    result.Remove(nearestToTop);
                }

                if (nearestToBot != null)
                {
                    result.Remove(nearestToBot);
                }
            }

            for (int y = border.TopLeft.Y; y <= border.BotRight.Y; y++)
            {
                Point left = new Point(border.TopLeft.X, y);
                Point right = new Point(border.BotRight.X, y);

                Point nearestToLeft = Utils.FindNearestPoint(left, points);
                Point nearestToRight = Utils.FindNearestPoint(right, points);

                if (nearestToLeft != null)
                {
                    result.Remove(nearestToLeft);
                }

                if (nearestToRight != null)
                {
                    result.Remove(nearestToRight);
                }
            }

            return result.ToArray();
        }

        static int FindAreaCloseTo(Point point, Point[] points, Border border = null)
        {
            int area = 0;

            if (border == null)
            {
                border = Border.FindBorder(points);
            }

            var queue = new Queue<Point> ();
            var visited = new HashSet<Point>();
            queue.Enqueue(point);
            visited.Add(point);

            while (queue.Any())
            {
                Point current = queue.Dequeue();
                Point nearest = Utils.FindNearestPoint(current, points);

                if (nearest != point)
                {
                    continue;
                }

                if ((current.X == border.TopLeft.X || current.X == border.BotRight.X) ||
                    (current.Y == border.TopLeft.Y || current.Y == border.BotRight.Y))
                {
                    return -1;
                }

                area++;

                Point[] neighbors =
                {
                    new Point(current.X - 1, current.Y ),
                    new Point(current.X + 1, current.Y ),
                    new Point(current.X, current.Y - 1 ),
                    new Point(current.X, current.Y + 1 ),
                };

                foreach (var neighbor in neighbors.Where(neighbor => !visited.Contains(neighbor)))
                {
                    queue.Enqueue(neighbor);
                    visited.Add(neighbor);
                }
            }

            return area;
        }

        static int FindAreaCloseToAll(Point[] points, Border border, int maxDistance)
        {
            int size = 0;

            for (int x = border.TopLeft.X; x < border.BotRight.X; x++)
            {
                for (int y = border.TopLeft.Y; y < border.BotRight.Y; y++)
                {
                    Point point = new Point(x, y);

                    int totalDistance = points.Select(p => Utils.Distance(point, p))
                        .Sum();

                    if (totalDistance < maxDistance)
                    {
                        size++;
                    }
                }
            }

            return size;
        }
    }
}