namespace DigitalPlumber
{
    using System;
    using System.IO;
    using System.Collections.Generic;
    using System.Linq;

    public static class Program
    {
        public static void Main()
        {
            int[][] graph;
            using (var reader = new StreamReader("input.txt"))
            {
                graph = ReadGraph(reader);
            }

            var component = Traverse(graph, 0);
            var componentsCount = CountComponents(graph);

            Console.WriteLine($"Nodes connected to 0: {component.Count}");
            Console.WriteLine($"Components count: {componentsCount}");
        }

        public static int CountComponents(int[][] graph)
        {
            int componentsCount = 0;

            var unvisited = GetAllNodes(graph.Length);

            while (unvisited.Any())
            {
                var visited = Traverse(graph, unvisited.First());

                unvisited.ExceptWith(visited);

                componentsCount++;
            }

            return componentsCount;
        }

        public static HashSet<int> Traverse(int[][] graph, int start)
        {
            var visited = new HashSet<int>();
            var next = new Stack<int>();
            next.Push(start);

            while (next.Any())
            {
                var current = next.Pop();

                foreach (var neighbor in graph[current].Where(node => !visited.Contains(node)))
                {
                    visited.Add(neighbor);
                    next.Push(neighbor);
                }
            }

            return visited;
        }

        public static int[][] ReadGraph(TextReader reader)
        {
            var graph = new List<int[]>();

            while (true)
            {
                string line = reader.ReadLine();
                if (line == null)
                {
                    return graph.ToArray();
                }

                int[] neighbors = line.Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Skip(2)
                    .Select(int.Parse)
                    .ToArray();

                graph.Add(neighbors);
            }

            return graph.ToArray();
        }

        public static HashSet<int> GetAllNodes(int count)
        {
            var nodes = new HashSet<int>();

            for (int i = 0; i < count; i++)
            {
                nodes.Add(i);
            }

            return nodes;
        }
    }
}