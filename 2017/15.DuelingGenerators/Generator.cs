namespace DuelingGenerators
{
    public class Generator
    {
        private const long Divider = 2147483647;

        public Generator(long seed, long factor)
        {
            this.Previous = seed;
            this.Factor = factor;
        }

        public long Previous { get; private set; }

        public long Factor { get; private set; }

        public virtual long Next()
        {
            this.Previous = (this.Previous * this.Factor) % Generator.Divider;

            return this.Previous;
        }
    }
}