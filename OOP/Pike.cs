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
		public Pike(string imagePath, int depth) : base(imagePath, depth)
		{
			Species = "Pike";
			Depth = depth;
		}

		public override string Sound()
		{
			return "boolk";
		}
		public override string ToString()
		{
			return $"{base.ToString()}, Depth: {Depth}m - {Sound()}, {Move()}";
		}
	}
}
