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
            char[] polymer = File.ReadAllText("input.txt")
                .Trim()
                .ToCharArray();

            int fullyReactedPolymerLength = FullyReact(polymer).Length;
            Console.WriteLine($"Polymer Length: {fullyReactedPolymerLength}");


            Console.WriteLine("\nFinding shortest polymer...");
            int shortestPolymer = FindShortestPolymer(polymer);
            Console.WriteLine($"Shortest Polymer: {shortestPolymer}");
        }

        static char[] Trigger(char[] polymer, out bool triggered)
        {
            const char Destroyed = '*';

            triggered = false;

            for (int i = 0; i < polymer.Length - 1; i++)
            {
                if ((char.ToLower(polymer[i]) == char.ToLower(polymer[i + 1])) &&
                    (polymer[i] != polymer[i + 1])) 
                {
                    triggered = true;
                    polymer[i] = Destroyed;
                    polymer[i + 1] = Destroyed;
                    i++;
                }
            }

            return polymer.Where(unit => unit != Destroyed)
                .ToArray();
        }

        static char[] FullyReact(char[] polymer)
        {
            bool triggered = false;
            do
            {
                polymer = Trigger(polymer, out triggered);
            } while (triggered);

            return polymer;
        }

        static int FindShortestPolymer(char[] polymer)
        {
            polymer = FullyReact(polymer);

            char[] alphabet = new char[26];
            for (int i = 0; i < alphabet.Length; i++)
            {
                alphabet[i] = (char)(i + 65);
            }

            return alphabet.Select(letter =>
            {
                var unblocked = polymer.Where(unit => char.ToUpper(unit) != letter)
                    .ToArray();

                return FullyReact(unblocked).Length;
            }).Min();
        }
    }
}