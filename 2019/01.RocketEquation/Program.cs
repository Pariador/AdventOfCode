namespace RocketEquation
{
    using System;
    using System.IO;
    using System.Linq;

    static class Program
    {
        static void Main()
        {
            int[] modules = File.ReadAllLines("input.txt")
                .Select(int.Parse)
                .ToArray();

            int fuel = modules.Select(CalcFuel)
                .Sum();

            int absoluteFuel = modules.Select(CalcAbsoluteFuel)
                .Sum();

            Console.WriteLine($"Fuel required: {fuel}");
            Console.WriteLine($"Absolute fuel required: {absoluteFuel}");
        }

        static int CalcFuel(int mass)
        {
            if (mass <= 6)
                return 0;

            return mass / 3 - 2;
        }

        static int CalcAbsoluteFuel(int mass)
        {
            int total = 0;

            while (mass != 0)
            {
                int fuel = CalcFuel(mass);
                total += fuel;

                mass = fuel;
            }

            return total;
        }
    }
}