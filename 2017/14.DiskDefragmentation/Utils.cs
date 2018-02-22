namespace DiskDefragmentation
{
    using System;

    public static class Utils
    {
        public const int BitsInByte = 8;

        public static bool GetBit(byte @byte, int i)
        {
            if (i < 0 || Utils.BitsInByte <= i)
            {
                throw new IndexOutOfRangeException("Index must be in range 0 - 7!");
            }

            int mask = 0x80 >> i;

            int result = @byte & mask;

            return result == mask;
        }       

        public static byte SetBit(byte @byte, int i, bool value)
        {
            if (i < 0 || Utils.BitsInByte <= i)
            {
                throw new IndexOutOfRangeException("Index must be in range 0 - 7!");
            }

            int mask = 0x80 >> i;

            if (value)
            {
                return (byte)(@byte | mask);
            }
            else  
            {
                return (byte)(@byte & ~mask);
            }
        }
    }
}