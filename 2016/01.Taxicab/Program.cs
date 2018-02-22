namespace Taxicab
{
    using System;
    using System.Linq;
    using System.IO;

    public static class Program
    {
        public static void Main()
        {
            var instructions = ReadInstructions("input.txt");

            Unit unit = new Unit();

            foreach (var instruction in instructions)
            {
                unit.Move(instruction);
            }

            int distanceEnd = CalcDistance(unit.Position);
            int distanceRevisited = CalcDistance(unit.Revisited);

            Console.WriteLine($"Distance to end position: {distanceEnd}");
            Console.WriteLine($"Revisited position distance: {distanceRevisited}");
        }

        public static int CalcDistance(int[] position)
        {
            return Math.Abs(position[0]) + Math.Abs(position[1]);
        }


        public static Instruction[] ReadInstructions(string fileName)
        {
            string file = null;
            using (var reader = new StreamReader(fileName))
            {
                file = reader.ReadToEnd();
            }

            return file.Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries)
                .Select(Instruction.Parse)
                .ToArray();
        }
    }
}