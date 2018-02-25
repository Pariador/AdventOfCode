namespace SignalsAndNoise
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;

    public static class Program
    {
        public static void Main()
        {
            string[] data = ReadData("input.txt");

            string[] words = ToWords(data);

            string message = new string(
                words.Select(
                    word => Decode(word, dict => dict.OrderByDescending(pair => pair.Value))
                ).ToArray()
            );

            Console.WriteLine($"First message: {message}");

            message = new string(
                words.Select(
                    word => Decode(word, dict => dict.OrderBy(pair => pair.Value))
                ).ToArray()
            );

            Console.WriteLine($"Second message: {message}");
        }

        public static char Decode(string word, Func<Dictionary<char, int>, IEnumerable<KeyValuePair<char, int>>> orderBy)
        {
            var counts = new Dictionary<char, int>();

            foreach (var @char in word)
            {
                if (counts.ContainsKey(@char))
                {
                    counts[@char]++;
                }
                else
                {
                    counts[@char] = 1;
                }
            }

            return orderBy(counts)
                .First()
                .Key;
        }

        public static string[] ToWords(string[] data)
        {
            StringBuilder builder = new StringBuilder();

            var words = new List<string>();

            for (int col = 0; col < data[0].Length; col++)
            {
                for (int row = 0; row < data.Length; row++)
                {
                    builder.Append(data[row][col]);
                }

                words.Add(builder.ToString());
                builder.Clear();
            }

            return words.ToArray();
        }

        public static string[] ReadData(string fileName)
        {
            var data = new List<string>();

            using (var reader = new StreamReader(fileName))
            {
                while (true)
                {
                    string line = reader.ReadLine();
                    if (line == null)
                    {
                        break;
                    }

                    data.Add(line);
                }
            }

            return data.ToArray();
        }
    }
}