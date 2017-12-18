namespace Duet
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    public static class Program
    {
        public static void Main()
        {
            string input = null;
            using (var reader = new StreamReader("input.txt"))
            {
                input = reader.ReadToEnd();
            }

            Instruction[] firstInstructions = ReadInstructions(new StringReader(input));
            Instruction[] secondInstructions = ReadInstructions(new StringReader(input), sendAndRecieve: true);

            Computer first = new Computer();
            first.Execute(firstInstructions);

            Console.WriteLine($"First result: {first.FirstRecoveredSound}");

            first.Reset();
            Computer second = new Computer();
            first.Partner = second;

            while ((!first.Halt && !first.Waiting) || (!second.Halt && !second.Waiting))
            {
                first.Execute(secondInstructions);
                second.Execute(secondInstructions);
            }

            Console.WriteLine($"Second result: {second.MessagesSent}");
        }

        public static Instruction[] ReadInstructions(TextReader reader, bool sendAndRecieve = false)
        {
            var instructions = new List<Instruction>();

            while (true)
            {
                string line = reader.ReadLine();
                if (line == null)
                {
                    break;
                }

                Instruction instruction = Instruction.Parse(line, sendAndRecieve);

                instructions.Add(instruction);
            }

            return instructions.ToArray();
        }
    }
}