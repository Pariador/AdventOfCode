namespace ParticleSwarm
{
    public static class Utils
    {
        public static int[] Add(this int[] arr, int[] other)
        {
            int[] result = new int[arr.Length];

            for (int i = 0; i < result.Length; i++)
            {
                result[i] = arr[i] + other[i];
            }

            return result;
        }

        public static bool ValueEquals(this int[] arr, int[] other)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] != other[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}