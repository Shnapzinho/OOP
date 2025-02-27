using OOP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP
{
	public class Eagle : Bird
	{
		public Eagle(string imagePath) : base(imagePath)
		{
			Species = "Eagle";
		}
		public override string Sound()
		{
			return "ay";
		}
	}
}

