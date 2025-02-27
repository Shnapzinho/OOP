using OOP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP
{
	public class Pike : Fish
	{
		public Pike(string imagePath) : base(imagePath)
		{
			Species = "Pike";
		}

		public override string Sound()
		{
			return "boolk";
		}
	}
}
