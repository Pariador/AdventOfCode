namespace ReposeRecord
{
    using System.Linq;
    using System.Collections.Generic;

    class Guard
    {
        public static Guard[] ToGuards(Session[] sessions)
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

        public Guard(int id)
        {
            this.Id = id;
            this.Sessions = new List<Session>();
        }

        public int Id { get; }

        public List<Session> Sessions { get; }
    }
}