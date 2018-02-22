namespace DuelingGenerators
{
    using System;

    public static class Program
    {
        public static void Main()
        {
            Console.WriteLine("Computing...");

            Generator genA = new Generator(seed: 873/*65*/, factor: 16807);
            Generator genB = new Generator(seed: 583/*8921*/, factor: 48271);

            int matches = CountShortEquals(genA, genB, 40000000);

            Console.WriteLine($"Matches#1: {matches}");

            genA = new AdvancedGenerator(seed: 873/*65*/, factor: 16807, filter: 4);
            genB = new AdvancedGenerator(seed: 583/*8921*/, factor: 48271, filter: 8);

            matches = CountShortEquals(genA, genB, 5000000);

            Console.WriteLine($"Matches#2: {matches}");
        }

        public static int CountShortEquals(Generator genA, Generator genB, int length = 5)
        {
            int count = 0;  

            for (int i = 0; i < length; i++)
            {
                long first = genA.Next();
                long second = genB.Next();

                if (EqualsAsShort(first, second))
                {
                    count++;
                }
            }

            return count;
        }

        public static bool EqualsAsShort(long first, long second)
        {
            short f = (short)first;
            short s = (short)second;

            return f == s;
        }
    }
}