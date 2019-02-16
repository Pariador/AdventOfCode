namespace ReposeRecord
{
    using System;

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