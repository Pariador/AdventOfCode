namespace RecursiveCircus
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.IO;

    public static class Program
    {
        public static void Main()
        {
            Dictionary<string, List<string>> tree;
            Dictionary<string, int> values;
            using (var reader = new StreamReader("input.txt"))
            {
                tree = ReadTree(reader, out values);
            }

            string root = FindRoot(tree);

            string parent = null;
            var unbalancedNode = FindUnbalancedNode(tree, values, root, ref parent);

            int balancedValue = BalanceNode(tree, values, unbalancedNode, parent);

            Console.WriteLine($"Root: {root}");
            Console.WriteLine($"Balanced Value: {balancedValue}");
        }

        public static int BalanceNode(Dictionary<string, List<string>> tree, Dictionary<string, int> values, string node, string parent)
        {
            string sibling = tree[parent].First(child => child != node);

            int unbalancedWeight = GetWeight(tree, values, node);
            int balancedWeight = GetWeight(tree, values, sibling);

            int balanceDiff = balancedWeight - unbalancedWeight;

            values[node] += balanceDiff;

            return values[node];
        }

        public static string FindUnbalancedNode(Dictionary<string, List<string>> tree, Dictionary<string, int> values, string node, ref string parent)
        {
            string unbalancedChild = GetUnbalancedChild(tree, values, node);

            if (unbalancedChild != null)
            {
                parent = node;
                return FindUnbalancedNode(tree, values, unbalancedChild, ref parent);
            }

            if (parent == null)
            {
                return null;
            }

            return node;
        }

        public static string GetUnbalancedChild(Dictionary<string, List<string>> tree, Dictionary<string, int> values, string node)
        {
            if (tree[node].Count < 2)
            {
                return null;
            }

            var weights = tree[node].Select(child => GetWeight(tree, values, child))
                .ToArray();

            var counts = weights.CountEquals();

            if (counts.Count == 1)
            {
                return null;
            }

            int unbalancedWeight = counts.First(count => count.Value == counts.Min(c => c.Value)).Key;

            return tree[node][weights.IndexOf(unbalancedWeight)];
        }

        public static int GetWeight(Dictionary<string, List<string>> tree, Dictionary<string, int> values, string node)
        {
            if (!tree[node].Any())
            {
                return values[node];
            }

            return values[node] + tree[node].Sum(child => GetWeight(tree, values, child));
        }

        public static string FindRoot(Dictionary<string, List<string>> tree)
        {
            return tree.FirstOrDefault(pair => tree.All(p => !p.Value.Contains(pair.Key))).Key;
        }

        public static Dictionary<string, List<string>> ReadTree(TextReader reader, out Dictionary<string, int> values)
        {
            var tree = new Dictionary<string, List<string>>();
            values = new Dictionary<string, int>();

            while (true)
            {
                string line = reader.ReadLine();
                if (line == "end")
                {
                    return tree;
                }

                string[] words = line.Split(new[] {" ", ", " }, StringSplitOptions.RemoveEmptyEntries);

                string name = words[0];

                tree[name] = new List<string>();
                values[name] = int.Parse(words[1].Substring(1, words[1].Length - 2));

                if (words.Length <= 2)
                {
                    continue;
                }

                for (int i = 3; i < words.Length; i++)
                {
                    tree[name].Add(words[i]);
                }
            }

            return tree;
        }
    }
}