namespace Hashing.Rounds
{
	internal class SecondRound : BaseRound
	{
		public SecondRound() 
			: base(new [] { 5, 9, 14, 20 })
		{
			;
		}

		public override RoundResult Execute(int[] chunkHash, int step)
		{
			RoundResult result = base.Execute(chunkHash, step);

			result.Value = (chunkHash[3] & chunkHash[1]) | ((~chunkHash[3]) & chunkHash[2]);

			result.WordIndex = ((5 * step) + 1) % 16;

			return result;
		}
	}
}