using OOP;
using System;

namespace OOP
{
	public class Eagle : Bird
	{
		public int FlightSpeed { get; set; }
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

