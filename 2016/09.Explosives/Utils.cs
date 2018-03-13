namespace Explosives
{
    using System;

    public static class Utils
    {
        public static bool IsValidIndex(int index, int length)
        {
            return 0 <= index && index < length;
        }
        
        public static void ValidateIndex(int index, int length)
        {
            if (IsValidIndex(index, length))
            {
                return;
            }

            throw new IndexOutOfRangeException($"Invalid index! Index: {index}, Length: {length}.");
        }
    }
}