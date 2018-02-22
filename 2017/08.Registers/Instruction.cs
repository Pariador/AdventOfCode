namespace Registers
{
    public class Instruction
    {
        public static Instruction Parse(string str)
        {
            string[] data = str.Split(' ');

            return new Instruction
            (
                register: data[0],
                @operator: data[1],
                amount: int.Parse(data[2]),
                condition: new Condition(data[4], data[6], data[5])
            );
        }

        public Instruction(string register, string @operator, int amount, Condition condition)
        {
            this.Register = register;
            this.Operator = @operator;
            this.Amount = amount;
            this.Condition = condition;
        }

        public string Register { get; }

        public string Operator { get; }

        public int Amount { get; }

        public Condition Condition { get; }
    }
}
