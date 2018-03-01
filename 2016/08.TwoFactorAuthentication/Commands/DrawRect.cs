namespace TwoFactorAuthentication.Commands
{
    using System;

    public class DrawRect : ICommand
    {
        public DrawRect(int width, int height)
        {
            this.Width = width;
            this.Height = height;
        }

        public int Width { get; }

        public int Height { get; }

        public void Execute(bool[,] screen)
        {
            if (screen.GetLength(0) < this.Height || screen.GetLength(1) < this.Width)
            {
                throw new ArgumentException("Screen too small.");
            }

            for (int row = 0; row < this.Height; row++)
            {
                for (int col = 0; col < this.Width; col++)
                {
                    screen[row, col] = true;
                }
            }
        }
    }
}