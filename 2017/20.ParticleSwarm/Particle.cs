namespace ParticleSwarm
{
    public class Particle
    {
        private static int NextId = 0;

        public Particle(int[] position, int[] velocity, int[] acceleration)
        {
            this.Id = Particle.NextId++;
            this.Position = position;
            this.Velocity = velocity;
            this.Acceleration = acceleration;
        }

        public int Id { get; }

        public int[] Position { get; private set; }

        public int[] Velocity { get; private set; }

        public int[] Acceleration { get; private set; }

        public void Tick()
        {
            this.Velocity = this.Velocity.Add(this.Acceleration);
            this.Position = this.Position.Add(this.Velocity);
        }
    }
}