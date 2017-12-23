namespace ParticleSwarm
{
    using System;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.IO;
    using System.Collections.Generic;

    public static class Program
    {
        public static void Main()
        {
            var particles = ReadParticles("input.txt");

            Particle closest = FindClosest(particles);

            Console.WriteLine($"Closest: {closest.Id}");

            const int Ticks = 1000;

            Console.WriteLine($"Computing {Ticks} ticks...");
            int particlesLeft = Execute(particles, Ticks);

            Console.WriteLine($"Particles left: {particlesLeft}");
        }

        public static int Execute(List<Particle> particles, int ticks)
        {
            for (int t = 0; t < ticks; t++)
            {
                foreach (var particle in particles)
                {
                    particle.Tick();
                }

                var collisions = particles.Where(particle => particles.Any(p => p != particle && p.Position.ValueEquals(particle.Position)))
                    .ToArray();

                foreach (var particle in collisions)
                {
                    particles.Remove(particle);
                }
            }

            return particles.Count;
        }

        public static Particle FindClosest(List<Particle> particles)
        {
            Particle closest = particles.OrderBy(particle => CalcDistance(particle.Acceleration))
                .ThenBy(particle => CalcDistance(particle.Velocity))
                .ThenBy(particle => CalcDistance(particle.Position))
                .First();

            return closest;

        }

        public static int CalcDistance(int[] vector)
        {
            return vector.Select(value => Math.Abs(value))
                .Sum();
        }

        public static List<Particle> ReadParticles(string file)
        {
            Regex regex = new Regex(@"-?\d+");

            var particles = new List<Particle>();

            using (var reader = new StreamReader(file))
            {
                while (true)
                {
                    string line = reader.ReadLine();
                    if (line == null)
                    {
                        break;
                    }

                    var data = regex.Matches(line)
                        .Cast<Match>()
                        .Select(match => int.Parse(match.Value));

                    Particle particle = new Particle
                    (
                        position: data.Take(3)
                            .ToArray(),
                        velocity: data.Skip(3)
                            .Take(3)
                            .ToArray(),
                        acceleration: data.Skip(6)
                            .Take(3)
                            .ToArray()
                    );

                    particles.Add(particle);
                }
            }

            return particles;
        }
    }
}