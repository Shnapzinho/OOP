using OOP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP
{
	public class AnimalList
	{
		private List<Animal> animals = new List<Animal>();

		public void AddAnimal(Animal animal)
		{
			animals.Add(animal);
		}

		public List<Animal> GetAnimals()
		{
			return animals;
		}
	}
}
