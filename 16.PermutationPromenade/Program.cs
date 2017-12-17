namespace PermutationPromenade
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public static class Program
    {
        public static readonly Dictionary<string, string> Cash = new Dictionary<string, string>();

        public static void Main()
        {
            char[] chars = InitChars(16);

            string input = null;
            using (var reader = new StreamReader("input.txt"))
            {
                input = reader.ReadToEnd();
            }

            string[] commands = input.Split(',');


            chars = ExecuteCommands(chars, commands);

            Console.WriteLine($"Programs after 1 dance: {new string(chars)}");

            Console.WriteLine("Computing 1000000000 dances...");
            for (long i = 0; i < 999999999; i++)
            {            
                if ((i % 10000000) == 0)
                {
                    Console.WriteLine($"{i / 10000000}% done...");
                }

                chars = ExecuteCommands(chars, commands);
            }

            Console.WriteLine($"Programs after 1000000000 dances: {new string(chars)}");
        }

        public static char[] ExecuteCommands(char[] chars, string[] commands)
        {
            string before = new string(chars);

            if (Cash.ContainsKey(before))
            {
                return Cash[before].ToCharArray();
            }

            foreach (var command in commands)
            {
                chars = ExecuteCommand(chars, command);
            }

            string after = new string(chars);

            Cash[before] = after;

            return chars;
        }

        public static char[] ExecuteCommand(char[] chars, string command)
        {
            char type = command[0];
            string data = command.Substring(1);

            if (type == 's')
            {
                return Spin(chars, int.Parse(data));
            }
            else if (type == 'x')
            {
                int[] indices = data.Split('/')
                    .Select(int.Parse)
                    .ToArray();

                return Exchange(chars, indices[0], indices[1]);
            }
            else if (type == 'p')
            {
                return Partner(chars, data[0], data[2]);
            }
            else
            {
                throw new ArgumentException("Unknown command!");
            }
        }

        public static char[] Spin(char[] chars, int length)
        {
            if (length < 0 || length > chars.Length)
            {
                throw new ArgumentException("Length can't be less than 0 or bigger than chars.Length!");
            }

            char[] result = new char[chars.Length];

            for (int i = 0; i < length; i++)
            {
                result[i] = chars[chars.Length - length + i];
            }

            for (int i = length; i < chars.Length; i++)
            {
                result[i] = chars[i - length];
            }

            return result;
        }

        public static char[] Exchange(char[] chars, int first, int second)
        {
            Utils.ValidateIndex(first, chars.Length);
            Utils.ValidateIndex(second, chars.Length);

            char temp = chars[first];

            chars[first] = chars[second];
            chars[second] = temp;

            return chars;
        }

        public static char[] Partner(char[] chars, char first, char second)
        {
            int firstIndex = chars.IndexOf(first);
            if (firstIndex == -1)
            {
                throw new ArgumentException("First char not found.");
            }

            int secondIndex = chars.IndexOf(second);
            if (secondIndex == -1)
            {
                throw new ArgumentException("Second char not found.");
            }

            return Exchange(chars, firstIndex, secondIndex);
        }

        public static char[] InitChars(int length)
        {
            char[] chars = new char[length];

            for (int i = 0; i < length; i++)
            {
                chars[i] = (char)(i + 97);
            }

            return chars;
        }
    }
}