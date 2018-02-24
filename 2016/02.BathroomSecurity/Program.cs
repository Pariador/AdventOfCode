namespace BathroomSecurity
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Collections.Generic;

    public static class Program
    {
        public static readonly char[,] BasicKeypad =
        {
            { '1', '2', '3' },
            { '4', '5', '6' },
            { '7', '8', '9' }
        };

        public static readonly char[,] AdvancedKeypad =
        {
            { ' ', ' ', '1', ' ', ' ' },
            { ' ', '2', '3', '4', ' ' },
            { '5', '6', '7', '8', '9' },
            { ' ', 'A', 'B', 'C', ' ' },
            { ' ', ' ', 'D', ' ', ' ' }
        };

        public static void Main()
        {
            var directionSets = ReadDirections("input.txt");

            Decoder decoder = new Decoder(BasicKeypad, new int[] { 1, 1 });
            string password = decoder.Decode(directionSets);

            Console.WriteLine($"Basic keypad password: {password}");

            decoder = new Decoder(AdvancedKeypad, new int[] { 2, 0 });
            password = decoder.Decode(directionSets);

            Console.WriteLine($"Advanced keypad password: {password}");
        }

        public static Direction[][] ReadDirections(string fileName)
        {
            var directions = new List<Direction[]>();

            using (var reader = new StreamReader(fileName))
            {
                while (true)
                {
                    string line = reader.ReadLine();
                    if (line == null)
                    {
                        break;
                    }

                    directions.Add(line.Select(ParseDirection).ToArray());
                }
            }

            return directions.ToArray();
        }

        public static Direction ParseDirection(char @char)
        {
            switch (@char)
            {
                case 'U':
                    return Direction.Up;
                case 'D':
                    return Direction.Down;
                case 'L':
                    return Direction.Left;
                case 'R':
                    return Direction.Right;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}