namespace ElectoromagneticMoat
{
    using System.Linq;

    public class Component
    {
        public static Component Parse(string str)
        {
            int[] data = str.Split('/')
                .Select(int.Parse)
                .ToArray();

            return new Component(data[0], data[1]);
        }

        public Component(int left = 0, int right = 0)
        {
            this.Left = left;
            this.Right = right;
        }

        public int Left { get; private set; }

        public int Right { get; private set; }
    }
}