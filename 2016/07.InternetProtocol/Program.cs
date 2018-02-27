namespace InternetProtocol
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public static class Program
    {
        public const char OpenHypernet = '[';

        public const char CloseHypernet = ']';

        public static void Main()
        {
            string[] adresses = ReadAdresses("input.txt");

            int count = adresses.Count(SupportsTSL);
            Console.WriteLine($"TSL supported adresses: {count}");

            count = adresses.Count(SupportsSSL);
            Console.WriteLine($"SSL supported adresses: {count}");
        }

        public static bool SupportsTSL(string adress)
        {
            int hypernetLevel = 0;
            bool result = false;

            for (int i = 0; i < adress.Length - 3; i++)
            {
                if (adress[i] == OpenHypernet)
                {
                    hypernetLevel++;
                }
                else if (adress[i] == CloseHypernet)
                {
                    hypernetLevel--;
                }

                if (adress[i] != adress[i + 1] && adress[i] == adress[i + 3] && adress[i + 1] == adress[i + 2])
                {
                    if (hypernetLevel == 0)
                    {
                        result = true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            return result;
        }

        public static bool SupportsSSL(string adress)
        {
            int hypernetLevel = 0;
            bool result = false;

            var supernetSequences = new List<string>();
            var hypernetSequences = new List<string>();

            for (int i = 0; i < adress.Length - 2; i++)
            {
                if (adress[i] == OpenHypernet)
                {
                    hypernetLevel++;
                }
                else if (adress[i] == CloseHypernet)
                {
                    hypernetLevel--;
                }

                if (adress[i] != adress[i + 1] && adress[i] == adress[i + 2])
                {
                    string sequence = adress.Substring(i, 3);

                    if (hypernetLevel == 0)
                    {
                        supernetSequences.Add(sequence);
                    }
                    else
                    {
                        hypernetSequences.Add(sequence);
                    }
                }
            }

            return supernetSequences.Any(sequence => hypernetSequences.Contains(ReverseABA(sequence)));
        }

        public static string ReverseABA(string str)
        {
            return new string(new char[] { str[1], str[0], str[1] });
        }

        public static string[] ReadAdresses(string fileName)
        {
            var adresses = new List<string>();

            using (var reader = new StreamReader(fileName))
            {
                while (true)
                {
                    string adress = reader.ReadLine();
                    if (adress == null)
                    {
                        break;
                    }

                    adresses.Add(adress);
                }
            }

            return adresses.ToArray();
        }
    }
}