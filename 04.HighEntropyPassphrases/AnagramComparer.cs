namespace HighEntropyPassphrases
{
    using System.Linq;
    using System.Collections.Generic;

    public class AnagramComparer : IEqualityComparer<string>
    {
        public bool Equals(string x, string y)
        {
            return x.All(@char => y.Contains(@char));
        }

        public int GetHashCode(string obj)
        {
            if (string.IsNullOrEmpty(obj))
            {
                return obj.GetHashCode();
            }

            int hash = obj[0].GetHashCode();

            for (int i = 1; i < obj.Length; i++)
            {
                hash ^= obj[i].GetHashCode();
            }

            return hash;
        }
    }
}