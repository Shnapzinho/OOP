using OOP;
using System;

namespace AnimalExtensions
{
	[Serializable]
	public class Lion : Mammal
	{
		public int ManeSize { get; set; }
		public int MaxSpeed { get; set; } // Максимальная скорость бега в км/ч

		public Lion() : this("", 0, 0) { }

		public Lion(string imagePath, int maneSize, int maxSpeed) : base(imagePath)
		{
			Species = "Lion";
			ManeSize = maneSize;
			MaxSpeed = maxSpeed;
		}

		public override string Sound() => "roar";

		public override string ToString()
		{
			return $"{base.ToString()}, Mane Size: {ManeSize}cm, Max Speed: {MaxSpeed}km/h - {Sound()}, {Move()}";
		}
	}
}