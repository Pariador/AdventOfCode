namespace TheHaltingProblem
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text.RegularExpressions;

    public static class Program
    {
        public static void Main()
        {
            string input = GetInput("input.txt");

            char start = ReadStart(input, out int stepsCount);
            var states = ReadStates(input);

            TuringMachine turingMachine = new TuringMachine(states, start);

            Console.WriteLine("Computing...");

            turingMachine.Excute(stepsCount);

            Console.WriteLine($"Checksum: {turingMachine.Checksum()}");
        }

        public static Dictionary<char, State> ReadStates(string input)
        {
            var states = new Dictionary<char, State>();

            var matches = Regex.Matches(input, @"In state ([A-z]):(?:\s*\n\s+If[\s\S]+?\d[\s\S]+?(\d)[\s\S]+?(right|left)[\s\S]+?state ([A-Z])\.){2}");

            foreach (Match match in matches)
            {
                char stateId = match.Groups[1].Value[0];

                Action[] actions = new Action[2];

                for (int i = 0; i < actions.Length; i++)
                {
                    actions[i] = new Action
                    (
                        write: match.Groups[2].Captures[i].Value == "1",
                        direction: match.Groups[3].Captures[i].Value == "left" ? Direction.Left : Direction.Right,
                        next: match.Groups[4].Captures[i].Value[0]
                    );
                }

                State state = new State(stateId, actions);

                states[state.Id] = state;
            }

            return states;
        }

        public static char ReadStart(string input, out int stepsCount)
        {
            Match match = Regex.Match(input, @"Begin in state ([A-Z])\.\s*\nPerform a diagnostic checksum after (\d+) steps\.");

            stepsCount = int.Parse(match.Groups[2].Value);
            return match.Groups[1].Value[0];
        }

        public static string GetInput(string file)
        {
            using (var reader = new StreamReader(file))
            {
                return reader.ReadToEnd();
            }
        }
    }
}