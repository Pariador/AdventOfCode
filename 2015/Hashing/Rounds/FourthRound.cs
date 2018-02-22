namespace Hashing.Rounds
{
	internal class FourthRound : BaseRound
	{
		public FourthRound() 
			: base(new [] { 6, 10, 15, 21 })
		{
			;
		}

		public override RoundResult Execute(int[] chunkHash, int step)
		{
			RoundResult result = base.Execute(chunkHash, step);

			result.Value = chunkHash[2] ^ (chunkHash[1] | (~chunkHash[3]));

			result.WordIndex = (7 * step) % 16;

			return result;
		}
	}
}