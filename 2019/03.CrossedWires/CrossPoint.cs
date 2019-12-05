namespace CrossedWires
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using static Geometry;

    class CrossPoint
    {
        public static CrossPoint[] Find(Wire first, Wire second)
        {
            var crossPoints = Find(first.Horizontals, second.Verticals);
            crossPoints = crossPoints.Concat(Find(second.Horizontals, first.Verticals))
                .ToArray();

            return crossPoints;
        }

        public static CrossPoint[] Find(Line[] horizontals, Line[] verticals)
        {
            var crossPoints = new List<CrossPoint>();

            foreach (var horizontal in horizontals)
            {
                foreach (var vertical in verticals)
                {
                    var crossPoint = New(horizontal, vertical);
                    if (crossPoint != null)
                        crossPoints.Add(crossPoint);
                }
            }

            return crossPoints.ToArray();
        }

        public static bool Cross(Line horizontal, Line vertical)
        {
            int x = vertical.From[0];

            int left = Math.Min(horizontal.From[0], horizontal.To[0]);
            int right = Math.Max(horizontal.From[0], horizontal.To[0]);
            if (x < left || right < x)
                return false;

            int y = horizontal.From[1];
            int top = Math.Min(vertical.From[1], vertical.To[1]);
            int bot = Math.Max(vertical.From[1], vertical.To[1]);
            if (y < top || bot < y)
                return false;

            return true;
        }

        public static CrossPoint New(Line horizontal, Line vertical)
        {
            if (Cross(horizontal, vertical))
                return new CrossPoint(horizontal, vertical);
            else
                return null;
        }

        public CrossPoint(Line horizontal, Line vertical)
        {
            if (!Cross(horizontal, vertical))
                throw new ArgumentException("Horizontal & vertical lines don't cross.");

            this.Horizontal = horizontal;
            this.Vertical = vertical;

            this.Point = new int[] { vertical.From[0], horizontal.From[1] };
        }

        public Line Horizontal { get; }

        public Line Vertical { get; }

        public int[] Point { get; }

        public int TotalOriginLength
        {
            get
            {
                int length = this.Horizontal.OriginLength- Distance(this.Horizontal.To, this.Point);
                length += this.Vertical.OriginLength - Distance(this.Vertical.To, this.Point);

                return length;
            }
        }
    }
}