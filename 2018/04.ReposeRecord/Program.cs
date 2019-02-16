namespace ReposeRecord
{
    using System;
    using System.Linq;
    using System.IO;
    using System.Collections.Generic;

    static class Program
    {
        static void Main()
        {
            Event[] events = File.ReadAllLines("input.txt")
                .Select(log => Event.Parse(log))
                .OrderBy(@event => @event.DateTime)
                .ToArray();

            Session[] sessions = Session.GetSessions(events);

            var guards = Guard.ToGuards(sessions);

            Guard sleepiestGuard = FindSleepiestGuard(guards);
            int sleepiesMinute = FindSleepiestMinute(sleepiestGuard, out int sleepCount);
            int mostSleptMinute = FindMostSleptMinute(guards, out int guardId);

            Console.WriteLine($"Part 1: {sleepiestGuard.Id * sleepiesMinute}");
            Console.WriteLine($"Part 2: {mostSleptMinute * guardId}");
        }

        static TimeSpan GetSleepingTime(Guard guard)
        {
            TimeSpan sleepingTime = new TimeSpan();

            foreach (var session in guard.Sessions)
            {
                foreach (var span in session.SleepSpans)
                {
                    sleepingTime += span.Stop - span.Start;
                }
            }

            return sleepingTime;
        }

        static Guard FindSleepiestGuard(Guard[] guards)
        {
            Guard guard = guards[0];
            TimeSpan max = GetSleepingTime(guards[0]);

            for (int i = 1; i < guards.Length; i++)
            {
                TimeSpan sleepingTime = GetSleepingTime(guards[i]);

                if (sleepingTime > max)
                {
                    guard = guards[i];
                    max = sleepingTime;
                }
            }

            return guard;
        }

        static int FindSleepiestMinute(Guard guard, out int sleepCount)
        {
            int[] minutes = new int[60];

            foreach (var session in guard.Sessions)
            {
                foreach (var span in session.SleepSpans)
                {
                    for (int m = span.Start.Minute; m < span.Stop.Minute; m++)
                    {
                        minutes[m]++;
                    }
                }
            }

            int max = 0;
            for (int m = 1; m < minutes.Length; m++)
            {
                if (minutes[m] > minutes[max])
                {
                    max = m;
                }
            }

            sleepCount = minutes[max];
            return max;
        }

        static int FindMostSleptMinute(Guard[] guards, out int guardId)
        {
            int mostSleptMinute = FindSleepiestMinute(guards[0], out int maxSleepCount);
            guardId = guards[0].Id;

            for (int i = 1; i < guards.Length; i++)
            {
                int minute = FindSleepiestMinute(guards[i], out int sleepCount);

                if (sleepCount > maxSleepCount)
                {
                    maxSleepCount = sleepCount;
                    mostSleptMinute = minute;
                    guardId = guards[i].Id;
                }
            }

            return mostSleptMinute;
        }
    }
}