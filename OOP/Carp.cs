using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace OOP
{
    class Carp:Fish
    {
		public Carp(string imagePath, int depth) : base(imagePath, depth)
		{
			Species = "Carp";
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
