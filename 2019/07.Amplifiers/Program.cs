using System;
using System.IO;
using System.Linq;
using static System.Linq.Enumerable;

using Intcode;

namespace Amplifiers
{
    static class Program
    {
        static void Main()
        {
            var program = File.ReadAllText("amplifier.txt")
                .Split(',')
                .Select(int.Parse)
                .ToArray();

            int best = FindBestSettings(program, 0);
            int bestLooped = FindBestSettings(program, 5);

            Console.WriteLine($"Best: {best}");
            Console.WriteLine($"Best Looped: {bestLooped}");
        }

        static int FindBestSettings(int[] program, int from = 0, int count = 5)
        {
            int[] settings = Range(from, count).ToArray();

            var computers = new IntCompPlus[settings.Length];
            for (int i = 0; i < computers.Length; i++)
                computers[i] = new IntCompPlus(program);

            for (int i = 1; i < computers.Length; i++)
                computers[i - 1].Output = computers[i].Input;

            computers.Last().Output = computers.First().Input;

            int best = -1;
            do
            {
                foreach (var comp in computers)
                    comp.Reload();

                for (int c = 0; c < computers.Length; c++)
                    computers[c].Input.Enqueue(settings[c]);

                computers[0].Input.Enqueue(0);

                int res = Run(computers);

                if (best < res)
                    best = res;

            } while (NextPermutation(settings));

            return best;
        }

        static int Run(IntCompPlus[] computers)
        {
            bool halted = false;

            while (!halted)
            {
                halted = true;

                foreach (var comp in computers)
                    halted &= comp.Start() == ExitCode.Halt;
            }

            return computers.Last().Output.Dequeue();
        }

        static void Reverse<T>(T[] array, int start = 0)
        {
            int len = array.Length - start;

            int mid = len / 2;
            for (int i = 0; i < mid; i++)
            {
                var hold = array[start + i];
                array[start + i] = array[array.Length - 1 - i];
                array[array.Length - 1 - i] = hold;
            }
        }

        static bool NextPermutation<T>(T[] objects)
            where T : IComparable<T>
        {
            int descent = objects.Length - 1;
            for (; descent >= 1; descent--)
            {
                if (objects[descent - 1].CompareTo(objects[descent]) < 0)
                    break;
            }

            if (descent == 0)
                return false;

            int next = objects.Length - 1;
            for (; next >= descent; next--)
            {
                if (objects[descent - 1].CompareTo(objects[next]) < 0)
                    break;
            }

            var hold = objects[descent - 1];
            objects[descent - 1] = objects[next];
            objects[next] = hold;

            Reverse(objects, descent);

            return true;
        }
    }
}