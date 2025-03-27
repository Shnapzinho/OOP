using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP
{
	public class HistoryAdd : ICommand
	{
		private readonly AnimalList _animalList;
		private readonly Animal _animal;

		public HistoryAdd(AnimalList animalList, Animal animal)
		{
			_animalList = animalList;
			_animal = animal;
		}

		public void Execute()
		{
			_animalList.AddAnimal(_animal);
		}

		public void Undo()
		{
			_animalList.RemoveAnimal(_animal);
		}
	}
}
