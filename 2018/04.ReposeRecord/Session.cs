namespace ReposeRecord
{
    using System;
    using System.Collections.Generic;

    class Session
    {
        public static Session[] GetSessions(Event[] events)
        {
            var sessions = new List<Session>();

            var groups = GroupByStart(events);

            foreach (var group in groups)
            {
                Event start = group.Key;
                Event[] times = group.Value;

                Session session = new Session(start.GuardId, start.DateTime.Date);

                for (int i = 0; i < times.Length; i += 2)
                {
                    SleepSpan sleepSpan = new SleepSpan(times[i].DateTime, times[i + 1].DateTime);

                    session.SleepSpans.Add(sleepSpan);
                }

                sessions.Add(session);
            }

            return sessions.ToArray();
        }

        private static Dictionary<Event, Event[]> GroupByStart(Event[] events)
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
}