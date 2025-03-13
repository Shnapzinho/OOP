using OOP;
using System;

namespace OOP
{
	public abstract class Fish : Animal
	{
		public int Depth { get; set; }
		public Fish(string imagePath, int depth) : base(imagePath)
		{
			Type = "Fish";
		}

		public override string Move()
		{
			return "Swimming";
		}
	}
}