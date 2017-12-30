namespace SporificaVirus
{
    using System;

    public class Position
    {
        public Position(int x = 0, int y = 0)
        {
            this.X = x;
            this.Y = y;
        }

        public int X { get; }

        public int Y { get; }

        public Position Move(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    return new Position(this.X, this.Y - 1);
                case Direction.Down:
                    return new Position(this.X, this.Y + 1);
                case Direction.Left:
                    return new Position(this.X - 1, this.Y);
                case Direction.Right:
                    return new Position(this.X + 1, this.Y);
                default:
                    throw new ArgumentException("Unknown direction!");
            }
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as Position);
        }

        public override int GetHashCode()
        {
            return this.X ^ this.Y;
        }

        private bool Equals(Position other)
        {
            return other != null &&
                this.X == other.X &&
                this.Y == other.Y;
        }
    }
}