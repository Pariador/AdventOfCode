namespace PacketScanners
{
    public class Scanner
    {
        private int savePosition;

        private bool saveMovingForward;

        public Scanner(int range = 1)
        {
            this.Range = range;

            this.Position = 0;
            this.MovingForward = true;

            this.Save();
        }

        public int Position { get; private set; }

        public int Range { get; private set; }

        public bool MovingForward { get; private set; }

        public void Move()
        {
            if (this.Position == 0)
            {
                this.MovingForward = true;
            }
            else if (this.Position == this.Range - 1)
            {
                this.MovingForward = false;
            }

            if (this.MovingForward)
            {
                this.Position++;
            }
            else
            {
                this.Position--;
            }
        }

        public void Save()
        {
            this.savePosition = this.Position;
            this.saveMovingForward = this.MovingForward;
        }

        public void Restore()
        {
            this.Position = this.savePosition;
            this.MovingForward = this.saveMovingForward;
        }
    }
}