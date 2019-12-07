namespace Intcode
{
    using System;
    using System.Linq;

    public delegate int? OperationAction(int[] parameters, int? writeAddr);

    public class Definition
    {
        public Definition(int code, string name, int paramCount, bool write, OperationAction action)
        {
            this.Code = code;
            this.Name = name;
            this.ParamCount = paramCount;
            this.Write = write;
            this.Action = action;
        }

        public int Code { get; }

        public string Name { get; }

        public int ParamCount { get; }

        public bool Write { get; }

        public int Length
        {
            get
            {
                int len = this.ParamCount + 1;

                if (this.Write)
                    len++;

                return len;
            }
        }

        public OperationAction Action { get; }

        public override string ToString()
        {
            return $"{this.Name}({this.Code})";
        }
    }

    public class Operation
    {
        public Operation(Definition def, int[] parameters, int[] modes, int? writeAddr)
        {
            this.Definition = def;
            this.Parameters = parameters.ToArray();
            this.Modes = new int[this.Parameters.Length];
            this.WriteAddr = writeAddr;

            int len = Math.Min(this.Parameters.Length, modes.Length);
            for (int i = 0; i < len; i++)
                this.Modes[i] = modes[i];
        }

        public Definition Definition { get; }

        public int[] Parameters { get; }

        public int[] Modes { get; }

        public int? WriteAddr { get; }

        public int Length
        {
            get
            {
                return this.Definition.Length;
            }
        }

        public int Code
        {
            get
            {
                return this.Definition.Code;
            }
        }

        public string Name
        {
            get
            {
                return this.Definition.Name;
            }
        }

        public int? Execute()
        {
            return this.Definition.Action(this.Parameters, this.WriteAddr);
        }

        public override string ToString()
        {
            string str = this.Definition.ToString();

            str += " " + string.Join(" ", this.Parameters);

            if (this.WriteAddr != null)
                str += $"-> {this.WriteAddr}";

            return str;
        }
    }
}