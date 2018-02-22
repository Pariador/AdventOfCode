namespace DuelingGenerators
{
    public class AdvancedGenerator : Generator
    {
        public AdvancedGenerator(long seed, long factor, int filter)
            : base(seed, factor)
        {
            this.Filter = filter;
        }


        public int Filter { get; private set; }

        public override long Next()
        {
            while (base.Next() % this.Filter != 0)
            {
                ;
            }

            return this.Previous;
        }
    }
}