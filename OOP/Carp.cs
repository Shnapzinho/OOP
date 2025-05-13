using OOP;
using System;

namespace OOP
{
	[Serializable]
	public class Carp : Fish
	{
		public Carp() : base("", 0)
		{
			Species = "Carp";
		}

		public Carp(string imagePath, int depth) : base(imagePath, depth)
		{
			Species = "Carp";
		}

		public override string Sound() => "boolk";

		public override string ToString()
		{
			return $"{base.ToString()}{GetFishInfo()}";
		}
	}
}
