namespace TheSumOfItsParts
{
    using System;
    using System.Linq;
    using System.Text;

    using Dependencies = System.Collections.Generic.SortedDictionary<char, System.Collections.Generic.List<char>>;
    using Jobs = System.Collections.Generic.Dictionary<char, int>;

    class Resolver
    {
        private Dependencies dependencies;

        private Dependencies working;

        private Jobs jobs;

        private int baseTime;

        public Resolver(Dependencies dependencies)
        {
            if (dependencies == null)
            {
                throw new ArgumentNullException("Dependencies can't be null.");
            }

            this.dependencies = dependencies;
            this.jobs = new Jobs();
        }

        public string FindAssemblyOrder()
        {
            this.SetWorking();

            StringBuilder result = new StringBuilder();

            while (this.working.Any())
            {
                char? next = ExecuteStep();

                if (next != null)
                {
                    result.Append(next);
                }
            }

            return result.ToString();
        }

        public int CalcAssemblyTime(int workerCount, int baseTime = 60)
        {
            this.SetWorking();
            this.baseTime = baseTime;

            int time = 0;

            while (this.working.Any() || jobs.Any())
            {
                workerCount = StartJobs(workerCount);

                workerCount += FinishJobs(out int jobsTime);

                time += jobsTime;
            }

            return time;
        }

        private void SetWorking()
        {
            this.working = new Dependencies(this.dependencies.ToDictionary(dep => dep.Key, dep => dep.Value.ToList()));
        }

        private int StartJobs(int workerCount)
        {
            for (; workerCount > 0; workerCount--)
            {
                char? next = GetNextStep();
                if (next == null)
                {
                    break;
                }

                this.jobs[(char)next] = GetTime((char)next);
            }

            return workerCount;
        }

        private int FinishJobs(out int time)
        {
            time = this.jobs.Values.Min();

            int jobCount = this.jobs.Count;

            foreach (var step in this.jobs.Keys.ToArray())
            {
                this.jobs[step] -= time;

                if (this.jobs[step] == 0)
                {
                    this.jobs.Remove(step);
                    RemoveDependency(step);
                }
            }

            return jobCount - this.jobs.Count;
        }

        private int GetTime(char step)
        {
            return this.baseTime + step - 64;
        }

        private char? ExecuteStep()
        {
            char? next = GetNextStep();

            if (next == null)
            {
                return next;
            }

            RemoveDependency((char)next);

            return next;
        }

        private char? GetNextStep()
        {
            char? next = this.working.Keys.FirstOrDefault(step => !this.working[step].Any());

            if (next != '\0')
            {
                this.working.Remove((char)next);
                return next;
            }

            return null;
        }

        private void RemoveDependency(char step)
        {
            foreach (var dependency in this.working)
            {
                dependency.Value.Remove(step);
            }
        }
    }
}