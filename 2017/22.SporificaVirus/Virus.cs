namespace SporificaVirus
{
    using System;
    using System.Collections.Generic;

    public class Virus
    {
        public Virus(HashSet<Position> infected = null)
        {
            this.Infected = infected ?? new HashSet<Position>();

            this.InfectionsCaused = 0;
            this.Direction = Direction.Up;
            this.Position = new Position();
        }

        public int InfectionsCaused { get; protected set; }

        protected HashSet<Position> Infected { get; }

        protected Direction Direction { get; set; }

        protected Position Position { get; set; }

        public virtual void Burst()
        {
            if (this.Infected.Contains(this.Position))
            {
                this.Direction = this.Direction.Turn(Turn.Clockwise);
                this.Infected.Remove(this.Position);
            }
            else
            {
                this.Direction = this.Direction.Turn(Turn.CounterClockwise);
                this.Infected.Add(this.Position);

                this.InfectionsCaused++;
            }

            this.Position = this.Position.Move(this.Direction);
        }

        public void Burst(int count, Action<int> report = null)
        {
            int percent = count / 100;

            for (int i = 0; i < count; i++)
            {
                if (report != null && i % percent == 0)
                {
                    report(i / percent);
                }

                this.Burst();
            }
        }
    }
}