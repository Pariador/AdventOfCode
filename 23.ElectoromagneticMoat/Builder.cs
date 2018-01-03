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

        public int LongestLength { get; private set; }

        public int LongestStrength { get; private set; }

        public int FindStrongest(HashSet<Component> components)
        {
            return FindStrongest(components, pin: 0, length: 0, currentStrength: 0);
        }

        private int FindStrongest(HashSet<Component> components, int pin, int length, int currentStrength)
        {
            var compatibles = components.Where(comp => comp.Left == pin || comp.Right == pin)
                .ToArray();

            if (!compatibles.Any())
            {
                return 0;
            }

            length++;

            int maxStrength = 0;
            foreach (var compatible in compatibles)
            {
                components.Remove(compatible);

                int nextPin = 0;
                if (compatible.Left == pin)
                {
                    nextPin = compatible.Right;
                }
                else
                {
                    nextPin = compatible.Left;
                }

                int strength = compatible.Left + compatible.Right;

                strength += this.FindStrongest(components, nextPin, length, currentStrength + strength);

                components.Add(compatible);

                if ((this.LongestLength < length) || (this.LongestLength == length && this.LongestStrength < currentStrength + strength))
                {
                    this.LongestLength = length;
                    this.LongestStrength = currentStrength + strength;
                }

                if (strength > maxStrength)
                {
                    maxStrength = strength;
                }
            }

            return maxStrength;
            
        }

        public void Reset()
        {
            this.LongestLength = 0;
            this.LongestLength = 0;
        }
    }
}