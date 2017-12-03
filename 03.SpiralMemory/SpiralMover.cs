namespace SpiralMemory
{
    using System;

    public class SpiralMover
    {
        private int[,] grid;

        private Position start;

        private Position current;

        public SpiralMover(int[,] grid)
        {
            this.grid = grid;

            this.SetStart();

            this.current = this.start;
        }

        public Position Start
        {
            get
            {
                return this.start.Clone();
            }    
        }

        public Position Current
        {
            get
            {
                return this.current.Clone();
            }

            private set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Current positon can't be null!s");
                }

                this.current = current;
            }
        }

        private Direction Direction { get; set; }

        public Position Next()
        {
            this.current = this.current.Move(this.Direction);

            this.AdjustDirection();

            return this.Current;
        }

        private void AdjustDirection()
        {
            bool upEmpty = !IsFull(this.current.Move(Direction.Up));
            bool downEmpty = !IsFull(this.current.Move(Direction.Down));
            bool leftEmpty = !IsFull(this.current.Move(Direction.Left));
            bool rightEmpty = !IsFull(this.current.Move(Direction.Right));

            if (upEmpty && downEmpty && rightEmpty)
            {
                this.Direction = Direction.Up;
            }
            else if (upEmpty && downEmpty && leftEmpty)
            {
                this.Direction = Direction.Down;
            }
            else if (rightEmpty && leftEmpty && upEmpty)
            {
                this.Direction = Direction.Left;
            }
            else if (rightEmpty && leftEmpty && downEmpty)
            {
                this.Direction = Direction.Right;
            }
        }

        private bool IsFull(Position positon)
        {
            return IsValid(positon) && this.grid[positon.Row, positon.Col] != 0;
        }

        private bool IsValid(Position position)
        {
            return Utils.IsValidPositon(position.Row, position.Col, this.grid);
        }

        private void SetStart()
        {
            int middle = this.grid.GetLength(0) / 2;

            this.start = new Position(middle,  middle);
        }
    }
}