namespace DiskDefragmentation
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using KnotHash;

    public static class Program
    {
        public const int GridSize = 128;

        public const int BitsInByte = 8;

        public static void Main()
        {
            string test = "flqrgnkx";
            string input = "amgozmfv";

            Grid grid = ConstructGrid(input);

            int onesCount = CountOnes(grid);
            int regionsCount = CountRegions(grid);

            Console.WriteLine($"Ones count: {onesCount}");
            Console.WriteLine($"Regions count: {regionsCount}");
        }

        public static int CountRegions(Grid grid)
        {
            int regionsCount = 0;

            for (int row = 0; row < grid.Size; row++)
            {
                for (int col = 0; col < grid.Size; col++)
                {
                    if (grid[row, col])
                    {
                        regionsCount++;

                        TraverseRegion(grid, new Position(row, col));
                    }
                }
            }

            return regionsCount;
        }
 
        public static void TraverseRegion(Grid grid, Position position)
        {
            var visited = new HashSet<Position>();
            var next = new Queue<Position>();
            next.Enqueue(position);
            visited.Add(position);

            while (next.Any())
            {
                var current = next.Dequeue();

                Position[] neighbors =
                {
                    new Position(current.Row + 1, current.Col),
                    new Position(current.Row - 1, current.Col),
                    new Position(current.Row, current.Col + 1),
                    new Position(current.Row, current.Col - 1)
                };

                foreach (var neighbor in neighbors.Where(neighbor =>  grid.IsValid(neighbor) && grid[neighbor.Row, neighbor.Col] && !visited.Contains(neighbor)))
                {
                    next.Enqueue(neighbor);
                    visited.Add(neighbor);
                }
            }

            foreach (var pos in visited)
            {
                grid[pos.Row, pos.Col] = false;
            }
        }

        public static int CountOnes(Grid grid)
        {
            return grid.Count(bit => bit);
        }

        public static int CountOnes(byte @byte)
        {
            int onesCount = 0;

            for (int i = 0; i < Program.BitsInByte; i++)
            {
                int mask = 1 << i;

                int result = @byte & mask;

                if (result == mask)
                {
                    onesCount++;
                }
            }

            return onesCount;
        }

        public static Grid ConstructGrid(string key)
        {
            byte[,] bytes = new byte[Program.GridSize, Program.GridSize / Program.BitsInByte];

            string[] inputs = GetHashInputs(key);

            Hasher hasher = new Hasher();

            for (int i = 0; i < inputs.Length; i++)
            {
                byte[] hash = hasher.Hash(inputs[i]);

                for (int h = 0; h < hash.Length; h++)
                {
                    bytes[i, h] = hash[h];
                }
            }

            return new Grid(bytes);
        }

        public static string[] GetHashInputs(string key)
        {
            string[] inputs = new string[Program.GridSize];

            for (int i = 0; i < inputs.Length; i++)
            {
                inputs[i] = key + "-" + i.ToString(); 
            }

            return inputs;
        }

        public static void Print(Grid grid)
        {
            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    Console.Write(grid[row, col] ? "#" : ".");
                }
                Console.WriteLine();
            }
        }
    }
}