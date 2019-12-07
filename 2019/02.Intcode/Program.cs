namespace Intcode
{
    using System;
    using System.IO;
    using System.Linq;

    static class Program
    {
        private const int MaxInput = 100;

        static void Main()
        {
            int[] program = File.ReadAllText("input.txt")
                .Split(',')
                .Select(int.Parse)
                .ToArray();

            var comp = new IntComp(program);

            int a = 12, b = 2;
            int output = comp.Run(1, a, b);
            if (comp.ExitCode == 1)
                Console.WriteLine("WARN: Program exited with code 1.");

            PrintResults(a, b, output);

            output = 19690720;
            (int, int)? inputs = FindInputs(comp, output);
            if (inputs != null)
            {
                (a, b) = inputs.Value;

                PrintResults(a, b, output);
                Console.WriteLine($"100 * {a} + {b} = {100 * a + b}");
            }
            else
                Console.WriteLine($"Inputs for {output} not found!");
        }

        static (int, int)? FindInputs(IntComp comp, int target)
        {
            for (int a = 0; a < MaxInput; a++)
            {
                for (int b = 0; b < MaxInput; b++)
                {
                    var output = comp.Run(1, a, b);

                    if (output == target)
                        return (a, b);
                }
            }

            return null;
        }

        static void PrintResults(int a, int b, int output)
        {
            Console.WriteLine($"({a}, {b}) > P > {output}");
        }
    }
}