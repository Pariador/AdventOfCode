namespace PerfectlySphericalHousesInAVacuum
{
	using System;
	using System.IO;
	using System.Collections.Generic;

	public static class Program
	{
		public static int CountHousesWithPresents(string direcions)
		{
			Position position = new Position(0, 0);

			var visited = new HashSet<Position> { position };

			int count = 1;
			foreach (var direction in direcions)
			{
				switch (direction)
				{
					case '>': 
						position.Y += 1;
						break;
					case '<':
						position.Y -= 1;
						break;
					case '^':
						position.X += 1;
						break;
					case 'v':
						position.X -= 1;
						break;
					default: throw new ArgumentException($"Unknown direction: {direction}");
				}

				if (!visited.Contains(position))
				{
					count++;
					visited.Add(position);
				}
			}

			return count;
		}

		public static int CountHousesWithPresentsUsingRoboSanta(string direcions)
		{
			Position santaPosition = new Position(0, 0);
			Position roboSantaPosition = santaPosition;

			var visited = new HashSet<Position> { santaPosition };

			int count = 1;
			for (int i = 0; i < direcions.Length; i++)
			{
				Position position = santaPosition;

				bool isRoboSantaTurn = i % 2 == 1;
				if (isRoboSantaTurn)
				{
					position = roboSantaPosition;
				}

				switch (direcions[i])
				{
					case '>':
						position.Y += 1;
						break;
					case '<':
						position.Y -= 1;
						break;
					case '^':
						position.X += 1;
						break;
					case 'v':
						position.X -= 1;
						break;
					default: throw new ArgumentException($"Unknown direction: {direcions[i]}");
				}

				if (!visited.Contains(position))
				{
					count++;
					visited.Add(position);
				}

				if (isRoboSantaTurn)
				{
					roboSantaPosition = position;
				}
				else
				{
					santaPosition = position;
				}
			}

			return count;
		}

		public static void Main()
		{
			string directions;
			using (var reader = new StreamReader("input.txt"))
			{	
				directions = reader.ReadToEnd();
			}

			Console.WriteLine($"Houses with presents: {CountHousesWithPresents(directions)}");
			Console.WriteLine($"Houses with presents, using robo santa: {CountHousesWithPresentsUsingRoboSanta(directions)}");
		}
	}
}