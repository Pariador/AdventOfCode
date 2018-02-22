namespace PerfectlySphericalHousesInAVacuum
{
	public struct Position
	{
		public Position(int x, int y)
		{
			this.X = x;
			this.Y = y;
		}

		public int X { get; set; }

		public int Y { get; set; }

		public override bool Equals(object obj)
		{
			if (obj is Position)
			{
				Position other = (Position)obj;

				return (this.X == other.X) && (this.Y == other.Y);
			}

			return false;
		}

		public override int GetHashCode()
		{
			return ((this.X ^ this.Y) << 17) * 27;
		}
	}
}