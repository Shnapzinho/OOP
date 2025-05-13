using OOP;
using System;

namespace OOP
{
	[Serializable]
	public class Eagle : Bird
	{
		public int FlightSpeed { get; set; }
		public Eagle() : base("")
		{
			Species = "Eagle";
			FlightSpeed = 0;
		}
		public Eagle(string imagePath, int flightSpeed) : base(imagePath)
		{
			Species = "Eagle";
			FlightSpeed = flightSpeed;
		}
		public override string Sound()
		{
			return "ay";
		}
		public override string ToString()
		{
			return $"{base.ToString()}, Flight Speed: {FlightSpeed}km/h - {Sound()}, {Move()}";
		}
	}
}

