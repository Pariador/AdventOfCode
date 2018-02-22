namespace SpiralMemory
{
    using System;

    public class Position
    {
        public Position(int row = 0, int col = 0)
        {
            this.Row = row;
            this.Col = col;
        }

        public int Row { get; set; }

        public int Col { get; set; }

        public Position Clone()
        {
            return (Position)this.MemberwiseClone();
        }

        public Position Move(Direction direction)
        {
            Position @new = this.Clone();

            switch (direction)
            {
                case Direction.Up:
                    @new.Row--;
                    break;
                case Direction.Down:
                    @new.Row++;
                    break;
                case Direction.Left:
                    @new.Col--;
                    break;
                case Direction.Right:
                    @new.Col++;
                    break;
                default:
                    throw new NotImplementedException(direction.ToString());
            }

            return @new;
        }
    }
}