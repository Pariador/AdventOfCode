namespace ChronalCalibration
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    static class Program
    {
        static void Main()
        {
            int[] numbers = ReadNumbers();
            int sum = Sum(numbers);
            int firstRepeatingSum = FindFirstRepeatingSum(numbers);

            Console.WriteLine($"Sum: {sum}");
            Console.WriteLine($"First Repeating Sum: {firstRepeatingSum}");
        }

        static int[] ReadNumbers()
        {
            var numbers = new List<int>();

            using (var reader = new StreamReader("input.txt"))
            {
                while (true)
                {
                    string line = reader.ReadLine();
                    if (line == null)
                    {
                        break;
                    }

                    numbers.Add(int.Parse(line));
                }
            }

            return numbers.ToArray();
        }

        static int Sum(int[] numbers)
        {
            int sum = 0;

            for (int i = 0; i < numbers.Length; i++)
            {
                sum += numbers[i];
            }

            return sum;
        }

        static int FindFirstRepeatingSum(int[] numbers)
        {
            var sums = new HashSet<int>();
            int sum = 0;

            for (int i = 0; true; i = (i + 1) % numbers.Length)
            {
                if (sums.Contains(sum))
                {
                    return sum;
                }

                sums.Add(sum);

                sum += numbers[i];
            }
        }
    }
}