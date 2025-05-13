using OOP;
using System;

namespace OOP
{
	[Serializable]
	public abstract class Bird : Animal
	{
		public Bird() : base("")
		{
			Type = "Bird";
		}
		public Bird(string imagePath) : base(imagePath)
		{
			Type = "Bird";
		}

		public override string Move()
		{
			return "Flying";
		}
	}
}
