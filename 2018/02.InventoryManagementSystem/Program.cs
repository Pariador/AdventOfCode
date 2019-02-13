namespace InventoryManagementSystem
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.IO;

    static class Program
    {
        static void Main()
        {
            string[] ids = ReadIds();
            var counts = ids.Select(id => CountChars(id));

            var idsWithOneRepetition = counts.Count(count => count.Values.Contains(2));
            var idsWithTwoRepeptiions = counts.Count(count => count.Values.Contains(3));

            int checksum = idsWithOneRepetition * idsWithTwoRepeptiions;

            Console.WriteLine($"Checksum: {checksum}");

            string root = FindRoot(ids);

            Console.WriteLine($"Root: {root}");
        }

        static string[] ReadIds()
        {
            var ids = new List<string>();

            using (var reader = new StreamReader("input.txt"))
            {
                while (true)
                {
                    string line = reader.ReadLine();
                    if (line == null)
                    {
                        break;
                    }

                    ids.Add(line);
                }
            }

            return ids.ToArray();
        }

        static Dictionary<char, int> CountChars(string str)
        {
            var counts = new Dictionary<char, int>();

            foreach (var @char in str)
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

            return counts;
        }

        static string GetRoot(string first, string second)
        {
            int position = -1;

            for (int i = 0; i < first.Length; i++)
            {
                if (first[i] == second[i])
                {
                    continue;
                }

                if (position != -1)
                {
                    return null;
                }

                position = i;
            }

            string root = first.Substring(0, position) + first.Substring(position + 1);

            return root;
        }

        static string FindRoot(string[] ids)
        {
            for (int f = 0; f < ids.Length - 1; f++)
            {
                for (int s = f + 1; s < ids.Length; s++)
                {
                    string root = GetRoot(ids[f], ids[s]);
                    if (root != null)
                    {
                        return root;
                    }
                }
            }

            return null;
        }
    }
}