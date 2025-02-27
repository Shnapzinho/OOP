using OOP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP
{
	public abstract class Bird : Animal
	{
		public Bird(string imagePath) : base(imagePath)
		{
			Type = "Bird";
		}

		public override string Move()
		{
			return "Flying";
		}
	}
}
