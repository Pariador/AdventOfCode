namespace HexEd
{
    using System;
    using System.Linq;

    public static class Utils
    {
        public static Direction ParseDirection(string str)
        {
            switch (str)
            {
                case "n":
                    return Direction.North;
                case "s":
                    return Direction.South;
                case "ne":
                    return Direction.NorthEast;
                case "sw":
                    return Direction.SouthWest;
                case "nw":
                    return Direction.NorthWest;
                case "se":
                    return Direction.SouthEast;
                default:
                    throw new ArgumentException("Unknown direction!");
            }
        }

        public static Direction[] ParseDirections(string str)
        {
            return str.Split(',')
                .Select(Utils.ParseDirection)
                .ToArray();
        }
    }
}