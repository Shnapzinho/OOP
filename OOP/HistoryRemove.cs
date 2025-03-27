using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP
{
	public class HistoryRemove : ICommand
	{
		private readonly AnimalList _animalList;
		private readonly Animal _animal;
		private readonly int _index;

		public HistoryRemove(AnimalList animalList, Animal animal, int index)
		{
			_animalList = animalList;
			_animal = animal;
			_index = index;
		}

		public void Execute()
		{
			_animalList.RemoveAnimal(_animal);
		}

		public void Undo()
		{
			_animalList.GetAnimals().Insert(_index, _animal);
		}
	}
}
