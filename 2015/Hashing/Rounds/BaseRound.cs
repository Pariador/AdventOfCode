namespace Hashing.Rounds
{
	internal abstract class BaseRound : IRound
	{
		protected readonly int[] RoundRotations;

		protected BaseRound(int[] roundRotations)
		{
			this.RoundRotations = roundRotations;
		}

		public virtual RoundResult Execute(int[] chunkHash, int step)
		{
			return new RoundResult
			{
				Rotation = this.RoundRotations[step % this.RoundRotations.Length]
			};
		}
	}
}