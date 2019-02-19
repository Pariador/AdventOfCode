namespace ChronalCoordinates
{
    class Border
    {
        public static Border FindBorder(Point[] points)
        {
            Point topLeft = points[0];
            Point botRight = points[0];

            for (int i = 1; i < points.Length; i++)
            {
                if (points[i].X < topLeft.X)
                {
                    topLeft = new Point(points[i].X, topLeft.Y);
                }

                if (points[i].Y < topLeft.Y)
                {
                    topLeft = new Point(topLeft.X, points[i].Y);
                }

                if (points[i].X > botRight.X)
                {
                    botRight = new Point(points[i].X, botRight.Y);
                }

                if (points[i].Y > botRight.Y)
                {
                    botRight = new Point(botRight.X, points[i].Y);
                }
            }

            return new Border(topLeft, botRight);
        }

        public Border(Point topLeft, Point botRight)
        {
            this.TopLeft = topLeft;
            this.BotRight = botRight;
        }

        public Point TopLeft { get; }

        public Point BotRight { get; }

        public override string ToString()
        {
            return $"Top left: {this.TopLeft}, Bottom right: {this.BotRight}";
        }
    }
}