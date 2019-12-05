namespace SecureContainer
{
    using System;

    class Program
    {
        static void Main()
        {
            const int From = 236491, To = 713787;

            int keys = CountValidKeys(From, To, exact: false);
            int exactKeys = CountValidKeys(From, To, exact: true);

            Console.WriteLine($"Keys: {keys}");
            Console.WriteLine($"Exact Keys: {exactKeys}");
        }

        static int CountValidKeys(int from, int to, bool exact = false)
        {
            int count = 0;

            for (int i = from; i <= to; i++)
            {
                string number = i.ToString();

                bool hasPair = exact ? HasExactPair(number) : HasPair(number);
                if (hasPair && NonDecreasing(number))
                    count++;
            }

            return count;
        }

        static bool NonDecreasing(string number)
        {
            for (int i = 1; i < number.Length; i++)
            {
                if (number[i - 1] > number[i])
                    return false;
            }

            return true;
        }

        static bool HasPair(string number)
        {
            for (int i = 1; i < number.Length; i++)
            {
                if (number[i - 1] == number[i])
                    return true;
            }

            return false;
        }

        static bool HasExactPair(string number)
        {
            bool result = false;

            for (int i = 1, len = number.Length; i < len; i++)
            {
                int count = 1;
                for (; i < len && number[i - 1] == number[i]; count++, i++);

                if (count == 2)
                    return true;
            }

            return false;
        }
    }
}