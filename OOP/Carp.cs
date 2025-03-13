using OOP;
using System;

namespace OOP
{
    class Carp:Fish
    {
		public Carp(string imagePath, int depth) : base(imagePath, depth)
		{
			Species = "Carp";
			Depth = depth;
		}

		public override string Sound()
		{
			return "boolk";
		}

		public override string ToString()
		{
			return $"{base.ToString()}, Depth: {Depth}m - {Sound()}, {Move()}";
		}
	}
}
