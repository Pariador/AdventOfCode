namespace CrossedWires
{
    using System;
    using System.IO;
    using System.Linq;

    using static Geometry;

    static class Program
    {
        static void Main()
        {
            var wires = File.ReadAllLines("input.txt")
                .Select(line => line.Split(',').ToArray())
                .Select(Wire.Parse)
                .ToArray();

            var crossPoints = CrossPoint.Find(wires[0], wires[1])
                .Where(cp => !PointsEqual(cp.Point, Origin))
                .ToArray();

            int minDistance = crossPoints.Min(cp => Distance(Origin, cp.Point));
            int minLength = crossPoints.Min(cp => cp.TotalOriginLength);

            Console.WriteLine($"Closest cross point: {minDistance}");
            Console.WriteLine($"Shortest cross point: {minLength}");
        }
    }
}