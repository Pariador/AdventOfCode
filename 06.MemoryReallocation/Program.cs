namespace MemoryReallocation
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    public static class Program
    {
        public static void Main()
        {
            //byte[] banks = { 0, 2, 7, 0 };
            byte[] banks = { 4, 10, 4, 1, 8, 4, 9, 14, 5, 1, 14, 15, 0, 15, 3, 5 };

            int count = CountToDuplicateDistribution(banks, out int duplicatesDistance);

            Console.WriteLine($"Count to duplicate distribution: {count}");
            Console.WriteLine($"Duplicates distance: {duplicatesDistance}");
        }

        public static int CountToDuplicateDistribution(byte[] banks, out int duplicatesDistance)
        {
            var distributions = new Dictionary<byte[], int>(new BytesEqualityComparer());

            while (!distributions.ContainsKey(banks))
            {
                distributions[banks] = distributions.Count;

                banks = Redistribute(banks);
            }

            duplicatesDistance = distributions.Count - distributions[banks];
            return distributions.Count;
        }

        public static byte[] Redistribute(byte[] banks)
        {
            byte[] distribution = banks.ToArray();

            int maxIndex = FindMaxIndex(distribution);

            int blocks = distribution[maxIndex];
            distribution[maxIndex] = 0;

            int start = NextIndex(maxIndex, distribution.Length);
            for (int b = 0, i = start; b < blocks; b++, i = NextIndex(i, distribution.Length))
            {
                distribution[i]++;
            }

            return distribution;
        }

        public static int NextIndex(int i, int length)
        {
            return (i + 1) % length;
        }

        public static int FindMaxIndex(byte[] bytes)
        {
            if (!bytes.Any())
            {
                throw new ArgumentException("Bytes can't be empty!");
            }

            int index = 0;
            int max = bytes[0];

            for (int i = 1; i < bytes.Length; i++)
            {
                if (bytes[i] > max)
                {
                    max = bytes[i];
                    index = i;
                }
            }

            return index;
        }
    }
}