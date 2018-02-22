namespace Duet
{
    using System;

    public class Utils
    {
        public static bool IsPrime(int number)
        {
            if (number < 2)
            {
                return false;
            }
            else if (number == 2 || number == 3)
            {
                return true;
            }

            if (number % 2 == 0)
            {
                return false;
            }

            int sqrt = (int)Math.Sqrt(number);

            for (int i = 3; i <= sqrt; i += 2)
            {
                if (number % i == 0)
                {
                    return false;
                }
            }

            return true;
        }
    }
}