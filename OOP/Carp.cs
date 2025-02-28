using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP
{
    class Carp:Fish
    {
		public Carp(string imagePath) : base(imagePath)
		{
			Species = "Carp";
		}

		public override string Sound()
		{
			return "boolk";
		}
	}
}
