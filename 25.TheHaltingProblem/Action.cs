namespace TheHaltingProblem
{
    public class Action
    {
        public Action(bool write, Direction direction, char next)
        {
            this.Write = write;
            this.Direction = direction;
            this.Next = next;
        }

        public bool Write { get; }

        public Direction Direction { get; }

        public char Next { get; }
    }
}