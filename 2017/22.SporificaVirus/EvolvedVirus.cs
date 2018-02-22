namespace SporificaVirus
{
    using System.Collections.Generic;

    public class EvolvedVirus : Virus
    {
        public EvolvedVirus(HashSet<Position> infected = null) 
            : base(infected)
        {
            this.Weakened = new HashSet<Position>();
            this.Flagged = new HashSet<Position>();
        }

        private HashSet<Position> Weakened { get; set; }

        private HashSet<Position> Flagged { get; set; }

        public override void Burst()
        {
            if (this.Weakened.Contains(this.Position))
            {
                this.Weakened.Remove(this.Position);
                this.Infected.Add(this.Position);

                this.InfectionsCaused++;
            }
            else if (this.Infected.Contains(this.Position))
            {
                this.Direction = this.Direction.Turn(Turn.Clockwise);

                this.Infected.Remove(this.Position);
                this.Flagged.Add(this.Position);
            }
            else if (this.Flagged.Contains(this.Position))
            {
                this.Direction = this.Direction.Turn(Turn.Clockwise)
                    .Turn(Turn.Clockwise);

                this.Flagged.Remove(this.Position);
            }
            else
            {
                this.Direction = this.Direction.Turn(Turn.CounterClockwise);

                this.Weakened.Add(this.Position);
            }

            this.Position = this.Position.Move(this.Direction);
        }
    }
}