using OOP;
using System;

namespace OOP
{
	public abstract class Bird : Animal
	{
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
