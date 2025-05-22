using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace OOP
{
	public static class AnimalFactory
	{
		private static Dictionary<string, Type> _animalTypes = new Dictionary<string, Type>();
		private static List<Assembly> _loadedAssemblies = new List<Assembly>();

		static AnimalFactory()
		{
			LoadAssembly(Assembly.GetExecutingAssembly());
		}

		public static void LoadAssembly(Assembly assembly)
		{
			if (_loadedAssemblies.Contains(assembly)) return;

			_loadedAssemblies.Add(assembly);
			UpdateAnimalTypes(assembly);
		}

		private static void UpdateAnimalTypes(Assembly assembly)
		{
			foreach (var type in assembly.GetTypes()
				.Where(t => t.IsClass &&
						   !t.IsAbstract &&
						   typeof(Animal).IsAssignableFrom(t)))
			{
				if (!_animalTypes.ContainsKey(type.Name))
				{
					_animalTypes[type.Name] = type;
				}
			}
		}

		public static Type GetAnimalType(string animalType)
		{
			return _animalTypes.TryGetValue(animalType, out Type type) ? type : null;
		}

		public static IEnumerable<string> GetAvailableAnimalTypes()
		{
			return _animalTypes.Keys.OrderBy(x => x);
		}

		public static Animal CreateAnimal(string animalType, string imagePath, params object[] additionalParameters)
		{
			if (_animalTypes.TryGetValue(animalType, out Type type))
			{
				var parameters = new List<object> { imagePath };
				parameters.AddRange(additionalParameters ?? Array.Empty<object>());

				try
				{
					return (Animal)Activator.CreateInstance(type, parameters.ToArray());
				}
				catch (Exception ex)
				{
					throw new ArgumentException($"Failed to create {animalType}: {ex.Message}");
				}
			}
			throw new ArgumentException($"Unknown animal type: {animalType}");
		}
	}
}