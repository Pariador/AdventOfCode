namespace PermutationPromenade
{
    using System;

    public static class Utils
    {
        public static int IndexOf<T>(this T[] items, T query)
        {
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i].Equals(query))
                {
                    return i;
                }
            }

            return -1;
        }

        public static void ValidateIndex(int index, int length)
        {
            if (index < 0 || length <= index)
            {
                throw new IndexOutOfRangeException();
            }
        }
    }
}