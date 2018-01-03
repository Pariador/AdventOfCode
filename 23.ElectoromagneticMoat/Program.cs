namespace ElectoromagneticMoat
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Collections.Generic;

    public static class Program
    {
        public static void Main()
        {
            var components = ReadComponents("input.txt");

            Builder builder = new Builder();

            int result = builder.FindStrongest(components);

            Console.WriteLine($"Strongest: {result}");

            Console.WriteLine($"Longest Strength: {builder.LongestStrength}");
            Console.WriteLine($"Longest Length: {builder.LongestLength}");
        }


        public static HashSet<Component> ReadComponents(string file)
        {
            var components = new HashSet<Component>();

            using (var reader = new StreamReader(file))
            {
                while (true)
                {
                    string line = reader.ReadLine();
                    if (line == null)
                    {
                        break;
                    }

                    Component component = Component.Parse(line);

                    components.Add(component);
                }
            }

            return components;
        }
    }
}