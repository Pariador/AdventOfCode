namespace Hashing.Rounds
{
	internal class FirstRound : BaseRound
	{
		public FirstRound() 
			: base(new [] { 7, 12, 17, 22 })
		{
			;
		}

		public override RoundResult Execute(int[] chunkHash, int step)
		{
			RoundResult result = base.Execute(chunkHash, step);

			result.Value = (chunkHash[1] & chunkHash[2]) | ((~chunkHash[1]) & chunkHash[3]);

			result.WordIndex = step;

			return result;
		}
	}
}