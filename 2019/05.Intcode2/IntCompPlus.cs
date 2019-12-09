namespace Intcode
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    public class IntCompPlus : IntComp
    {
        private Queue<int> input;
        private Queue<int> output;

        private bool waiting;

        public IntCompPlus(Queue<int> input = null, Queue<int> output = null)
            : base()
        {
            this.Input = input ?? new Queue<int>();
            this.Output = output ?? new Queue<int>();
        }

        public IntCompPlus(int[] program, Queue<int> input = null, Queue<int> output = null)
            : this(input, output)
        {
            this.Program = program;
        }

        public Queue<int> Input 
        {
            get { return this.input; }
            set 
            {
                if (value == null)
                    throw new ArgumentNullException();

                this.input = value; 
            }
        }

        public Queue<int> Output 
        { 
            get { return this.output; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException();

                this.output = value;
            }
        }

        public override void Reset()
        {
            base.Reset();
            this.waiting = false;
        }

        protected override ExitCode? ShouldExit()
        {
            var exit = base.ShouldExit();
            if (exit != null) return exit;

            if (this.waiting) return ExitCode.Waiting;

            return null;
        }

        protected override void Resolve(Operation operation)
        {
            for (int i = 0, len = operation.Modes.Length; i < len; i++)
            {
                if (operation.Modes[i] == 0)
                    operation.Parameters[i] = this.instance[operation.Parameters[i]];
            }
        }

        protected override List<Definition> DefineOperations()
        {
            var operations = base.DefineOperations();

            operations.AddRange(new Definition[] {
                new Definition(3, "IN", paramCount: 0, write: true, action: this.InputOperation),
                new Definition(4, "OUT", paramCount: 1, write: false, action: this.OutputOperation),
                new Definition(5, "IF", paramCount: 2, write: false, action: this.IfOperation),
                new Definition(6, "UN", paramCount: 2, write: false, action: this.UnlessOperation),
                new Definition(7, "LT", paramCount: 2, write: true, action: this.LessThanOperation),
                new Definition(8, "EQL", paramCount: 2, write: true, action: this.EqualsOperation),
            });

            return operations;
        }

        private int? InputOperation(int[] parameters, int? writeAddr)
        {
            int target = (int)writeAddr;

            if (!this.Input.Any())
            {
                this.waiting = true;
                return this.current;
            }

            this.waiting = false;
            this.instance[target] = this.Input.Dequeue();

            return null;
        }

        private int? OutputOperation(int[] parameters, int? writeAddr)
        {
            this.Output.Enqueue(parameters[0]);

            return null;
        }
        
        private int? IfOperation(int[] parameters, int? writeAddr)
        {
            if (0 < parameters[0])
                return parameters[1];
            else
                return null;
        }

        private int? UnlessOperation(int[] parameters, int? writeAddr)
        {
            if (parameters[0] == 0)
                return parameters[1];
            else
                return null;
        }

        private int? LessThanOperation(int[] parameters, int? writeAddr)
        {
            int target = (int)writeAddr;

            if (parameters[0] < parameters[1])
                this.instance[target] = 1;
            else
                this.instance[target] = 0;

            return null;
        }

        private int? EqualsOperation(int[] parameters, int? writeAddr)
        {
            int target = (int)writeAddr;

            if (parameters[0] == parameters[1])
                this.instance[target] = 1;
            else
                this.instance[target] = 0;

            return null;
        }
    }
}