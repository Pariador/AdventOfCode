namespace SeriesOfTubes
{
    using System;
    using System.Text;

    public class Traverser
    {
        private const char EmptySymbol = ' ';

        private const char HorizontalSymbol = '-';

        private const char VerticalSymbol = '|';

        private const char TurnSymbol = '+';

        private char[,] map;

        public Traverser(char[,] map)
        {
            this.map = map;

            this.Visited = new StringBuilder();
            this.Direction = Direction.Down;
            this.Position = this.FindStart();
        }

        private StringBuilder Visited { get; set; }

        private Direction Direction { get; set; }

        private Position Position { get; set; }

        public string Traverse(out int stepsCount)
        {
            stepsCount = 0;

            while (this.Position != null)
            {
                stepsCount++;
                this.Move();
            }

            return this.Visited.ToString();
        }

        private void Move()
        {
            if (this.map[this.Position.Row, this.Position.Col] == Traverser.TurnSymbol)
            {
                this.Turn();
            }
            else if (char.IsLetter(this.map[this.Position.Row, this.Position.Col]))
            {
                this.Visited.Append(this.map[this.Position.Row, this.Position.Col]);
            }

            this.Position = this.Position.Next(this.Direction);

            if (!this.IsValid(this.Position) || this.map[this.Position.Row, this.Position.Col] == Traverser.EmptySymbol)
            {
                this.Position = null;
            }
        }

        private void Turn()
        {
            if (this.Direction == Direction.Up || this.Direction == Direction.Down)
            {
                Position left = this.Position.Next(Direction.Left);
                Position right = this.Position.Next(Direction.Right);

                if (this.IsValid(left) && this.map[left.Row, left.Col] != Traverser.EmptySymbol)
                {
                    this.Direction = Direction.Left;
                }
                else if (this.IsValid(right) && this.map[right.Row, right.Col] != Traverser.EmptySymbol)
                {
                    this.Direction = Direction.Right;
                }
            }
            else
            {
                Position up = this.Position.Next(Direction.Up);
                Position down = this.Position.Next(Direction.Down);

                if (this.IsValid(up) && this.map[up.Row, up.Col] != Traverser.EmptySymbol)
                {
                    this.Direction = Direction.Up;
                }
                else if (this.IsValid(down) && this.map[down.Row, down.Col] != Traverser.EmptySymbol)
                {
                    this.Direction = Direction.Down;
                }
            }
        }

        private Position FindStart()
        {
            for (int col = 0; col < this.map.GetLength(1); col++)
            {
                if (this.map[0, col] != Traverser.EmptySymbol)
                {
                    return new Position(0, col);
                }
            }

            throw new ArgumentException("Unknown starting position!");
        }

        private bool IsValid(Position position)
        {
            return Utils.IsValidIndex(position.Row, this.map.GetLength(0)) &&
                Utils.IsValidIndex(position.Col, this.map.GetLength(1));
        }
    }
}