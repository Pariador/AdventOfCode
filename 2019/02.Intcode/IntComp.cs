namespace Intcode
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    public class IntComp
    {
        protected const int HaltCode = 99;

        private Dictionary<int, Definition> definitions;

        public IntComp()
        {
            this.ExitCode = -1;

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

        public int[] Program
        {
            get
            {
                return this.program;
            }

            set
            {
                if (value == null)
                    throw new ArgumentException("Program can't be null!");

                this.program = value.ToArray();
            }
        }

        public int ExitCode { get; private set; }

        private void Init(int skip = 0, params int[] initial)
        {
            this.instance = this.program.ToArray();

            for (int i = 0; i < initial.Length; i++)
                this.instance[skip + i] = initial[i];
        }

        public int Run(int skip = 0, params int[] initial)
        {
            this.Init(skip, initial);

            int current = 0;
            int count = 1;
            while (true)
            {
                if (this.instance.Length <= current)
                {
                    this.ExitCode = 1;
                    break;
                }
                else if (this.instance[current] == HaltCode)
                {
                    this.ExitCode = 0;
                    break;
                }

                var operation = Read(current);
                this.Resolve(operation);
                int? jump = operation.Execute();

                if (jump == null)
                    current += operation.Length;
                else
                    current = (int)jump;

                count++;
            }

            return this.instance[0];
        }

        public int[] MemDump()
        {
            return this.instance.ToArray();
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