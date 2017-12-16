namespace PacketScanners
{
    using System;
    using System.Linq;
    using System.IO;
    using System.Collections.Generic;

    public static class Program
    {
        public static void Main()
        {
            Dictionary<int, int> record;
            using (var reader = new StreamReader("input.txt"))
            {
                record = ReadRecord(reader);
            }

            Console.WriteLine("Computing...");

            Scanner[] layers = InitLayers(record);

            layers.Save();

            int score = RunThrough(layers);

            layers.Restore();

            // needs optimization
            int safeDelay = FindSafeDelay(layers);

            Console.WriteLine($"Score: {score}");
            Console.WriteLine($"Safe deley: {safeDelay}");
        }

        public static int FindSafeDelay(Scanner[] layers)
        {
            int delay = 0;

            while (true)
            {
                int depth = -1;

                bool safe = layers.All(scanner =>
                {
                    depth++;

                    if (scanner == null)
                    {
                        return true;
                    }

                    return IsSafe(scanner, depth);
                }); 

                if (safe)
                {
                    return delay;
                }

                delay++;

                foreach (var scanner in layers.Where(scanner => scanner != null))
                {
                    scanner.Move();
                }
            }

            return delay;
        }

        public static bool IsSafe(Scanner scanner, int depth)
        {
            scanner.Save();

            for (int t = 0; t < depth; t++)
            {
                scanner.Move();
            }

            bool isSafe = false;

            if (scanner.Position != 0)
            {
                isSafe = true;
            }

            scanner.Restore();

            return isSafe;
        }

        public static int RunThrough(Scanner[] layers)
        {
            int score = 0;

            for (int i = 0; i < layers.Length; i++)
            {
                if (layers[i] != null && layers[i].Position == 0)
                {
                    score += i * layers[i].Range;
                }

                foreach (var scanner in layers.Where(scanner => scanner != null))
                {
                    scanner.Move();
                }
            }

            return score;
        }

        public static Dictionary<int, int> ReadRecord(TextReader reader)
        {
            var record = new Dictionary<int, int>();

            while (true)
            {
                string line = reader.ReadLine();
                if (line == null)
                {
                    break;
                }

                var data = line.Split(new[] { ": " }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse);

                record[data.First()] = data.Last();
            }

            return record;
        }

        public static Scanner[] InitLayers(Dictionary<int, int> record)
        {
            Scanner[] layers = new Scanner[record.Keys.Max() + 1];

            foreach (var pair in record)
            {
                layers[pair.Key] = new Scanner(pair.Value);
            }

            return layers;
        }

        public static void Save(this IEnumerable<Scanner> scanners) 
        {
            foreach (var scanner in scanners)
            {
                if (scanner != null)
                {
                    scanner.Save();
                }
            }
        }

        public static void Restore(this IEnumerable<Scanner> scanners)
        {
            foreach (var scanner in scanners)
            {
                if (scanner != null)
                {
                    scanner.Restore();
                }
            }
        }
    }
}