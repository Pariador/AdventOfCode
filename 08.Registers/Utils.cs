namespace Registers
{
    using System.Collections.Generic;

    public static class Utils
    {
        public static void InitRegister(string key, Dictionary<string, int> regisers)
        {
            if (!regisers.ContainsKey(key))
            {
                regisers[key] = 0;
            }
        }
    }
}