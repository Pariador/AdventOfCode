namespace TwistyTrampolines
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class Program
    {
        public static void Main()
        {
            int[] instructions = null;
            using (var reader = new StreamReader("input.txt"))
            {
                instructions = ReadInstructions(reader);
            }

            Console.Write("Advanced execute (y/n)?: ");
            bool advancedExecute = Console.ReadLine() == "y";

            int count = advancedExecute ? AdvandedExecute(instructions) : Execute(instructions);

            Console.WriteLine($"Instructions Executed: {count}");
        }

        public static int[] ReadInstructions(TextReader reader)
        {
            var instructions = new List<int>();

            while (true)
            {
                string line = reader.ReadLine();
                if (line == "end")
                {
                    break;
                }

                instructions.Add(int.Parse(line));
            }

            return instructions.ToArray();
        } 

        public static int Execute(int[] instructions)
        {
            int count = 0;

            for (int i = 0; 0 <= i && i < instructions.Length; i += instructions[i]++)
            {
                count++;
            }

            return count;
        }

        public static int AdvandedExecute(int[] instructions)
        {
            int count = 0;

            for (int i = 0; 0 <= i && i < instructions.Length; i += instructions[i] < 3 ? instructions[i]++ : instructions[i]--)
            {
                count++;
            }

            return count;
        }
    }
}