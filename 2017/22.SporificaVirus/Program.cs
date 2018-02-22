namespace SporificaVirus
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    public static class Program
    {
        public static void Main()
        {
            var grid = ReadGrid("input.txt");

            var infected = GetInfected(grid);
            Virus virus = new Virus(infected);
            int burstsCount = 10000;

            virus.Burst(burstsCount);

            Console.WriteLine($"Virus infections caused with {burstsCount} bursts: {virus.InfectionsCaused}");

            infected = GetInfected(grid);
            virus = new EvolvedVirus(infected);
            burstsCount = 10000000;

            Console.WriteLine("Computing...");
            virus.Burst(burstsCount, percent => Console.WriteLine(percent + "%"));

            Console.WriteLine($"Evolved virus infections caused with {burstsCount} bursts: {virus.InfectionsCaused}");
        }

        public static HashSet<Position> GetInfected(List<string> grid)
        {
            var infected = new HashSet<Position>();

            int midRow = grid.Count / 2;
            int midCol = grid[0].Length / 2;

            for (int row = 0; row < grid.Count; row++)
            {
                for (int col = 0; col < grid[row].Length; col++)
                {
                    if (grid[row][col] == '#')
                    {
                        infected.Add(new Position(col - midCol, row - midRow));
                    }
                }
            }

            return infected;
        }

        public static List<string> ReadGrid(string file)
        {
            var grid = new List<string>();

            using (var reader = new StreamReader(file))
            {
                while (true)
                {
                    string line = reader.ReadLine();
                    if (line == null)
                    {
                        break;
                    }

                    grid.Add(line);
                }
            }

            return grid;
        }
    }
}