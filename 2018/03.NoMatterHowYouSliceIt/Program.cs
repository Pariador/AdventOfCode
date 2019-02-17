namespace NoMatterHowYouSliceIt
{
    using System;
    using System.IO;
    using System.Linq;

    static class Program
    {
        const int Overlap = -1;
        const int Empty = 0;

        static void Main()
        {
            var claims = File.ReadAllLines("input.txt")
                .Select(Claim.Parse)
                .ToArray();

            int best = FindBestClaim(claims, out short[,] fabric);
            int overlapsCount = CountOverlaps(fabric);

            Console.WriteLine($"Squere inches overlap: {overlapsCount}");
            Console.WriteLine($"Best claim: {best}");

        }

        static int FindBestClaim(Claim[] claims, out short[,] fabric)
        {
            fabric = new short[1000, 1000];
            var ids = claims.Select(rect => rect.Id)
                .ToHashSet();

            foreach (var claim in claims)
            {
                for (int row = claim.Row; row < claim.Row + claim.Height; row++)
                {
                    for (int col = claim.Col; col < claim.Col + claim.Width; col++)
                    {
                        if (fabric[row, col] == Empty)
                        {
                            fabric[row, col] = (short)claim.Id;
                        }
                        else
                        {
                            ids.Remove(claim.Id);

                            if (fabric[row, col] != Overlap)
                            {
                                ids.Remove(fabric[row, col]);
                                fabric[row, col] = Overlap;
                            }
                        }
                    }
                }
            }

            return ids.First();
        }

        static int CountOverlaps(short[,] fabric)
        {
            int overlapsCount = 0;

            for (int row = 0; row < fabric.GetLength(0); row++)
            {
                for (int col = 0; col < fabric.GetLength(1); col++)
                {
                    if (fabric[row, col] == Overlap)
                    {
                        overlapsCount++;
                    }
                }
            }

            return overlapsCount;
        }
    }
}