namespace Hashing.Rounds
{
	internal interface IRound
	{
		RoundResult Execute(int[] chunkHash, int step);
	}
}