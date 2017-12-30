namespace FractralArt
{
    public static class Transformations
    {
        public static char[,] FlipHorizontal(char[,] image)
        {
            int middle = image.GetLength(0) / 2;

            for (int row = 0; row < middle; row++)
            {
                for (int col = 0; col < image.GetLength(1); col++)
                {
                    int otherRow = image.GetLength(0) - 1 - row;

                    char temp = image[row, col];
                    image[row, col] = image[otherRow, col];
                    image[otherRow, col] = temp;
                }
            }

            return image;
        }

        public static char[,] FlipVertical(char[,] image)
        {
            int middle = image.GetLength(1) / 2;

            for (int row = 0; row < image.GetLength(0); row++)
            {
                for (int col = 0; col < middle; col++)
                {
                    int otherCol = image.GetLength(1) - 1 - col;

                    char temp = image[row, col];
                    image[row, col] = image[row, otherCol];
                    image[row, otherCol] = temp;
                }
            }

            return image;
        }

        public static char[,] Rotate(char[,] image)
        {
            return image;
        }
    }
}