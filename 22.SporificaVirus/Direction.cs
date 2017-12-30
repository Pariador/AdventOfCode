namespace SporificaVirus
{
    using System;

    public enum Direction
    {
        Up = 0,
        Right = 1,
        Down = 2,
        Left = 3
    }

    public enum Turn
    {
        Clockwise = 1,
        CounterClockwise = -1
    }

    public static class DirectionExtension
    {
        public static Direction Turn(this Direction direction, Turn turn)
        {
            int directionsCount = Enum.GetValues(typeof(Direction)).Length;

            return (Direction)((((int)direction + (int)turn) + directionsCount) % directionsCount);
        }
    }
}