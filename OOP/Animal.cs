using OOP;
using System;
using System.Text.Json.Serialization;

namespace OOP
{
	[Serializable]
	public abstract class Animal
	{
		public string ImagePath { get; set; }
		public string Type { get; set; }
		public string Species { get; set; }
		public static int Count { get; private set; }

		protected Animal() : this("") { }

		static Animal()
		{
			Count = 0;
		}

		protected Animal(string imagePath)
		{
			ImagePath = imagePath ?? "";
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
