namespace MemoryManeuver
{
    using System;
    using System.IO;
    using System.Linq;

    static class Start
    {
        static void Main(string[] args)
        {
            string file = "input.txt";
            if (args.Length > 0)
            {
                file = args[0];
            }

            int[] data = File.ReadAllText(file)
                .Split(' ')
                .Select(int.Parse)
                .ToArray();

            var root = ParseTree(data, out int end);

            int sum = SumTreeData(root);

            int value = CalcTreeValue(root);

            Console.WriteLine($"Tree metadata sum: {sum}");
            Console.WriteLine($"Tree value: {value}");
        }

        static Node<int[]> ParseTree(int[] data, out int end, int start = 0)
        {
            var node = new Node<int[]>();

            int childrenCount = data[start];
            node.Value = new int[data[start + 1]];

            end = start + 1;
            for (int i = 0; i < childrenCount; i++)
            {
                node.Children.Add(ParseTree(data, out end, end + 1));
            }

            Array.Copy(data, end + 1, node.Value, 0, node.Value.Length);

            end += node.Value.Length;

            return node;
        }

        static int SumTreeData(Node<int[]> node)
        {
            int sum = node.Value.Sum();

            foreach (var child in node.Children)
            {
                sum += SumTreeData(child);
            }

            return sum;
        }

        static int CalcTreeValue(Node<int[]> node)
        {
            int value = 0;

            if (!node.Children.Any())
            {
                value = node.Value.Sum();
            }
            else
            {
                for (int i = 0; i < node.Value.Length; i++)
                {
                    int child = node.Value[i] - 1;

                    if (0 <= child && child < node.Children.Count)
                    {
                        value += CalcTreeValue(node.Children[child]);
                    }
                }
            }

            return value;
        }
    }
}