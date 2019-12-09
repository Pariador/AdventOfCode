namespace Intcode
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    public enum ExitCode
    {
        Halt = 99,
        OutOfBounds = -1,
        Waiting = 0
    }

    public class IntComp
    {
        public const int HaltCode = 99;
        public const int OutOfBoundsCode = -1;

        private Dictionary<int, Definition> definitions;

        public IntComp()
        {
            this.Reset();

            this.definitions = this.DefineOperations()
                .ToDictionary(op => op.Code, op => op);
        }

        public IntComp(int[] program)
            : this()
        {
            this.Program = program;
        }

        protected int[] program;

        protected int[] instance;

        protected int current;

        protected int count;

        public int[] Program
        {
            get
            {
                return this.program.ToArray();
            }

            set
            {
                if (value == null)
                    throw new ArgumentException("Program can't be null!");

                this.program = value.ToArray();

                this.Reload();
            }
        }

        public virtual void Reset()
        {
            this.current = 0;
            this.count = 0;
        }

        public virtual void Load()
        {
            this.instance = this.program.ToArray();
        }

        public void Reload()
        {
            this.Reset();
            this.Load();
        }

        public ExitCode Restart()
        {
            this.Reload();

            return this.Start();
        }

        public ExitCode Start()
        {
            ExitCode? exit = null;

            while (exit == null)
            {
                var operation = Read(this.current);
                this.Resolve(operation);
                int? jump = operation.Execute();

                if (jump == null)
                    this.current += operation.Length;
                else
                    this.current = (int)jump;

                this.count++;

                exit = this.ShouldExit();
            }

            return (ExitCode)exit;
        }

        public int[] MemDump()
        {
            return this.instance.ToArray();
        }

        protected virtual ExitCode? ShouldExit()
        {
            if (this.current < 0 || this.instance.Length <= this.current)
                return ExitCode.OutOfBounds;
            else if (this.instance[this.current] == (int)ExitCode.Halt)
                return ExitCode.Halt;

            return null;
        }

        protected virtual List<Definition> DefineOperations()
        {
            List<Definition> operations = new List<Definition>
            {
                new Definition(1, "ADD", paramCount: 2, write: true, action: this.AddOperation),
                new Definition(2, "MUL", paramCount: 2, write: true, action: this.MultiplyOperation),
            };

            return operations;
        }

        protected virtual void Resolve(Operation operation)
        {
            for (int i = 0; i < operation.Parameters.Length; i++)
            {
                operation.Parameters[i] = this.instance[operation.Parameters[i]];
            }
        }

        private Operation Read(int location)
        {
            var (code, modes) = this.ParseOpcode(this.instance[location++]);
            var definition = this.definitions[code];

            int[] parameters = new int[definition.ParamCount];
            for (int i = 0; i < definition.ParamCount; i++, location++)
                parameters[i] = this.instance[location];

            int? writeAddr = null;
            if (definition.Write)
                writeAddr = this.instance[location];

            return new Operation(definition, parameters, modes, writeAddr);
        }

        private int? AddOperation(int[] parameters, int? writeAddr)
        {
            int target = (int)writeAddr;

            this.instance[target] = parameters[0] + parameters[1];

            return null;
        }

        private int? MultiplyOperation(int[] parameters, int? writeAddr)
        {
            int target = (int)writeAddr;

            this.instance[target] = parameters[0] * parameters[1];

            return null;
        }

        private (int, int[]) ParseOpcode(int opcode)
        {
            if (opcode < 100)
                return (opcode, new int[0]);

            string str = opcode.ToString();

            int modesLength = str.Length - 2;

            int code = int.Parse(str.Substring(modesLength));
            int[] modes = str.Substring(0, modesLength)
                .Select(c => c - 48)
                .Reverse()
                .ToArray();

            return (code, modes);
        }
    }
}