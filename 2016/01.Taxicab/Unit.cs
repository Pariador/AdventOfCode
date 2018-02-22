namespace Taxicab
{
    using System.Collections.Generic;
    using System.Linq;

    public class Unit
    {
        private int[] position;

        private HashSet<int[]> visited;

        public Unit()
        {
            this.position = new int[2];
            this.visited = new HashSet<int[]>(new ArrayValueComparer<int>());

            this.Visit();
        }

        public int[] Position
        {
            get
            {
                return this.position.ToArray();
            }
        }

        public Direction Direction { get; private set; }

        public int[] Revisited { get; private set; }

        public void Move(Instruction instruction)
        {
            this.Turn(instruction.Side);

            int dimension = (int)this.Direction % (int)Direction.South != 0 ? 0 : 1;
            int mod = (int)this.Direction % (int)Direction.West == 0 ? -1 : 1;

            for (int i = 0; i < instruction.Distance; i++)
            {
                this.position[dimension] += 1 * mod;

                this.Visit();
            }
        }

        private void Visit()
        {
            if (this.Revisited != null)
            {
                return;
            }

            if (this.visited.Contains(this.Position))
            {
                this.Revisited = this.Position;
            }
            else
            {
                this.visited.Add(this.Position);
            }
        }

        private void Turn(Side side)
        {
            const int Step = 64;

            this.Direction = side == Side.Right ? this.Direction + Step : this.Direction - Step;
        }
    }
}