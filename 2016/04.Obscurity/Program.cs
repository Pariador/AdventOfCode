namespace Obscurity
{
    using System.IO;
    using System.Linq;
    using System.Collections.Generic;
    using System;
    using System.Text;

    public static class Program
    {
        public static void Main()
        {
            Room[] rooms = ReadRooms("input.txt");

            rooms = rooms.Where(room => room.Checksum == CalcChecksum(room.Name))
                .ToArray();

            var sum = rooms.Sum(room => room.Id);

            Console.WriteLine($"Valid rooms IDs sum: {sum}");

            string query = "northpole";

            foreach (var room in rooms)
            {
                room.Name = Decode(room.Name, room.Id);
            }

            int id = rooms.First(room => room.Name.Contains(query)).Id;

            Console.WriteLine($"North Pole objects are in room: {id}");

        }

        public static Room[] ReadRooms(string fileName)
        {
            var rooms = new List<Room>();

            using (var reader = new StreamReader(fileName))
            {
                while (true)
                {
                    string line = reader.ReadLine();
                    if (line == null)
                    {
                        break;
                    }

                    Room room = Room.Parse(line);

                    rooms.Add(room);
                }
            }

            return rooms.ToArray();
        }

        public static string Decode(string encoded, int key)
        {
            StringBuilder builder = new StringBuilder();

            foreach (var @char in encoded)
            {
                if (@char == '-')
                {
                    builder.Append(' ');
                }
                else
                {
                    builder.Append(Decode(@char, key));
                }
            }

            return builder.ToString();
        }

        public static char Decode(char encoded, int shift)
        {
            return (char)((((encoded - 'a') + shift) % 26) + 'a');
        }

        public static string CalcChecksum(string name)
        {
            name = name.Replace("-", "");

            var charCounts = new Dictionary<char, int>();

            foreach (var @char in name)
            {
                if (charCounts.ContainsKey(@char))
                {
                    charCounts[@char]++;
                }
                else
                {
                    charCounts[@char] = 1;
                }
            }

            var chars = charCounts.OrderByDescending(pair => pair.Value)
                .ThenBy(pair => pair.Key)
                .Take(5)
                .Select(pair => pair.Key)
                .ToArray(); 

            return new string(chars);
        }
    }
}