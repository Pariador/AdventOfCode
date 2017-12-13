namespace HexEd
{
    using System;

    public class Position
    {
        public Position(int x = 0, int y = 0, int z = 0)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public int X { get; set; }

        public int Y { get; set; }

        public int Z { get; set; }

        public void Change(Direction direction)
        {
            switch (direction)
            {
                case Direction.North:
                    this.Y++;
                    this.Z--;
                    break;
                case Direction.South:
                    this.Y--;
                    this.Z++;
                    break;
                case Direction.NorthEast:
                    this.X++;
                    this.Z--;
                    break;
                case Direction.SouthWest:
                    this.X--;
                    this.Z++;
                    break;
                case Direction.NorthWest:
                    this.Y++;
                    this.X--;
                    break;
                case Direction.SouthEast:
                    this.Y--;
                    this.X++;
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        public override string ToString()
        {
            return $"X: {this.X}, Y: {this.Y}, Z: {this.Z}";
        }
    }
}