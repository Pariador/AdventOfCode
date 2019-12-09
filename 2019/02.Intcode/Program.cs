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

            int a = 12, b = 2;
            SetInputs(program, a, b);

            var comp = new IntComp(program);

            var exit = comp.Start();
            if (exit != ExitCode.Halt)
                Console.WriteLine("WARN: Computer did not halt!");

            int output = GetOutput(comp.MemDump());

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
            var program = comp.Program;

            for (int a = 0; a < MaxInput; a++)
            {
                for (int b = 0; b < MaxInput; b++)
                {
                    SetInputs(program, a, b);
                    comp.Program = program;

                    var exit = comp.Start();

                    int output = GetOutput(comp.MemDump());
                    if (output == target)
                        return (a, b);
                }
            }

            return null;
        }

        static void SetInputs(int[] program, int a, int b)
        {
            if (program.Length < 3)
                throw new ArgumentException("Program too short!");

            program[1] = a;
            program[2] = b;
        }

        static int GetOutput(int[] program)
        {
            if (program.Length == 0)
                throw new ArgumentException("Program too short!");

            return program[0];
        }

        static void PrintResults(int a, int b, int output)
        {
            Console.WriteLine($"({a}, {b}) > P > {output}");
        }
    }
}