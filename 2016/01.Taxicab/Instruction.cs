namespace Taxicab
{
    public class Instruction
    {
        public static Instruction Parse(string str)
        {
            Side side = str[0] == 'L' ? Side.Left : Side.Right;

            uint distance = uint.Parse(str.Substring(1));

            return new Instruction(distance, side);
        }

        public Instruction(uint distance, Side side)
        {
            this.Distance = distance;
            this.Side = side;
        }

        public uint Distance { get; }

        public Side Side { get; }
    }
}