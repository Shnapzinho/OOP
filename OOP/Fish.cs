using OOP;
using System;


namespace OOP
{
	[Serializable]
	public abstract class Fish : Animal
	{
		public int Depth { get; set; }

		public Fish() : this("", 0) { }

		public Fish(string imagePath, int depth) : base(imagePath)
		{
			Type = "Fish";
			Depth = depth;
		}

		public override string Move()
		{
			return "Swimming";
		}

		protected string GetFishInfo()
		{
			return $", Depth: {Depth}m - {Sound()}, {Move()}";
		}
	}
}