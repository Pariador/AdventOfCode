namespace Triangles
{
    using System;
    using System.Linq;
    using System.IO;
    using System.Collections.Generic;

    public static class Program
    {
        public static void Main()
        {
            var triangles = ReadTriangles("input.txt");
            int count = triangles.Count(IsValidTriangle);

            Console.WriteLine($"Valid triangles: {count}");

            triangles = ReadTrianglesVertically(triangles);
            count = triangles.Count(IsValidTriangle);

            Console.WriteLine($"Valid vartical triangles: {count}");
        }

        public static bool IsValidTriangle(int[] triangle)
        {
            for (int i = 0; i < triangle.Length; i++)
            {
                int sidesSum = 0;

                for (int j = 0; j < triangle.Length; j++)
                {
                    if (i == j)
                    {
                        continue;
                    }

                    sidesSum += triangle[j];
                }

                if (triangle[i] >= sidesSum)
                {
                    return false;
                }
            }

            return true;
        }

        public static int[][] ReadTriangles(string fileName)
        {
            var triangles = new List<int[]>();

            using (var reader = new StreamReader(fileName))
            {
                while (true)
                {
                    string line = reader.ReadLine();
                    if (line == null)
                    {
                        break;
                    }

                    int[] triangle = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(int.Parse)
                        .ToArray();

                    triangles.Add(triangle);
                }
            }

            return triangles.ToArray();
        }

        public static int[][] ReadTrianglesVertically(int[][] data)
        {
            var triangles = new List<int[]>();

            for (int col = 0; col < data[0].Length; col++)
            {
                for (int set = 0; set < data.Length / 3; set++)
                {
                    int[] triangle = new int[3];

                    for (int i = 0; i < triangle.Length; i++)
                    {
                        int row = (set * 3) + i;

                        triangle[i] = data[row][col];
                    }

                    triangles.Add(triangle);
                }
            }

            return triangles.ToArray();
        }
    }
}