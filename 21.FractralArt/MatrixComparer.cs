namespace FractralArt
{
    using System;
    using System.Collections.Generic;

    public class MatrixComparer<T> : IEqualityComparer<T[,]>
    {
        public bool Equals(T[,] x, T[,] y)
        {
            if (x.GetLength(0) != y.GetLength(0) || x.GetLength(1) != y.GetLength(1))
            {
                return false;
            }

            for (int row = 0; row < x.GetLength(0); row++)
            {
                for (int col = 0; col < x.GetLength(1); col++)
                {
                    if (!x[row, col].Equals(y[row, col]))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public int GetHashCode(T[,] obj)
        {
            int hash = 0;

            for (int row = 0; row < obj.GetLength(0); row++)
            {
                for (int col = 0; col < obj.GetLength(1); col++)
                {
                    hash ^= obj[row, col].GetHashCode();
                }
            }

            return hash;
        }
    }
}