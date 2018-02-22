namespace TheIdealStockingStuffer
{
	using System;
	using System.Text;

	using Hashing;

	public static class Program
	{
		public const string SecretKey = "ckczppom";

		public static int FindLowestNumberThatGivesHashWithZeros(int zeroesCount)
		{
			string zeroes = new string('0', zeroesCount);

			int number = 0;
			while (true)
			{
				string hash = new Md5Hash(Program.SecretKey + number, Encoding.ASCII).ToString();

				string firstDigits = hash.Substring(0, zeroes.Length);

				if (firstDigits == zeroes)
				{
					return number;
				}

				number++;
			}
		}

		public static void Main()
		{
			Console.WriteLine("Searching...");
			Console.WriteLine($"Answer with 5 zeroes: {FindLowestNumberThatGivesHashWithZeros(5)}");

			Console.WriteLine("Searching...");
			Console.WriteLine($"Answer with 6 zeroes: {FindLowestNumberThatGivesHashWithZeros(6)}");
		}
	}
}
