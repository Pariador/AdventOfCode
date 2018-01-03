namespace Duet
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Computer
    {
        private static int NextId = 0;

        private const int StandartJump = 1;

        private Dictionary<char, long> registers;

        private Computer partner;

        private Queue<long> messages;

        public Computer()
        {
            this.Id = Computer.NextId++;

            this.Reset();
        }

        public int Id { get; }

        public Computer Partner
        {
            get
            {
                return this.partner;
            }

            set
            {
                if (value == null && this.partner != null)
                {
                    this.partner.partner = null;
                    this.partner = null;
                }
                else
                {
                    this.partner = value;
                    value.partner = this;
                }
            }
        }

        public long? FirstRecoveredSound { get; private set; }

        public long MessagesSent { get; private set; }

        public long MultiplicationCount { get; private set; }

        public bool Waiting { get; set; }

        public bool Halt { get; set; }

        private long CurrentInstruction { get; set; }

        private long LastPlayedSound { get; set; }

        public void Execute(Instruction[] instructions)
        {
            while (0 <= this.CurrentInstruction && this.CurrentInstruction < instructions.Length && !this.Halt && !this.Waiting)
            {
                this.Execute(instructions[this.CurrentInstruction]);
            }

            if (!this.Waiting)
            {
                this.Halt = true;
            }
        }

        public void Execute(Instruction instruction)
        {
            long jump = Computer.StandartJump;

            switch (instruction.Type)
            {
                case InstructionType.Set:
                    this.Set(instruction.Arguments[0][0], this.Eval(instruction.Arguments[1]));
                    break;
                case InstructionType.Add:
                    this.Add(instruction.Arguments[0][0], this.Eval(instruction.Arguments[1]));
                    break;
                case InstructionType.Subtract:
                    this.Subtract(instruction.Arguments[0][0], this.Eval(instruction.Arguments[1]));
                    break;
                case InstructionType.Multiply:
                    this.Multiply(instruction.Arguments[0][0], this.Eval(instruction.Arguments[1]));
                    break;
                case InstructionType.Modulo:
                    this.Modulo(instruction.Arguments[0][0], this.Eval(instruction.Arguments[1]));
                    break;
                case InstructionType.JumpIfPositive:
                    jump = this.JumpIfPositive(this.Eval(instruction.Arguments[0]), this.Eval(instruction.Arguments[1]));
                    break;
                case InstructionType.JumpIfNotZero:
                    jump = this.JumpIfNotZero(this.Eval(instruction.Arguments[0]), this.Eval(instruction.Arguments[1]));
                    break;
                case InstructionType.Play:
                    this.Play(this.Eval(instruction.Arguments[0]));
                    break;
                case InstructionType.Recover:
                    this.Recover(this.Eval(instruction.Arguments[0]));
                    break;
                case InstructionType.Send:
                    this.Send(this.Eval(instruction.Arguments[0]));
                    break;
                case InstructionType.Recieve:
                    this.Recieve(instruction.Arguments[0][0]);
                    if (this.Waiting)
                    {
                        jump = 0;
                    }
                    break;
                default:
                    throw new ArgumentException("Unknown Instruction type!");
            }

            this.CurrentInstruction += jump;
        }

        public long Get(char register)
        {
            return this.registers[register];
        }

        private void Set(char register, long value)
        {
            this.registers[register] = value;
        }

        private void Add(char register, long value)
        {
            this.registers[register] += value;
        }

        private void Subtract(char register, long value)
        {
            this.registers[register] -= value;
        }

        private void Multiply(char register, long value)
        {
            this.registers[register] *= value;

            this.MultiplicationCount++;
        }

        private void Modulo(char register, long value)
        {
            this.registers[register] %= value;
        }

        private long JumpIfPositive(long value, long offset)
        {
            if (value > 0)
            {
                return offset;
            }

            return Computer.StandartJump;
        }

        private long JumpIfNotZero(long value, long offset)
        {
            if (value != 0)
            {
                return offset;
            }

            return Computer.StandartJump;
        }

        private void Play(long value)
        {
            this.LastPlayedSound = value;
        }

        private void Recover(long value)
        {
            if (value != 0 && this.FirstRecoveredSound == null)
            {
                this.FirstRecoveredSound = this.LastPlayedSound;
                this.Halt = true;
            }
        }

        private void Send(long value)
        {
            if (this.partner != null)
            {
                this.partner.messages.Enqueue(value);
                this.partner.Waiting = false;
                this.MessagesSent++;
            }
        }

        private void Recieve(char register)
        {
            if (this.partner == null)
            {
                return;
            }

            if (this.messages.Any())
            {
                this.Set(register, this.messages.Dequeue());
                this.Waiting = false;
            }
            else
            {
                this.Waiting = true;
            }
        }

        private long Eval(string argument)
        {
            if (char.IsLetter(argument[0]))
            {
                return this.registers[argument[0]];
            }

            return long.Parse(argument);
        }

        public void Reset()
        {
            this.CurrentInstruction = 0;
            this.messages = new Queue<long>();
            this.MessagesSent = 0;
            this.MultiplicationCount = 0;
            this.FirstRecoveredSound = null;
            this.LastPlayedSound = 0;
            this.Halt = false;
            this.Waiting = false;

            this.registers = new Dictionary<char, long>();

            for (int i = 0; i < 26; i++)
            {
                char register = (char)(i + 97);

                this.registers[register] = 0;
            }

            this.registers['p'] = this.Id;
        }
    }
}