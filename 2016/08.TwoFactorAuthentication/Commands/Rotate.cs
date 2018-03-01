namespace TwoFactorAuthentication.Commands
{
    using System;

    public class Rotate : ICommand
    {
        private int dimension;

        public Rotate(int count, int dimension, int index)
        {
            this.Count = count;
            this.Dimension = dimension;
            this.Index = index;
        }

        public int Count { get; }

        public int Dimension
        {
            get
            {
                return this.dimension;
            }

            set
            {
                if (value != 0 && value != 1)
                {
                    throw new ArgumentException("Only supports 2 dimensions!");
                }

                this.dimension = value;
            }
        }

        public int Index { get; }

        public void Execute(bool[,] screen)
        {
            switch (this.Dimension)
            {
                case 0:
                    this.RotateRow(screen);
                    break;
                case 1:
                    this.RotateCol(screen);
                    break;
                default:
                    throw new ArgumentException("Unsupported dimension!");
            }
        }

        private void RotateRow(bool[,] screen)
        {
            int colsCount = screen.GetLength(1);

            int count = this.Count % colsCount;
            bool[] rotated = new bool[colsCount];

            for (int col = 0; col < colsCount; col++)
            {
                rotated[(col + count) % colsCount] = screen[this.Index, col];
            }

            screen.CopyToRow(rotated, this.Index);
        }

        private void RotateCol(bool[,] screen)
        {
            int rowsCount = screen.GetLength(0);

            int count = this.Count % rowsCount;
            bool[] rotated = new bool[rowsCount];

            for (int row = 0; row < rowsCount; row++)
            {
                rotated[(row + count) % rowsCount] = screen[row, this.Index];
            }

            screen.CopyToCol(rotated, this.Index);
        }
    }
}