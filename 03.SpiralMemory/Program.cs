namespace SpiralMemory
{
    using System;

    public static class Program
    {
        public static void Main()
        {
            Console.Write("Target: ");
            int target = int.Parse(Console.ReadLine());

            Console.Write("Solution 1 or 2?: ");
            int solution = int.Parse(Console.ReadLine());

            int size = (int)Math.Ceiling(Math.Sqrt(target));

            size = size % 2 == 0 ? size + 1 : size;

            if (solution == 1)
            {
                FirstSolution(target, size);
            }
            else
            {
                SecondSolution(target, size);
            }
        }

        public static void FirstSolution(int target, int size)
        {
            int[,] grid = new int[size, size];

            SpiralMover mover = new SpiralMover(grid);

            for (int i = 1; i < target; i++)
            {
                grid[mover.Current.Row, mover.Current.Col] = i;

                mover.Next();
            }

            int distance = CalcDistance(mover.Start, mover.Current);

            Console.WriteLine($"Target Distance: {distance}");
        }

        public static void SecondSolution(int target, int size)
        {
            int[,] grid = new int[size, size];

            SpiralMover mover = new SpiralMover(grid);

            grid[mover.Current.Row, mover.Current.Col] = 1;

            while (true)
            {
                if (grid[mover.Current.Row, mover.Current.Col] > target)
                {
                    break;
                }

                mover.Next();

                grid[mover.Current.Row, mover.Current.Col] = Sum(mover.Current, grid);
            }

            Console.WriteLine($"Result: {grid[mover.Current.Row, mover.Current.Col]}");
        }

        public static int Sum(Position position, int[,] grid)
        {
            int sum = 0;

            int row = position.Row;
            int col = position.Col;

            sum += GetCell(row + 1, col, grid);
            sum += GetCell(row - 1, col, grid);
            sum += GetCell(row, col + 1, grid);
            sum += GetCell(row, col - 1, grid);

            sum += GetCell(row + 1, col + 1, grid);
            sum += GetCell(row - 1, col - 1, grid);
            sum += GetCell(row + 1, col - 1, grid);
            sum += GetCell(row - 1, col + 1, grid);

            return sum;
        }

        public static int GetCell(int row, int col, int[,] grid)
        {
            return Utils.IsValidPositon(row, col, grid) ? grid[row, col] : 0;
        }

        public static int CalcDistance(Position first, Position second)
        {
            return Math.Abs(first.Row - second.Row) + Math.Abs(first.Col - second.Col);
        }

        public static void Print(int[,] grid)
        {
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    Console.Write(grid[i, j].ToString().PadLeft(2, '0') + " ");
                }

                Console.WriteLine();
            }
        }
    }
}