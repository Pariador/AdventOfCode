namespace TheSumOfItsParts
{
    using System;
    using System.IO;
    using System.Collections.Generic;

    static class Start
    {
        static void Main(string[] args)
        {
            string file = "input.txt";
            if (args.Length > 0)
            {
                file = args[0];
            }

            var reader = new StreamReader(file);

            int workersCount = ParseNum(reader.ReadLine());
            int baseTime = ParseNum(reader.ReadLine());

            var dependencies = ReadDependencies(reader);

            reader.Dispose();

            Resolver resolver = new Resolver(dependencies);

            string assemblyOrder = resolver.FindAssemblyOrder();

            Console.WriteLine($"Assembly order: {assemblyOrder}");

            int assemblyTime = resolver.CalcAssemblyTime(workersCount, baseTime);

            Console.WriteLine($"Assembly time: {assemblyTime}");
        }

        static int ParseNum(string str)
        {
            return int.Parse(str.Split(' ')[2]);
        }

        static SortedDictionary<char, List<char>> ReadDependencies(TextReader reader)
        {
            var dependencies = new SortedDictionary<char, List<char>>();

            for (string line = reader.ReadLine(); line != null; line = reader.ReadLine())
            {
                string[] words = line.Split(' ');

                char step = words[7][0];
                if (!dependencies.ContainsKey(step))
                {
                    dependencies[step] = new List<char>();
                }

                char dependency = words[1][0];
                if (!dependencies.ContainsKey(dependency))
                {
                    dependencies[dependency] = new List<char>();
                }

                dependencies[step].Add(dependency);
            }

            return dependencies;
        }
    }
}