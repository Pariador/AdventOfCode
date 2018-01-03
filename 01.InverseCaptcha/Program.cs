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

        internal static long SumRepeatingDigits(string number, int step)
        {
            long sum = 0;

            for (int i = 0; i < number.Length; i++)
            {
                if (number[i] == number[(i + step) % number.Length])
                {
                    sum += ToInt(number[i]);        
                }
            }

            return sum;
        }

        internal static void Main()
        {
            bool oneStep = true;

            while (true)
            {
                Console.Write(">");

                string input = Console.ReadLine().ToLower();
                if (input == "exit")
                {
                    break;
                }
                else if (input == "file")
                {
                    string fileName = Console.ReadLine();

                    using (var reader = new StreamReader(fileName))
                    {
                        input = reader.ReadToEnd();
                    }
                }
                else if (input == "one")
                {
                    oneStep = true;
                    continue;
                }
                else if (input == "half")
                {
                    oneStep = false;
                    continue;
                }

                int step = oneStep ? 1 : input.Length / 2;

                long sum = SumRepeatingDigits(input, step);

                Console.WriteLine(sum + Environment.NewLine);
            }
        }
    }
}