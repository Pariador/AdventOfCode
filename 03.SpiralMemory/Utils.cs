namespace SpiralMemory
{
    public static class Utils
    {
        public static bool IsValidPositon(int row, int col, int[,] grid)
        {
            return 0 <= row && row < grid.GetLength(0) &&
                0 <= col && col < grid.GetLength(1);
        }
    }
}