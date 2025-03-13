using OOP;
using System;

namespace OOP
{
	public abstract class Mammal : Animal
	{
		public Mammal(string imagePath) : base(imagePath)
		{
			Type = "Mammal";
		}

		public override string Move()
		{
			return "Walking";
		}
	}
}
