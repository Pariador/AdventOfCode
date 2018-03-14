namespace BalanceBots
{
    using System;
    using System.Linq;
    using System.IO;
    using System.Collections.Generic;

    public static class Program
    {
        public static void Main()
        {
            var instructions = ReadInstructions("input.txt");

            BotController controller = new BotController(61, 17);

            int binsProduct = controller.Execute(instructions);

            Console.WriteLine($"Bot with chips: {controller.BotWithChips}");
            Console.WriteLine($"Bins product: {binsProduct}");
        }

        public static List<string> ReadInstructions(string fileName)
        {
            var instructions = new List<string>();

            using (var reader = new StreamReader(fileName))
            {
                while (true)
                {
                    string line = reader.ReadLine();
                    if (line == null)
                    {
                        break;
                    }

                    instructions.Add(line);
                }
            }

            return instructions;
        }
    }
}