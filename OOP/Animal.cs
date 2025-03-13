using OOP;
using System;

namespace OOP
{
	public abstract class Animal
	{
		public string ImagePath { get; set; }
		public string Type { get; set; }
		public string Species { get; set; }
		public static int Count { get; private set; }

		static Animal()
		{
			Count = 0;
		}

		public Animal(string imagePath)
		{
			ImagePath = imagePath;
			Count++;
		}

		public abstract string Sound();
		public abstract string Move();
		public override string ToString()
		{
			return $"({Species}, {Type})";
		}
	}
}
