namespace TwoFactorAuthentication
{
    using System;

    public static class Utils
    {
        public static T[,] CopyToRow<T>(this T[,] matrix, T[] arr, int row)
        {
            if (matrix.GetLength(1) != arr.Length)
            {
                throw new ArgumentException("Array length must equal columns count!");
            }

            ValidateIndex(row, matrix.GetLength(0));

            for (int col = 0; col < matrix.GetLength(1); col++)
            {
                matrix[row, col] = arr[col];
            }

            return matrix;
        }

        public static T[,] CopyToCol<T>(this T[,] matrix, T[] arr, int col)
        {
            if (matrix.GetLength(0) != arr.Length)
            {
                throw new ArgumentException("Array length must equal rows count!");
            }

            ValidateIndex(col, matrix.GetLength(1));

            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                matrix[row, col] = arr[row];
            }

            return matrix;
        }

        public static void ValidateIndex(int index, int length)
        {
            if (index < 0 || length <= index)
            {
                throw new ArgumentException("Invalid index!");
            }
        }
    }
}