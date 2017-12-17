namespace Spinlock
{
    using System;
    using System.Collections.Generic;

    public static class Utils
    {
        public static LinkedListNode<T> NodeAt<T>(this LinkedList<T> list, int index)
        {
            Utils.ValidateIndex(index, list.Count);

            var current = list.First;

            for (int i = 0; i < index; i++)
            {
                current = current.Next;
            }

            return current;
        }

        public static void ValidateIndex(int index, int length)
        {
            if (index < 0 || length <= index)
            {
                throw new IndexOutOfRangeException();
            }

            return;
        }

        public static int ToPercentage(int value, int total)
        {
            double percent = (double)total / 100;

            return (int)(value / percent);
        }
    }
}