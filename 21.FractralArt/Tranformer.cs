namespace FractralArt
{
    using System;
    using System.Collections.Generic;

    public class Transformer
    {
        public static char[,] FlipHorizontal(char[,] image)
        {
            char[,] result = new char[image.GetLength(0), image.GetLength(1)];

            for (int row = 0; row < result.GetLength(0); row++)
            {
                for (int col = 0; col < result.GetLength(1); col++)
                {
                    result[row, col] = image[image.GetLength(0) - 1 - row, col];
                }
            }

            return result;
        }

        public static char[,] FlipVertical(char[,] image)
        {
            char[,] result = new char[image.GetLength(0), image.GetLength(1)];

            for (int row = 0; row < result.GetLength(0); row++)
            {
                for (int col = 0; col < result.GetLength(1); col++)
                {
                    result[row, col] = image[row, image.GetLength(1) - 1 - col];
                }
            }

            return result;
        }

        public static char[,] Rotate(char[,] image)
        {
            char[,] result = new char[image.GetLength(0), image.GetLength(1)];

            for (int row = 0; row < result.GetLength(0); row++)
            {
                for (int col = 0; col < result.GetLength(1); col++)
                {
                    result[row, col] = image[image.GetLength(0) - 1 - col, row];
                }
            }

            return result;
        }

        public static void WriteSquare(char[,] image, int fromRow, int fromCol, char[,] squere)
        {
            for (int row = 0; row < squere.GetLength(0); row++)
            {
                for (int col = 0; col < squere.GetLength(1); col++)
                {
                    image[fromRow + row, fromCol + col] = squere[row, col];
                }
            }
        }

        public static char[,] ReadSquare(char[,] image, int fromRow, int fromCol, int size)
        {
            char[,] result = new char[size, size];

            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    result[row, col] = image[fromRow + row, fromCol + col];
                }
            }

            return result;
        }

        private char[,] image;

        private Dictionary<char[,], char[,]> rules;

        public Transformer(char[,] image, Dictionary<char[,], char[,]> rules = null)
        {
            this.Image = image;
            this.Rules = rules ?? new Dictionary<char[,], char[,]>(new MatrixComparer<char>());
        }

        public char[,] Image 
        {
            get
            {
                return (char[,])this.image.Clone();
            }

            set
            {
                this.image = value ?? throw new ArgumentException("Image can't be null!");
            }
        }

        public Dictionary<char[,], char[,]> Rules
        {
            get
            {
                return this.rules;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentException("Rules can't be null!");
                }

                if (!(value.Comparer is MatrixComparer<char>))
                {
                    throw new ArgumentException("Rules must use MatrixComparer!");
                }

                this.rules = value;
            }
        }

        public void Enhance()
        {
            int squareSize = this.image.GetLength(0) % 2 == 0 ? 2 : 3;
            int squaresCount = this.image.GetLength(0) / squareSize;

            int newSquareSize = squareSize + 1;

            char[,] result = new char[this.image.GetLength(0) + squaresCount, this.image.GetLength(1) + squaresCount];

            for (int row = 0; row < squaresCount; row++)
            {
                for (int col = 0; col < squaresCount; col++)
                {
                    char[,] square = Transformer.ReadSquare(this.image, row * squareSize, col * squareSize, squareSize);

                    Transformer.WriteSquare(result, row * newSquareSize, col * newSquareSize, this.Rules[square]);
                }
            }

            this.image = result;
        }

        public void Enhance(int times)
        {
            for (int i = 0; i < times; i++)
            {
                this.Enhance();
            }
        }
    }
}