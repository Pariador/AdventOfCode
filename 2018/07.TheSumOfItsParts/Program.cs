namespace TheSumOfItsParts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.IO;

    using Dependencies = System.Collections.Generic.SortedDictionary<char, System.Collections.Generic.List<char>>;
    using Jobs = System.Collections.Generic.Dictionary<char, int>;

    static class Program
    {
        static void Main()
        {
            var dependencies = ReadDependencies();
            var dependencies2 = new Dependencies(dependencies.ToDictionary(dep => dep.Key, dep => dep.Value.ToList()));

            string assemblyOrder = FindResolutionOrder(dependencies);

            Console.WriteLine($"Assembly order: {assemblyOrder}");

            int assemblyTime = CalcAssemblyTime(dependencies2, 5);

            Console.WriteLine($"Assembly time: {assemblyTime}");
        }

        static int CalcAssemblyTime(Dependencies dependencies, int workerCount)
        {
            int time = 0;

            var jobs = new Jobs();

            while (dependencies.Any() || jobs.Any())
            {
                workerCount = StartJobs(jobs, dependencies, workerCount);

                workerCount += FinishJobs(jobs, dependencies, out int jobsTime);

                time += jobsTime;
            }

            return time;
        }

        static int StartJobs(Jobs jobs, Dependencies dependencies, int workerCount)
        {
            //for
            //(
                //char? next = GetNextStep(dependencies);
                //workerCount > 0 && next != null;
                //workerCount--, next = GetNextStep(dependencies)
            //)
            for (; workerCount > 0; workerCount--)
            {
                char? next = GetNextStep(dependencies);
                if (next == null)
                {
                    break;
                }

                jobs[(char)next] = GetTime((char)next);
            }

            return workerCount;
        }

        static int FinishJobs(Jobs jobs, Dependencies dependencies, out int time)
        {
            time = jobs.Values.Min();

            int jobCount = jobs.Count;

            foreach (var step in jobs.Keys.ToArray())
            {
                jobs[step] -= time;

                if (jobs[step] == 0)
                {
                    jobs.Remove(step);
                    RemoveDependency(dependencies, step);
                }
            }

            return jobCount - jobs.Count;
        }

        static void RemoveDependency(Dependencies dependencies, char step)
        {
            foreach (var dependency in dependencies)
            {
                dependency.Value.Remove(step);
            }
        }

        static int GetTime(char step)
        {
            return 60 + step - 64;
        }

        static char? GetNextStep(Dependencies dependencies)
        {
            char? next = dependencies.Keys.FirstOrDefault(step => !dependencies[step].Any());

            if (next != '\0')
            {
                dependencies.Remove((char)next);
                return next;
            }

            return null;
        }

        static string FindResolutionOrder(Dependencies dependencies)
        {
            StringBuilder result = new StringBuilder();

            while (dependencies.Any())
            {
                char? next = ExecuteStep(dependencies);

                if (next != null)
                {
                    result.Append(next);
                }
            }

            return result.ToString();
        }

        static char? ExecuteStep(Dependencies dependencies)
        {
            char? next = GetNextStep(dependencies);

            if (next == null)
            {
                return next;
            }

            RemoveDependency(dependencies, (char)next);

            return next;
        }

        static Dependencies ReadDependencies()
        {
            var dependencies = new Dependencies();

            var reader = new StreamReader("input.txt");

            for (string line = reader.ReadLine(); line != null; line = reader.ReadLine())
            {
                string[] words = line.Split(' ');

                char step = words[7][0];
                if (!dependencies.ContainsKey(step))
                {
                    dependencies[step] = new List<char>();
                }

                char dependency = words[1][0];
                if (!dependencies.ContainsKey(dependency))
                {
                    dependencies[dependency] = new List<char>();
                }

                dependencies[step].Add(dependency);
            }

            reader.Dispose();

            return dependencies;
        }
    }
}