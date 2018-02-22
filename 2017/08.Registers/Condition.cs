namespace Registers
{
    using System;
    using System.Collections.Generic;

    public class Condition
    {
        private static int GetValue(string str, Dictionary<string, int> registers)
        {
            bool parsed = int.TryParse(str, out int result);
            if (!parsed)
            {
                Utils.InitRegister(str, registers);

                result = registers[str];
            }

            return result;
        }

        public Condition(string left, string right, string @operator)
        {
            this.Left = left;
            this.Right = right;
            this.Operator = @operator;
        }

        public string Left { get; }

        public string Right { get; }

        public string Operator { get; }

        public bool Eval(Dictionary<string, int> registers)
        {
            int left = GetValue(this.Left, registers);
            int right = GetValue(this.Right, registers);

            switch (this.Operator)
            {
                case "==":
                    return left == right;
                case "!=":
                    return left != right;
                case ">":
                    return left > right;
                case "<":
                    return left < right;
                case ">=":
                    return left >= right;
                case "<=":
                    return left <= right;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}