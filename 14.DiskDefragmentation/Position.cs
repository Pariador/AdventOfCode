namespace DiskDefragmentation
{
    public class Position
    {
        public Position(int row = 0, int col = 0)
        {
            this.Row = row;
            this.Col = col;
        }

        public int Row { get; set; }

        public int Col { get; set; }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as Position);
        }

        public override int GetHashCode()
        {
            return this.Row ^ this.Col;
        }

        private bool Equals(Position other)
        {
            return other != null &&
                this.Row == other.Row &&
                this.Col == other.Col;
        }
    }
}