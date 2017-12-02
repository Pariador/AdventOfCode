namespace CorruptionChecksum
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal static class Program
    {
        internal static int DiffrenceChecksum(int[][] table)
        {
            return table.Sum(row => row.Max() - row.Min());
        }

        internal static int EvenlyDevisibleChecksum(int[][] table)
        {
            return table.Sum(row => DivideFirstDevisibles(row));
        }

        internal static int DivideFirstDevisibles(int[] numbers)
        {
            for (int i = 0; i < numbers.Length - 1; i++)
            {
                for (int j = i + 1; j < numbers.Length; j++)
                {
                    if (numbers[i] % numbers[j] == 0)
                    {
                        return numbers[i] / numbers[j];
                    }
                    else if (numbers[j] % numbers[i] == 0)
                    {
                        return numbers[j] / numbers[i];
                    }
                }
            }

            throw new ArgumentException("No divisibles.");
        }

        internal static void Main()
        {
            bool useDiffrenceChecksum = true;

            Console.Write("Use diffrence checksum (y/n)?: ");
            string answer = Console.ReadLine();

            if (answer == "y")
            {
                useDiffrenceChecksum = true;
            }
            else if (answer == "n")
            {
                useDiffrenceChecksum = false;
            }
            else
            {
                throw new ApplicationException("Answer must be 'y' or 'n'!");
            }

            Console.WriteLine("Enter talbe:");

            var table = new List<int[]>();

            while (true)
            {
                string input = Console.ReadLine();
                if (input == "end")
                {
                    break;
                }

                int[] row = input.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(str => int.Parse(str.Trim()))
                    .ToArray();

                table.Add(row);
            }

            int checksum = useDiffrenceChecksum ? DiffrenceChecksum(table.ToArray()) : EvenlyDevisibleChecksum(table.ToArray());

            Console.Write("\nChecksum: ");
            Console.WriteLine(checksum);

        }
    }
}