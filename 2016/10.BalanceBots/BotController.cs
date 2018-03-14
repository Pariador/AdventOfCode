namespace BalanceBots
{
    using System.Linq;
    using System.Collections.Generic;

    public class BotController
    {
        private static void ReadyCheck(Dictionary<int, List<int>> storage, int key)
        {
            if (storage.ContainsKey(key))
            {
                return;
            }

            storage[key] = new List<int>();
        }

        private static TargetType Parse(string str)
        {
            return str == "output" ? TargetType.Bin : TargetType.Bot;
        }

        private Dictionary<int, List<int>> bots;

        private Dictionary<int, List<int>> bins;

        private Queue<string> waitingInstructions;

        public BotController(params int[] chips)
        {
            this.Chips = chips;
            this.BotWithChips = -1;

            this.bots = new Dictionary<int, List<int>>();
            this.bins = new Dictionary<int, List<int>>();
            this.waitingInstructions = new Queue<string>();
        }

        public int[] Chips { get; }

        public int BotWithChips { get; set; }

        public int Execute(IEnumerable<string> instructions)
        {
            foreach (var instruction in instructions)
            {
                if (!this.Execute(instruction))
                {
                    this.waitingInstructions.Enqueue(instruction);
                    continue;
                }

                while (this.ExecuteWaitingInstruction());
            }

            return this.bins[0].First() * this.bins[1].First() * this.bins[2].First();
        }

        private bool Execute(string instruction)
        {
            string[] data = instruction.Split(' ');
            if (data[0] == "value")
            {
                return this.ExecuteLoad(data);
            }
            else
            {
                return this.ExecuteTrades(data);
            }
        }

        private bool ExecuteTrades(string[] data)
        {
            int bot = int.Parse(data[1]);

            ReadyCheck(this.bots, bot);

            if (this.bots[bot].Count != 2)
            {
                return false;
            }

            Trade low = new Trade
            (
                source: bot,
                target: int.Parse(data[6]),
                targetType: Parse(data[5]),
                value: this.bots[bot].Min()
            );

            Trade high = new Trade
            (
                source: bot,
                target: int.Parse(data[11]),
                targetType: Parse(data[10]),
                value: this.bots[bot].Max()
            );

            this.ExecuteTrade(low);
            this.ExecuteTrade(high);


            if (this.Chips.Contains(low.Value) && this.Chips.Contains(high.Value))
            {
                this.BotWithChips = bot;
            }

            return true;
        }

        private void ExecuteTrade(Trade trade)
        {
            ReadyCheck(this.bots, trade.Source);

            this.bots[trade.Source].Remove(trade.Value);

            if (trade.TargetType == TargetType.Bin)
            {
                ReadyCheck(this.bins, trade.Target);

                this.bins[trade.Target].Add(trade.Value);
            }
            else if (trade.TargetType == TargetType.Bot)
            {
                ReadyCheck(this.bots, trade.Target);

                this.bots[trade.Target].Add(trade.Value);
            }
        }

        private bool ExecuteLoad(string[] data)
        {
            int bot = int.Parse(data[5]);
            int chip = int.Parse(data[1]);

            ReadyCheck(this.bots, bot);

            this.bots[bot].Add(chip);
            return true;
        }

        private bool ExecuteWaitingInstruction()
        {
            int count = this.waitingInstructions.Count;

            for (int i = 0; i < count; i++)
            {
                string instruction = this.waitingInstructions.Dequeue();

                if (!this.Execute(instruction))
                {
                    this.waitingInstructions.Enqueue(instruction);
                }
                else
                {
                    return true;
                }
            }

            return false;
        }

    }
}