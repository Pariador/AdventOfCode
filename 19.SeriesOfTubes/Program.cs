namespace SeriesOfTubes
{
    using System;
    using System.IO;
    using System.Collections.Generic;

    public static class Program
    {
        public static void Main()
        {
            char[,] map = ReadMap("input.txt");

            Traverser traverser = new Traverser(map);

            string visited = traverser.Traverse(out int stepsCount);

            Console.WriteLine($"Visited: {visited}");
            Console.WriteLine($"Steps Count: {stepsCount}");
        }

        public static char[,] ReadMap(string file)
        {
            var lines = new List<string>();

            using (var reader = new StreamReader(file))
            {
                while (true)
                {
                    string line = reader.ReadLine();
                    if (line == null)
                    {
                        break;
                    }

                    lines.Add(line);
                }
            }

            return ToMap(lines);
        }

        public static char[,] ToMap(List<string> lines)
        {
            char[,] map = new char[lines.Count, lines[0].Length];

            for (int row = 0; row < map.GetLength(0); row++)
            {
                for (int col = 0; col < map.GetLength(1); col++)
                {
                    map[row, col] = lines[row][col];
                }
            }

            return map;
        }

        public static void Print(char[,] map)
        {
            for (int row = 0; row < map.GetLength(0); row++)
            {
                for (int col = 0; col < map.GetLength(1); col++)
                {
                    Console.Write(map[row, col]);
                }

                Console.WriteLine();
            }
        }
    }
}