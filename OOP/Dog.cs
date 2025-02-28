using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP
{
    class Dog:Mammal
    {
		public Dog(string imagePath) : base(imagePath)
		{
			Species = "Dog";
		}

		public override string Sound()
		{
			return "woof";
		}
	}
}
