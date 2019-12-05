namespace CrossedWires
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using static Geometry;

    class Wire
    {
        public static Wire Parse(string[] wire)
        {
            var lines = new List<Line>();

            int originLength = 0;
            int[] position = Origin.ToArray();
            foreach (var line in wire)
            {
                int[] direction = ParseDirection(line[0]);
                int distance = int.Parse(line.Substring(1));

                int[] from = position.ToArray();

                Move(position, direction, distance);

                int[] to = position.ToArray();

                originLength += distance;
                lines.Add(new Line(from, to, originLength));
            }

            return new Wire(lines.ToArray());
        }

        private static int[] ParseDirection(char c)
        {
            switch (c)
            {
                case 'U': return new int[] { 1, -1 };
                case 'D': return new int[] { 1, +1 };
                case 'L': return new int[] { 0, -1 };
                case 'R': return new int[] { 0, +1 };
                default: throw new ArgumentException("Unknown direction!");
            }
        }

        public Wire(Line[] lines)
        {
            this.Lines = lines;

            this.SortLines();
        }

        public Line[] Lines { get; }

        public Line[] Horizontals { get; private set; }

        public Line[] Verticals { get; private set; }

        private void SortLines()
        {
            var horizontals = new List<Line>();
            var verticals = new List<Line>();

            foreach (var line in this.Lines)
            {
                if (line.IsHorizontal)
                    horizontals.Add(line);
                else
                    verticals.Add(line);
            }

            this.Horizontals = horizontals.ToArray();
            this.Verticals = verticals.ToArray();
        }
    }
}