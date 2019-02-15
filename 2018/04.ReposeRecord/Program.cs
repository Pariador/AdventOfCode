namespace ReposeRecord
{
    using System;
    using System.Linq;
    using System.IO;
    using System.Collections.Generic;

    static class Program
    {
        const string Sep = "-----------------------";

        static void Main()
        {
            Event[] events = File.ReadAllLines("input.txt")
                .Select(log => Event.Parse(log))
                .OrderBy(@event => @event.Time)
                .ToArray();

            Session[] sessions = GetSessions(events);

            var guards = ToGuards(sessions);

            Guard sleepiestGuard = FindSleepiestGuard(guards);
            int sleepiesMinute = FindSleepiestMinute(sleepiestGuard, out int sleepCount);

            var gg = guards.Select(guard =>
            {
                int minute = FindSleepiestMinute(guard, out int count);
                return new { GuardId = guard.Id, Minute = minute, SleepCount = count };
            }).OrderByDescending(guard => guard.SleepCount)
            .First();

            Console.WriteLine(sleepiestGuard.Id);
            Console.WriteLine(sleepiesMinute);
            Console.WriteLine(sleepiestGuard.Id * sleepiesMinute);
            Console.WriteLine(gg.GuardId * gg.Minute);
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

        static Guard FindSleepiestGuard(Guard[] guards)
        {
            Guard guard = guards[0];
            TimeSpan max = GetSleepingTime(guards[0]);

            for (int i = 1; i < guards.Length; i++)
            {
                TimeSpan sleepintTime = GetSleepingTime(guards[i]);

                if (sleepintTime > max)
                {
                    guard = guards[i];
                    max = sleepintTime;
                }
            }

            return guard;
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

        static Dictionary<Event, Event[]> GroupByStart(Event[] events)
        {
            var groups = new Dictionary<Event, Event[]>();

            Event start = events[0];
            List<Event> group = new List<Event>();

            for (int i = 1; i < events.Length; i++)
            {
                if (events[i].Type == EventType.Start)
                {
                    groups[start] = group.ToArray();

                    start = events[i];
                    group.Clear();
                }
                else
                {
                    group.Add(events[i]);
                }

            }

            groups[start] = group.ToArray();

            return groups;
        }

        static Session[] GetSessions(Event[] events)
        {
            var sessions = new List<Session>();

            var groups = GroupByStart(events);

            foreach (var group in groups)
            {
                Event start = group.Key;
                Event[] times = group.Value;

                Session session = new Session(start.GuardId, start.Time.Date);

                for (int i = 0; i < times.Length; i += 2)
                {
                    SleepSpan sleepSpan = new SleepSpan(times[i].Time, times[i + 1].Time);

                    session.SleepSpans.Add(sleepSpan);
                }

                sessions.Add(session);
            }

            return sessions.ToArray();
        }

        static Guard[] ToGuards(Session[] sessions)
        {
            var guards = new Dictionary<int, Guard>();

            foreach (var session in sessions)
            {
                if (!guards.ContainsKey(session.GuardId))
                {
                    guards[session.GuardId] = new Guard(session.GuardId);
                }

                guards[session.GuardId].Sessions.Add(session);
            }

            return guards.Values.ToArray();
        }
    }

    class Guard
    {
        public Guard(int id)
        {
            this.Id = id;
            this.Sessions = new List<Session>();
        }

        public int Id { get; }

        public List<Session> Sessions { get; }
    }

    class Session
    {
        public Session(int guardId, DateTime date)
        {
            this.GuardId = guardId;
            this.Date = date;
            this.SleepSpans = new List<SleepSpan>();
        }

        public int GuardId { get; }

        public DateTime Date { get; }

        public List<SleepSpan> SleepSpans { get; }
    }

    class SleepSpan
    {
        public SleepSpan(DateTime start, DateTime stop)
        {
            this.Start = start;
            this.Stop = stop;
        }

        public DateTime Start { get; }

        public DateTime Stop { get; }
    }
}