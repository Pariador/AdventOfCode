namespace TwoFactorAuthentication
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Collections.Generic;

    using Commands;

    public static class Program
    {
        public static void Main()
        {
            bool[,] screen = new bool[6, 50];

            var commands = ReadCommands("input.txt");

            foreach (var command in commands)
            {
                command.Execute(screen);
            }

            int litCount = CountLit(screen);

            Console.WriteLine($"Lit pixels count: {litCount}\n\n");

            Print(screen);
        }

        public static int CountLit(bool[,] screen)
        {
            int litCount = 0;

            for (int row = 0; row < screen.GetLength(0); row++)
            {
                for (int col = 0; col < screen.GetLength(1); col++)
                {
                    if (screen[row, col])
                    {
                        litCount++;
                    }
                }
            }

            return litCount;
        }

        public static List<ICommand> ReadCommands(string fileName)
        {
            var commands = new List<ICommand>();

            using (var reader = new StreamReader(fileName))
            {
                while (true)
                {
                    string line = reader.ReadLine();
                    if (line == null)
                    {
                        break;
                    }

                    commands.Add(ParseCommand(line));
                }
            }

            return commands;
        }

        public static ICommand ParseCommand(string str)
        {
            string[] words = str.Split(' ');

            if (words[0] == "rect")
            {
                int[] dimensions = words[1].Split('x')
                    .Select(int.Parse)
                    .ToArray();

                return new DrawRect(width: dimensions[0], height: dimensions[1]);
            }
            else if (words[0] != "rotate")
            {
                throw new ArgumentException($"Unkown command - {words[0]}!!!");
            }

            int dimension = words[1] == "row" ? 0 : 1;
            int index = int.Parse(words[2].Substring(2));
            int count = int.Parse(words[4]);

            return new Rotate(count, dimension, index);
        }

        public static void Print(bool[,] screen)
        {
            for (int row = 0; row < screen.GetLength(0); row++)
            {
                for (int col = 0; col < screen.GetLength(1); col++)
                {
                    if (screen[row, col])
                    {
                        Console.Write("#");
                    }
                    else
                    {
                        Console.Write(".");
                    }
                }

                Console.WriteLine();
            }

            Console.WriteLine("---------------------------");
        }
    }
}