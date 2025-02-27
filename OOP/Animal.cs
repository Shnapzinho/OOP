using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP
{
	public abstract class Animal
	{
		public string ImagePath { get; set; }
		public string Type { get; protected set; }
		public string Species { get; protected set; }

		public Animal(string imagePath)
		{
			ImagePath = imagePath;
		}

		public abstract string Sound();
		public abstract string Move();

	}
}
