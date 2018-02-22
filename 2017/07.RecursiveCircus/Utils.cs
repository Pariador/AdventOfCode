namespace RecursiveCircus
{
    using System.Collections.Generic;

    public static class Utils
    {
        public static int IndexOf<T>(this T[] items, T item)
        {
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i].Equals(item))
                {
                    return i;
                }
            }

            return -1;
        }

        public static Dictionary<T, int> CountEquals<T>(this IEnumerable<T> items)
        {
            var counts = new Dictionary<T, int>();

            foreach (var item in items)
            {
                if (counts.ContainsKey(item))
                {
                    counts[item]++;
                }
                else
                {
                    counts[item] = 1;
                }

            }

            return counts;
        }
    }
}