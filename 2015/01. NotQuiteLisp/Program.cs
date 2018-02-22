namespace NotQuiteLisp
{
	using System;
	using System.IO;

	public static class Program
	{
		public static int GetFinalFloor(string directions)
		{
			int floor = 0;

			foreach (var direction in directions)
			{
				if (direction == '(')
				{
					floor++;
				}
				else
				{
					floor--;
				}
			}

			return floor;
		}

		public static int FindCharWichLeadsToBasement(string directions)
		{
			int floor = 0;

			for (int i = 0; i < directions.Length; i++)
			{
				if (directions[i] == '(')
				{
					floor++;
				}
				else
				{
					floor--;
				}

				if (floor == -1)
				{
					return i + 1;
				}
			}

			throw new ArgumentException("Basement floor never reached.");
		}

		public static void Main()
		{
			string directions;
			using (var reader = new StreamReader("input.txt"))
			{
				directions = reader.ReadToEnd();
			}

			Console.WriteLine($"{nameof(GetFinalFloor)}: {GetFinalFloor(directions)}");
			Console.WriteLine($"{nameof(FindCharWichLeadsToBasement)}: {FindCharWichLeadsToBasement(directions)}");
		}
	}
}