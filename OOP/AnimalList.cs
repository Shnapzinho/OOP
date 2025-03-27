using OOP;
using System;

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

		public void RemoveAnimal(Animal animal)
		{
			animals.Remove(animal);
		}
	}
}
