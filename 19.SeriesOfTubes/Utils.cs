namespace SeriesOfTubes
{
    public static class Utils
    {
        public static bool IsValidIndex(int index, int length)
        {
            return 0 <= index && index < length;
        }
    }
}