namespace InverseCaptcha
{
    using System;
    using System.IO;

    internal static class Program
    {
        internal static int ToInt(char digit)
        {
            if (!char.IsDigit(digit))
            {
                throw new ArgumentException("Char must be a digit.");
            }

            return digit - '0';
        }

        internal static long SumRepeatingDigits(string number)
        {
            long sum = 0;

            for (int i = 0; i < number.Length - 1; i++)
            {
                if (number[i] == number[i + 1])
                {
                    sum += ToInt(number[i]);        
                }
            }

            if (number[number.Length - 1] == number[0])
            {
                sum += ToInt(number[0]);
            }

            return sum;
        }

        internal static void Main()
        {
            string puzzleInput = null;

            using (var reader = new StreamReader("input.txt"))
            {
                puzzleInput = reader.ReadToEnd();
            }

            while (true)
            {
                Console.Write(">");

                string input = Console.ReadLine().ToLower();
                if (input == "exit")
                {
                    break;
                }
                else if (input == "puzzle")
                {
                    input = puzzleInput;
                }

                long sum = SumRepeatingDigits(input);

                Console.WriteLine(sum + Environment.NewLine);
            }
        }
    }
}