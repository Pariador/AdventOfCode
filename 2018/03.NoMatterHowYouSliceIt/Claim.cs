namespace NoMatterHowYouSliceIt
{
    using System;
    using System.Text.RegularExpressions;

    class Claim
    {
        private static readonly Regex RectFormat = new Regex(@"#(\d+)\s+@\s+(\d+),(\d+):\s+(\d+)x(\d+)");

        public static Claim Parse(string str)
        {
            Match match = RectFormat.Match(str);

            if (!match.Success)
            {
                throw new ArgumentException("Wrong claim format!");
            }

            return new Claim
            (
                id: int.Parse(match.Groups[1].Value),
                row: int.Parse(match.Groups[3].Value),
                col: int.Parse(match.Groups[2].Value),
                width: int.Parse(match.Groups[4].Value),
                height: int.Parse(match.Groups[5].Value)
            );
        }

        public Claim(int id, int row, int col, int width, int height)
        {
            this.Id = id;
            this.Row = row;
            this.Col = col;
            this.Width = width;
            this.Height = height;
        }

        public int Id { get; set; }

        public int Row { get; set; }

        public int Col { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public override string ToString()
        {
            return $"Id: {this.Id}, Row: {this.Row}, Col: {this.Col}, Width: {this.Width}, Height: {this.Height}";
        }
    }
}