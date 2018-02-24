namespace BathroomSecurity
{
    using System;
    using System.Text;

    public class Decoder
    {
        private const int KeyPadSize = 3;

        private const char EmptySpace = ' ';

        private int[] position;

        private char[,] keypad;

        public Decoder(char[,] keypad, int[] startingPosition)
        {
            this.KeyPad = keypad;

            this.Position = startingPosition;
        }

        private char[,] KeyPad
        {
            get
            {
                return this.keypad;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException();
                }

                this.keypad = value;
            }
        }

        private int[] Position
        {
            get
            {
                return this.position;
            }

            set
            {
                if (value == null || value.Length != 2)
                {
                    throw new ArgumentException("Positon must contain 2 numbers and can't be null!");
                }

                this.position = value;
            }
        }

        public string Decode(Direction[][] directionSets)
        {
            StringBuilder builder = new StringBuilder();

            foreach (var set in directionSets)
            {
                foreach (var direction in set)
                {
                    this.Move(direction);
                }

                builder.Append(this.keypad[this.position[0], this.position[1]]);
            }

            return builder.ToString();
        }

        private void Move(Direction direction)
        {
            int code = (int)direction;

            int dimension = Math.Abs(code) - 1;
            int mod = code / Math.Abs(code);

            this.position[dimension] += (1 * mod);

            if (!IsValidPosition())
            {
                this.position[dimension] -= (1 * mod);
            }
        }

        private bool IsValidPosition()
        {
            for (int d = 0; d < this.position.Length; d++)
            {
                if (this.position[d] < 0 || this.keypad.GetLength(d) <= this.position[d])
                {
                    return false;
                }
            }

            if (this.keypad[this.position[0], this.position[1]] == Decoder.EmptySpace)
            {
                return false;
            }

            return true;
        }
    }
}