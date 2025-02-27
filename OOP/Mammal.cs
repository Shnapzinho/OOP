using OOP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP
{
	public abstract class Mammal : Animal
	{
		public Mammal(string imagePath) : base(imagePath)
		{
			Type = "Mammal";
		}

		public override string Move()
		{
			return "Walking";
		}
	}
}
