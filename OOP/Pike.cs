using OOP;
using System;

namespace OOP
{
	[Serializable]
	public class Pike : Fish
	{
		public Pike() : base("", 0)
		{
			Species = "Pike";
		}

		public Pike(string imagePath, int depth) : base(imagePath, depth)
		{
			Species = "Pike";
		}

		public override string Sound() => "boolk";

		public override string ToString()
		{
			return $"{base.ToString()}{GetFishInfo()}";
		}
	}
}
