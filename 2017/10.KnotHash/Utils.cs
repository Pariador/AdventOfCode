namespace KnotHash
{
    public static class Utils
    {
        public static T[] Reverse<T>(this T[] items, int start = 0, int? length = null)
        {
            int len = length ?? items.Length;

            length = LoopIndex(len, items.Length + 1, 0);

            int end = LoopIndex(start, items.Length, len - 1);
            int steps = len / 2;

            for (int s = 0; s < steps; s++)
            {
                int right = LoopIndex(start, items.Length, s);
                int left = LoopIndex(end, items.Length, -s);

                T temp = items[right];

                items[right] = items[left];
                items[left] = temp;
            }

            return items;
        }

        public static int LoopIndex(int index, int length, int diff = 1)
        {
            return (((index + diff) % length) + length) % length;
        }
    }
}