using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP
{
	public class HistoryModify : HistoryInt
	{
		private readonly AnimalList _animalList;
		private readonly int _index;
		private readonly Animal _originalAnimal;
		private readonly Animal _modifiedAnimal;

		public HistoryModify(AnimalList animalList, int index, Animal originalAnimal, Animal modifiedAnimal)
		{
			_animalList = animalList;
			_index = index;
			_originalAnimal = originalAnimal;
			_modifiedAnimal = modifiedAnimal;
		}

		public void Execute()
		{
			_animalList.GetAnimals()[_index] = _modifiedAnimal;
		}

		public void Undo()
		{
			_animalList.GetAnimals()[_index] = _originalAnimal;
		}
	}
}
