using OOP;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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

