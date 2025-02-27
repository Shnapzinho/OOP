using OOP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP
{
	public class Tiger : Mammal
	{
		public Tiger(string imagePath) : base(imagePath)
		{
			Species = "Tiger";
		}

		public override string Sound()
		{
			return "rrrr";
		}
	}
}