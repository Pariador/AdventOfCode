namespace Hashing
{
	public static class Extensions
	{
		public static uint LeftRotate(this uint original, int amount)
		{
			return (original << amount) | (original >> (32 - amount));
        }

		public static int LeftRotate(this int original, int amount)
		{
			uint rotated = ((uint)original).LeftRotate(amount);
			return (int)rotated;
		}
	}
}