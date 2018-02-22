namespace HighEntropyPassphrases
{
    using System;
    using System.IO;
    using System.Collections.Generic;

    public static class Program
    {
        public static void Main()
        {
            Console.Write("Read from file (y/n)?: ");
            bool readFromFile = Console.ReadLine() == "y";

            TextReader reader = Console.In;

            if (readFromFile)
            {
                reader = new StreamReader("input.txt");
            }

            Solution(reader);

            reader.Dispose();
        }

        public static void Solution(TextReader reader)
        {
            int validPhrasesCount = 0;

            Console.Write("No anagrams (y/n)?: ");
            bool noAnagrams = Console.ReadLine() == "y";

            while (true)
            {
                string line = reader.ReadLine();
                if (line == "end")
                {
                    break;
                }

                string[] phrase = line.Split(' ');

                if (IsValidPhrase(phrase, noAnagrams))
                {
                    validPhrasesCount ++;
                }
            }

            Console.WriteLine($"Valid phrases: {validPhrasesCount}");
        }

        public static bool IsValidPhrase(string[] phrase, bool noAnagrams = false)
        {
            HashSet<string> words = null;

            if (noAnagrams)
            {
                words = new HashSet<string>(new AnagramComparer());
            }
            else
            {
                words = new HashSet<string>();
            }

            foreach (var word in phrase)
            {
                if (words.Contains(word))
                {
                    return false;
                }

                words.Add(word);
            }

            return true;
        }
    }
}