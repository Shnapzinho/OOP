using OOP;
using System;

namespace OOP
{
	[Serializable]
	public abstract class Mammal : Animal
	{
		public Mammal() : base("")
		{
			Type = "Mammal";
		}
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
