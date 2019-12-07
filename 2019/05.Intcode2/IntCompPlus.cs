namespace Intcode
{
    using System;
    using System.Collections.Generic;

    public class IntCompPlus : IntComp
    {
        private Func<int> input;
        private Action<int> output;

        public IntCompPlus(Func<int> input = null, Action<int> output = null)
            : base()
        {
            this.Input = input ?? (() => 0);
            this.Output = output ?? (x => { });
        }

        public IntCompPlus(int[] program, Func<int> input = null, Action<int> output = null)
            : base(program)
        {
            this.Input = input ?? (() => 0);
            this.Output = output ?? (x => { });
        }

        public Func<int> Input 
        {
            get { return this.input; }
            set 
            {
                if (value == null)
                    throw new ArgumentNullException();

                this.input = value; 
            }
        }

        public Action<int> Output 
        { 
            get { return this.output; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException();

                this.output = value;
            }
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

            this.instance[target] = this.Input();

            return null;
        }

        private int? OutputOperation(int[] parameters, int? writeAddr)
        {
            this.Output(parameters[0]);

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