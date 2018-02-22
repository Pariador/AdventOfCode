namespace ElectoromagneticMoat
{
    using System.Linq;
    using System.Collections.Generic;

    public class Builder
    {
        public Builder()
        {
            this.Reset();
        }

        public int MaxStrength { get; private set; }

        public int LongestLength { get; private set; }

        public int LongestStrength { get; private set; }

        private int CurrentLength { get; set; }

        private int CurrentStrength { get; set; }

        public int FindStrongest(HashSet<Component> components)
        {
            this.Reset();

            Find(components, pin: 0);

            return this.MaxStrength;
        }

        private void Find(HashSet<Component> components, int pin)
        {
            var compatibles = components.Where(comp => comp.Left == pin || comp.Right == pin)
                .ToArray();

            if (!compatibles.Any())
            {
                return;
            }

            this.CurrentLength++;

            foreach (var compatible in compatibles)
            {
                components.Remove(compatible);

                int strength = compatible.Left + compatible.Right;

                this.CurrentStrength += strength;

                if (this.CurrentStrength > this.MaxStrength)
                {
                    this.MaxStrength = this.CurrentStrength;
                }

                if ((this.CurrentLength > this.LongestLength) || (this.CurrentLength == this.LongestLength && this.CurrentStrength > this.LongestStrength))
                {
                    this.LongestLength = this.CurrentLength;
                    this.LongestStrength = this.CurrentStrength;
                }

                int nextPin = compatible.Left == pin ? compatible.Right : compatible.Left;

                this.Find(components, nextPin);

                components.Add(compatible);
                this.CurrentStrength -= strength;
            }

            this.CurrentLength--;
        }

        private void Reset()
        {
            this.CurrentStrength = 0;
            this.CurrentLength = 0;

            this.MaxStrength = 0;
            this.LongestLength = 0;
            this.LongestStrength = 0;
        }
    }
}