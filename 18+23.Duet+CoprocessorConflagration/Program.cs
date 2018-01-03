namespace Duet
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    public static class Program
    {
        public static void Main()
        {
            string firstInput = null;
            using (var reader = new StreamReader("input.txt"))
            {
                firstInput = reader.ReadToEnd();
            }

            Instruction[] firstInstructions = ReadInstructions(new StringReader(firstInput));
            Instruction[] secondInstructions = ReadInstructions(new StringReader(firstInput), sendAndRecieve: true);

            Computer first = new Computer();
            first.Execute(firstInstructions);

            Console.WriteLine($"First result - first recovered sound: {first.FirstRecoveredSound}");

            first.Reset();
            Computer second = new Computer();
            first.Partner = second;

            while ((!first.Halt && !first.Waiting) || (!second.Halt && !second.Waiting))
            {
                first.Execute(secondInstructions);
                second.Execute(secondInstructions);
            }

            Console.WriteLine($"Second result - messages sent: {second.MessagesSent}");

            string secondInput = null;
            using (var reader = new StreamReader("input2.txt"))
            {
                secondInput = reader.ReadToEnd();
            }

            Instruction[] thirdInstructions = ReadInstructions(new StringReader(secondInput));

            first.Reset();
            first.Execute(thirdInstructions);

            Console.WriteLine($"Third result - multiplications count: {first.MultiplicationCount}");

            Console.WriteLine($"Fourth result: {SolveFourth()}");
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

        public static int SolveFourth()
        {
            int count = 0;

            for (int i = 106500; i <= 123500; i += 17)
            {
                if (!Utils.IsPrime(i))
                {
                    count++;
                }
            }

            return count;
        }
    }
}