namespace HexEd
{
    using System;
    using System.IO;
    using System.Linq;

    public static class Program
    {
        public static void Main()
        {
            string input = GetInput();

            var directions = Utils.ParseDirections(input);
            var position = ExecuteDirections(directions, out int maxDistance);
            int distance = CalcStepsDistance(position);

            Console.WriteLine($"Distance: {distance}");
            Console.WriteLine($"Max distance: {maxDistance}");
        }

        public static int CalcStepsDistance(Position position)
        {
            const int ChangesPerStep = 2;

            return (Math.Abs(position.X) + Math.Abs(position.Y) + Math.Abs(position.Z)) / ChangesPerStep;
        }

        public static Position ExecuteDirections(Direction[] directions, out int maxDistance)
        {
            Position position = new Position();
            maxDistance = 0;

            foreach (var direction in directions)
            {
                position.Change(direction);

                int distance = CalcStepsDistance(position);

                if (distance > maxDistance)
                {
                    maxDistance = distance;
                }
            }

            return position;
        }

        public static string GetInput()
        {
            using (var reader = new StreamReader("input.txt"))
            {
                return reader.ReadToEnd();
            }
        }
    }
}