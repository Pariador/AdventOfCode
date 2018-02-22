namespace KnotHash
{
    using System;
    using System.Linq;
    using System.Text;

    public static class Program
    {
        public static void Main()
        {
            string input = "130,126,1,11,140,2,255,207,18,254,246,164,29,104,0,224";
            byte[] data = input.Split(',')
                .Select(byte.Parse)
                .ToArray();

            Hasher hasher = new Hasher();

            byte[] firstHash = hasher.ExecuteRound(data);
            int checksum = KnotHashCheckSum(firstHash);

            byte[] secondHash = hasher.Hash(input);

            Console.WriteLine($"One hash round checksum: {checksum}");
            Console.WriteLine($"Full Hash: {ToHex(secondHash)}");
        }

        public static string ToHex(byte[] data)
        {
            StringBuilder result = new StringBuilder();

            foreach (var @byte in data)
            {
                result.Append(@byte.ToString("X").PadLeft(2, '0'));
            }

            return result.ToString().ToLower();
        }

        public static int KnotHashCheckSum(byte[] data)
        {
            return data[0] * data[1];
        }
    }
}