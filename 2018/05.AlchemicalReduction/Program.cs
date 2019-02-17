namespace AlchemicalReduction
{
    using System;
    using System.IO;
    using System.Collections.Generic;
    using System.Linq;

    static class Program
    {
        static void Main()
        {
            string text = File.ReadAllText("input.txt").Trim();
            var polymer = new LinkedList<char>(text);

            int fullyReactedPolymerLength = FullyReact(polymer).Count;
            Console.WriteLine($"Polymer Length: {fullyReactedPolymerLength}");

            Console.WriteLine("\nFinding shortest polymer...");
            int shortestPolymer = FindShortestPolymer(polymer);
            Console.WriteLine($"Shortest Polymer: {shortestPolymer}");
        }

        static bool Trigger(LinkedList<char> polymer)
        {
            bool triggered = false;

            var node = polymer.First;
            while (node != null && node.Next != null)
            {
                if ((char.ToLower(node.Value) == char.ToLower(node.Next.Value)) &&
                    (node.Value != node.Next.Value))
                {
                    triggered = true;

                    var delete = new LinkedListNode<char>[] { node, node.Next };

                    node = node.Next.Next;

                    polymer.Remove(delete[0]);
                    polymer.Remove(delete[1]);
                }
                else
                {
                    node = node.Next;
                }
            }

            return triggered;
        }

        static LinkedList<char> FullyReact(LinkedList<char> polymer)
        {
            while (Trigger(polymer));

            return polymer;
        }

        static LinkedList<char> Unblock(LinkedList<char> polymer, char type)
        {
            var node = polymer.First;
            while (node != null)
            {
                if (char.ToLower(node.Value) == char.ToLower(type))
                {
                    var delete = node;
                    node = node.Next;
                    polymer.Remove(delete);
                }
                else
                {
                    node = node.Next;
                }
            }

            return polymer;
        }

        static int FindShortestPolymer(LinkedList<char> polymer)
        {
            polymer = FullyReact(polymer);

            char[] alphabet = new char[26];
            for (int i = 0; i < alphabet.Length; i++)
            {
                alphabet[i] = (char)(i + 65);
            }

            return alphabet.Select(type =>
            {
                var unblocked = Unblock(new LinkedList<char>(polymer), type);

                return FullyReact(unblocked).Count;
            }).Min();
        }
    }
}