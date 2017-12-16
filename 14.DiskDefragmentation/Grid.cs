namespace DiskDefragmentation
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class Grid : IEnumerable<bool>
    {
        private static int GetColIndices(int col, out int bitIndex)
        {
            bitIndex = col % Utils.BitsInByte;

            return col / Utils.BitsInByte;
        }

        private int size;

        private byte[,] bytes;

        public Grid(int size = 128)
        {
            this.Size = size;

            this.InitBytes();
        }

        public Grid(byte[,] bytes)
        {
            this.InitBytes(bytes);
        }

        public int Size
        { 
            get
            {
                return this.size;
            }

            set
            {
                if (value % Utils.BitsInByte != 0)
                {
                    throw new ArgumentException($"Size must be a multiple of {Utils.BitsInByte}!");
                }

                this.size = value;
            }
        }

        public bool this[int row, int col]
        {
            get
            {
                this.ValidateIndex(row);
                this.ValidateIndex(col);

                int byteIndex = Grid.GetColIndices(col, out int bitIndex);

                return Utils.GetBit(this.bytes[row, byteIndex], bitIndex);;
            }

            set
            {
                this.ValidateIndex(row);
                this.ValidateIndex(col);

                int byteIndex = Grid.GetColIndices(col, out int bitIndex);

                this.bytes[row, byteIndex] = Utils.SetBit(this.bytes[row, byteIndex], bitIndex, value);
            }
        }

        public IEnumerator<bool> GetEnumerator()
        {
            for (int row = 0; row < this.Size; row++)
            {
                for (int col = 0; col < this.Size; col++)
                {
                    yield return this[row, col];
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private void InitBytes(byte[,] bytes = null)
        {
            if (bytes == null)
            {
                this.bytes = new byte[this.Size, this.Size / Utils.BitsInByte];
                return;
            }

            this.Size = bytes.GetLength(0);

            if (bytes.GetLength(1) * Utils.BitsInByte != this.Size)
            {
                throw new ArgumentException("Total bits in bytes row must equal number of bytes rows!");
            }

            this.bytes = bytes;
        }

        public bool IsValid(Position position)
        {
            return this.IsValidIndex(position.Row) &&
                this.IsValidIndex(position.Col);
        }

        private bool IsValidIndex(int i)
        {
            return 0 <= i && i < this.Size;
        }

        private void ValidateIndex(int i)
        {
            if (this.IsValidIndex(i))
            {
                return;
            }

            throw new IndexOutOfRangeException($"Index must be 0 - {this.Size - 1}!");
        }
    }
}