namespace Intcode
{
    using System;
    using System.Linq;
    using System.IO;

    static class Program
    {
        private const int HaltCode = 99;
        private const int Step = 4;
        private const int MaxInput = 100;

        static void Main()
        {
            int[] program = File.ReadAllText("input.txt")
                .Split(',')
                .Select(int.Parse)
                .ToArray();

            int a = 12;
            int b = 2;
            int output = Run(Init(program, 12, 2));
            PrintResults(a, b, output);

            output = 19690720;
            (int, int)? inputs = FindInputs(program, output);
            if (inputs != null)
            {
                (a, b) = inputs.Value;

                PrintResults(a, b, output);
                Console.WriteLine($"100 * {a} + {b} = {100 * a + b}");
            }
        }

        static int[] Init(int[] program, int a, int b)
        {
            int[] instance = program.ToArray();
            instance[1] = a;
            instance[2] = b;

            return instance;
        }

        static int Run(int[] program)
        {
            for (int current = 0; program[current] != HaltCode; current += Step)
                Execute(program, current);

            return program[0];
        }

        static void Execute(int[] memory, int location)
        {
            int opcode = memory[location];
            int aAddr = memory[location + 1];
            int bAddr = memory[location + 2];
            int target = memory[location + 3];

            int a = memory[aAddr];
            int b = memory[bAddr];
            int result;

            switch (opcode)
            {
                case 1:
                    result = a + b;
                    break;
                case 2:
                    result = a * b;
                    break;
                default:
                    throw new ArgumentException("Unknown opcode!");
            }

            memory[target] = result;
        }

        static (int, int)? FindInputs(int[] program, int output)
        {
            for (int a = 0; a < MaxInput; a++)
            {
                for (int b = 0; b < MaxInput; b++)
                {
                    var result = Run(Init(program, a, b));

                    if (result == output)
                        return (a, b);
                }
            }

            return null;
        }

        static void PrintResults(int a, int b, int output)
        {
            Console.WriteLine($"{a}, {b} > P > {output}");
        }
    }
}