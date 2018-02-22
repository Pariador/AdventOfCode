namespace Hashing.Rounds
{
	internal class RoundResult
	{
		public RoundResult()
		{
			;
		}

		public RoundResult(int value, int wordIndex, int rotation)
		{
			this.Value = value;
			this.WordIndex = wordIndex;
			this.Rotation = rotation;
		}

		public int Value { get; set; }

		public int WordIndex { get; set; }

		public int Rotation { get; set; }
	}
}