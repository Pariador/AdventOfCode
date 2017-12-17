namespace Spinlock
{
    using System;
    using System.Collections.Generic;

    public static class Program
    {
        public static void Main()
        {
            Spinlock spinlock = new Spinlock(363);

            Run(spinlock, count: 2018, target: 2017);
            Run(spinlock, count: 50000001, target: 0);
        }

        public static void Run(Spinlock spinlock, int count, int target)
        {
            spinlock.Clear();

            Console.WriteLine($"Filling buffer with {count} values...");
            spinlock.Fill(count, percentage => Console.WriteLine($"{percentage}%"));
            int result = spinlock.GetValueAfter(target);
            Console.WriteLine($"Number after {target}: {result}\n--------------------------------\n");
        }
    }
}