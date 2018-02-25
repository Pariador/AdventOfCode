namespace Obscurity
{
    public class Room
    {
        public static Room Parse(string str)
        {
            str = str.Substring(0, str.Length - 1);

            int separatorIndex = str.LastIndexOf('-');

            string name = str.Substring(0, separatorIndex);

            string[] data = str.Substring(separatorIndex + 1, str.Length - (separatorIndex + 1))
                .Split('[');

            return new Room
            (
                name: name,
                id: int.Parse(data[0]),
                checksum: data[1]

            );
        }

        public Room(string name, int id, string checksum)
        {
            this.Name = name;
            this.Id = id;
            this.Checksum = checksum;
        }

        public string Name { get; set; }

        public int Id { get; }

        public string Checksum { get; }
    }
}