namespace Taxicab
{
    using System.Collections.Generic;

    public class ArrayValueComparer<T> : IEqualityComparer<T[]>
    {
        public bool Equals(T[] x, T[] y)
        {
            if (x.Length != y.Length)
            {
                return false;
            }

            for (int i = 0; i < x.Length; i++)
            {
                if (!x[i].Equals(y[i]))
                {
                    return false;
                }
            }

            return true;
        }

        public int GetHashCode(T[] obj)
        {
            int hash = 7;

            for (int i = 0; i < obj.Length; i++)
            {
                hash ^= obj[i].GetHashCode();
            }

            return hash;
        }
    }
}
