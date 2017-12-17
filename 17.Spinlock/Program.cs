namespace Spinlock
{
    using System;
    using System.Collections.Generic;

    public static class Program
    {
        public static void Main()
        {
            Spinlock spinlock = new Spinlock(363);

            int count = 2018;

            spinlock.Fill(count);
            int first = spinlock.GetValueAfter(2017);
            Console.WriteLine($"First: {first}");

            spinlock.Clear();

            count = 50000001;

            spinlock.Fill(count, number => Console.WriteLine(Utils.ToPercentage(number, count)));
            int second = spinlock.GetValueAfter(0);
            Console.WriteLine($"Second: {second}");
        }
    }
}