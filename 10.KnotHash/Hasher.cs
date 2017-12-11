namespace KnotHash
{
    using System;
    using System.Linq;

    public class Hasher
    {
        private const int HashSize = 16;

        private const int RoundsCount = 64;

        private static readonly byte[] Salt = { 17, 31, 73, 47, 23 };

        private static byte[] InitHash()
        {
            byte[] hash = new byte[256];

            for (int i = 0; i < hash.Length; i++)
            {
                hash[i] = (byte)i;
            }

            return hash;
        }

        private static byte[] Reduce(byte[] data, int size = Hasher.HashSize)
        {
            if (data.Length < size)
            {
                throw new ArgumentException("Data is less smaller than size!");
            }
            else if ((data.Length % size) != 0)
            {
                throw new ArgumentException("Data count must be devisible by size!");
            }

            byte[] reduced = new byte[size];

            int blocksCount = data.Length / size;

            for (int b = 0; b < blocksCount; b++)
            {
                reduced[b] = data[b * size];

                for (int i = 1; i < size; i++)
                {
                    int blockIndex = (b * size) + i;

                    reduced[b] ^= data[blockIndex];
                }
            }

            return reduced;
        }

        public Hasher()
        {
            ;
        }

        private int Skip { get; set; }

        private int Position { get; set; }

        public byte[] Hash(string str)
        {
            byte[] data = str.Select(c => (byte)c)
                .ToArray();

            return this.Hash(data);
        }

        public byte[] Hash(byte[] data)
        {
            this.Reset();

            byte[] hash = InitHash();

            data = data.Concat(Hasher.Salt)
                .ToArray();

            for (int i = 0; i < Hasher.RoundsCount; i++)
            {
                hash = ExecuteRound(data, hash);
            }

            hash = Reduce(hash);

            return hash;
        }

        public byte[] ExecuteRound(byte[] data, byte[] hash = null)
        {
            if (hash == null)
            {
                hash = InitHash();
            }

            foreach (var @byte in data)
            {
                hash.Reverse(this.Position, @byte);

                this.Position = Utils.LoopIndex(this.Position, hash.Length, @byte + this.Skip);
                this.Skip++;
            }

            return hash;
        }

        public void Reset()
        {
            this.Skip = 0;
            this.Position = 0;
        }
    }
}