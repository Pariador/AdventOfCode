namespace Duet
{
    using System;
    using System.Linq;

    public class Instruction
    {
        public static Instruction Parse(string str, bool sendAndRecieve = false)
        {
            string[] data = str.Split(' ');

            InstructionType type;
            switch (data[0])
            {
                case "set":
                    type = InstructionType.Set;
                    break;
                case "add":
                    type = InstructionType.Add;
                    break;
                case "mul":
                    type = InstructionType.Multiply;
                    break;
                case "mod":
                    type = InstructionType.Modulo;
                    break;
                case "jgz":
                    type = InstructionType.ConditionalJump;
                    break;
                case "snd":
                    if (sendAndRecieve)
                    {
                        type = InstructionType.Send;
                    }
                    else
                    {
                        type = InstructionType.Play;
                    }
                    break;
                case "rcv":
                    if (sendAndRecieve)
                    {
                        type = InstructionType.Recieve;
                    }
                    else
                    {
                        type = InstructionType.Recover;
                    }
                    break;
                default:
                    throw new ArgumentException($"Unknown instruction {data[0]}!");
            }

            return new Instruction
            (
                type: type,
                arguments: data.Skip(1)
                    .ToArray()
            );
        }

        private string[] arguments;

        public Instruction(InstructionType type, string[] arguments = null)
        {
            this.Type = type;

            if (arguments == null)
            {
                this.arguments = new string[0];
            }
            else
            {
                this.arguments = arguments;
            }
        }

        public InstructionType Type { get; private set; }

        public string[] Arguments 
        {
            get
            {
                return this.arguments.ToArray();
            }
        }
    }
}