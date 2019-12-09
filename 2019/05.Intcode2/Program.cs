namespace Intcode
{
    using System;
    using System.IO;
    using System.Linq;

    static class Program
    {
        static void Main()
        {
            int[] program = File.ReadAllText("test-program.txt")
                .Split(',')
                .Select(int.Parse)
                .ToArray();

            var comp = new IntCompPlus(program);

            Run(comp, 1);
            Run(comp, 5);
        }

        static void Run(IntCompPlus comp, int input)
        {
            Console.WriteLine($"---INPUT {input}---");
            comp.Input.Enqueue(input);

            var exit = comp.Restart();
            if (exit != ExitCode.Halt)
                Console.WriteLine("WARN: Computer did not halt!");

            while (comp.Output.Any())
                Console.WriteLine(comp.Output.Dequeue());

            Console.WriteLine();
        }
    }
}