using OOP;
using System;

namespace OOP
{
	public class Tiger : Mammal
	{
		public Tiger(string imagePath) : base(imagePath)
		{
			Species = "Tiger";
		}

		public override string Sound()
		{
			return "rrrr";
		}
		public override string ToString()
		{
			return $"{base.ToString()} - {Sound()}, {Move()}";
		}
	}
}