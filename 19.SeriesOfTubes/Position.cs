namespace SeriesOfTubes
{
    using System;

    public class Position
    {
        public Position(int row, int col)
        {
            this.Row = row;
            this.Col = col;
        }

        public int Row { get; set; }

        public int Col { get; set; }

        public Position Next(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    return new Position(this.Row - 1, this.Col);
                case Direction.Down:
                    return new Position(this.Row + 1, this.Col);
                    break;
                case Direction.Left:
                    return new Position(this.Row, this.Col - 1);
                case Direction.Right:
                    return new Position(this.Row, this.Col + 1);
                default:
                    throw new ArgumentException("Unknown Direction!");
            }
        }
    }
}