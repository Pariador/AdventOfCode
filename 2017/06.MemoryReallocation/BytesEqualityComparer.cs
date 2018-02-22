namespace MemoryReallocation
{
    using System.Collections.Generic;

    public class BytesEqualityComparer : IEqualityComparer<byte[]>
    {
        public bool Equals(byte[] x, byte[] y)
        {
            if (x.Length != y.Length)
            {
                return false;
            }

            for (int i = 0; i < x.Length; i++)
            {
                if (x[i] != y[i])
                {
                    return false;
                }
            }

            return true;
        }

        public int GetHashCode(byte[] obj)
        {
            int hash = 0;
            for (int i = 0, shiftBytes = 0; i < obj.Length; i++, shiftBytes = (shiftBytes + 1) % 4)
            {
                int number = ((int)obj[i]) << (shiftBytes * 8);

                hash = (hash | number) * 1000003;
            }

            return hash;
        }
    }
}