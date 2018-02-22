namespace Hashing.Rounds
{
	internal class ThirdRound : BaseRound
	{
		public ThirdRound() 
			: base(new [] { 4, 11, 16, 23 })
		{
			;
		}

		public override RoundResult Execute(int[] chunkHash, int step)
		{
			RoundResult result = base.Execute(chunkHash, step);

			result.Value = chunkHash[1] ^ chunkHash[2] ^ chunkHash[3];

			result.WordIndex = ((3 * step) + 5) % 16;

			return result;
		}
	}
}