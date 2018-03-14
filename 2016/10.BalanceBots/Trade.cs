namespace BalanceBots
{
    public enum TargetType
    {
        Bot, Bin
    }

    public class Trade
    {
        public Trade(int source, int target, TargetType targetType, int value)
        {
            this.Source = source;
            this.Target = target;
            this.TargetType = targetType;
            this.Value = value;
        }

        public int Source { get; }

        public int Target { get; }

        public TargetType TargetType { get; }

        public int Value { get; }
    }
}