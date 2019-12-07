namespace Intcode
{
    using System;
    using System.IO;
    using System.Linq;

    static class Program
    {
        static void Main()
        {
            int[] program = File.ReadAllText("input.txt")
                .Split(',')
                .Select(int.Parse)
                .ToArray();

            var comp = new IntCompPlus(program);

            int[] inputs = { 1, 5 };
            int i = 0;

            comp.Input = () => 
            {
                Console.WriteLine($"---INPUT {inputs[i]}---");
                return inputs[i++];
            };

            comp.Output = Console.WriteLine;

            comp.Run();
            comp.Run();
        }
    }
}