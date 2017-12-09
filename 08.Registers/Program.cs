namespace Registers
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Collections.Generic;

    public static class Program
    {
        public static void Main()
        {
            var instructions = ReadInstructions(new StreamReader("input.txt"));

            var registers = new Dictionary<string, int>();

            int max = int.MinValue;

            foreach (var instruction in instructions)
            {
                Execute(instruction, registers);

                if (registers[instruction.Register] > max)
                {
                    max = registers[instruction.Register];
                }
            }

            int maxInRegisters = registers.Max(pair => pair.Value);

            Console.WriteLine($"Largest value in registers: {maxInRegisters}");
            Console.WriteLine($"Largest value ever in registers: {max}");
        }

        public static IEnumerable<Instruction> ReadInstructions(TextReader reader)
        {
            var instructions = new List<Instruction>();

            while (true)
            {
                string line = reader.ReadLine();
                if (line == null)
                {
                    return instructions;
                }

                instructions.Add(Instruction.Parse(line));
            }

            return instructions;
        }

        public static void Execute(Instruction instruction, Dictionary<string, int> registers)
        {
            if (!instruction.Condition.Eval(registers))
            {
                return;
            }

            Utils.InitRegister(instruction.Register, registers);

            if (instruction.Operator == "inc")
            {
                registers[instruction.Register] += instruction.Amount;
            }
            else
            {
                registers[instruction.Register] -= instruction.Amount;
            }
        }
    }
}