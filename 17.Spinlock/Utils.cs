namespace Spinlock
{
    using System;
    using System.Collections.Generic;

    public static class Utils
    {
        public static void ValidateIndex(int index, int length)
        {
            if (index < 0 || length <= index)
            {
                throw new IndexOutOfRangeException();
            }

            return;
        }
    }
}