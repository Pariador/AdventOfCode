namespace Orbits
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Collections.Generic;

    static class Program
    {
        const string Root = "COM";

        static void Main()
        {
            var orbits = ReadOrbits();
            int total = TotalOrbits(orbits);
            int distance = ShortestPath(orbits, "YOU", "SAN") - 2;

            Console.WriteLine($"Total Orbits: {total}");
            Console.WriteLine($"Shortest Path: {distance}");
        }

        static int TotalOrbits(Dictionary<string, List<string>> orbits)
        {
            int sum = 0;

            var frontier = new Queue<string>();
            var visited = new HashSet<string>();

            frontier.Enqueue(Root);
            visited.Add(Root);

            int level = 0;
            while (frontier.Any())
            {
                int levelCount = frontier.Count;

                sum += level * levelCount;

                for (int i = 0; i < levelCount; i++)
                {
                    var current = frontier.Dequeue();

                    foreach (var neighbor in orbits[current].Where(child => !visited.Contains(child)))
                    {
                        frontier.Enqueue(neighbor);
                        visited.Add(neighbor);
                    }
                }

                level++;
            }

            return sum;
        }

        static int ShortestPath(Dictionary<string, List<string>> orbits, string source, string target)
        {
            int sum = 0;

            var frontier = new Queue<string>();
            var visited = new HashSet<string>();

            frontier.Enqueue(source);
            visited.Add(Root);

            int level = 0;
            while (frontier.Any())
            {
                int levelCount = frontier.Count;

                sum += level * levelCount;

                for (int i = 0; i < levelCount; i++)
                {
                    var current = frontier.Dequeue();
                    if (current == target) return level;

                    foreach (var neighbor in orbits[current].Where(child => !visited.Contains(child)))
                    {
                        frontier.Enqueue(neighbor);
                        visited.Add(neighbor);
                    }
                }

                level++;
            }

            return -1;
        }

        static Dictionary<string, List<string>> ReadOrbits(string path = "input.txt")
        {
            var orbits = new Dictionary<string, List<string>>();

            string[] lines = File.ReadAllLines(path);
            foreach (var line in lines)
            {
                string[] pair = line.Split(')');
                string parent = pair[0];
                string child = pair[1];

                if (!orbits.ContainsKey(parent))
                    orbits[parent] = new List<string>();

                if (!orbits.ContainsKey(child))
                    orbits[child] = new List<string>();

                orbits[parent].Add(child);
                orbits[child].Add(parent);
            }

            return orbits;
        }
    }
}