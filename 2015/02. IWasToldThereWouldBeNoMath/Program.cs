namespace IWasToldThereWouldBeNoMath
{
	using System;
	using System.IO;
	using System.Linq;

	public static class Program
	{
		public static int CalcNeededPaperForPresent(params int[] dimensions)
		{
			Array.Sort(dimensions);

			int result = 0;

			for (int i = 0; i < dimensions.Length - 1; i++)
			{
				for (int j = i + 1; j < dimensions.Length; j++)
				{
					int multi = 2;
					if (i == 0 && j == 1)
					{
						multi++;
					} 

					result += dimensions[i] * dimensions[j] * multi;
				}
			}

			return result;
		}

		public static int CalcNeededRibbonForPresent(params int[] dimensions)
		{
			Array.Sort(dimensions);

			int parameter = (dimensions[0] * 2) + (dimensions[1] * 2);
			int volume = dimensions[0] * dimensions[1] * dimensions[2];

			return parameter + volume;
		}

		public static void Main()
		{
			using (var reader = new StreamReader("input.txt"))
			{
				long totalNeededPaper = 0;
				long totalNeededRibbon = 0;

				while (true)
				{
					string line = reader.ReadLine();
					if (line == null)
					{
						break;
					}

					int[] dimensions = line.Split('x')
						.Select(int.Parse)
						.ToArray();

					totalNeededPaper += CalcNeededPaperForPresent(dimensions);

					totalNeededRibbon += CalcNeededRibbonForPresent(dimensions);
				}

				Console.WriteLine($"Needed paper for presents: {totalNeededPaper}");
				Console.WriteLine($"Needed ribbon for presents: {totalNeededRibbon}");
			}
		}
	}
}