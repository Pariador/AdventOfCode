namespace ReposeRecord
{
    using System;
    using System.Text.RegularExpressions;

    enum EventType
    {
        Start = 0,
        Sleep = 1,
        Wake = 2
    }

    class Event
    {
        public const int NoGuardId = -1;

        private static readonly Regex DateTimePattern = new Regex(@"\[(\d+)-(\d{2})-(\d{2})\s+(\d{2}):(\d{2})\]");

        public static Event Parse(string str)
        {
            int splitIndex = str.IndexOf(']') + 1;
            if (splitIndex == 0)
            {
                throw new ArgumentException("Invalid event format!");
            }

            string timePart = str.Substring(0, splitIndex)
                .Trim();
            string typePart = str.Substring(splitIndex)
                .Trim();

            DateTime time = ParseTime(timePart);
            EventType type = ParseType(typePart, out int guardId);

            return new Event(time, type, guardId);
        }

        private static DateTime ParseTime(string str)
        {
            Match match = DateTimePattern.Match(str);
            if (!match.Success)
            {
                throw new ArgumentException("Invalid date-time format!");
            }

            return new DateTime
            (
                year: int.Parse(match.Groups[1].Value),
                month: int.Parse(match.Groups[2].Value),
                day: int.Parse(match.Groups[3].Value),
                hour: int.Parse(match.Groups[4].Value),
                minute: int.Parse(match.Groups[5].Value),
                second: 0
            );
        }

        private static EventType ParseType(string str, out int guardId)
        {
            const string SleepCmd = "falls asleep";
            const string WakeCmd = "wakes up";

            guardId = NoGuardId;

            if (str == SleepCmd)
            {
                return EventType.Sleep;
            }
            else if (str == WakeCmd)
            {
                return EventType.Wake;
            }

            string[] words = str.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (words.Length != 4 || words[1][0] != '#')
            {
                throw new ArgumentException("Invalid event format!");
            }

            bool parsed = int.TryParse(words[1].Substring(1), out guardId);
            if (!parsed)
            {
                throw new ArgumentException("Invalid id!");
            }

            return EventType.Start;
        }

        public Event(DateTime time, EventType type, int guardId)
        {
            this.Time = time;
            this.Type = type;
            this.GuardId = guardId;
        }

        public DateTime Time { get; }

        public EventType Type { get; }

        public int GuardId { get; }

        public override string ToString()
        {
            return $"Time: {this.Time} | Type: {this.Type} | Gurd: {this.GuardId}";
        }
    }
}