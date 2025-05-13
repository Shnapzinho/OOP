// AnimalFactory.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.IO;

namespace OOP
{
	public static class AnimalFactory
	{
		private static Dictionary<string, Type> AnimalTypes;

		static AnimalFactory()
		{
			AnimalTypes = new Dictionary<string, Type>();
			LoadAnimalTypes(Assembly.GetExecutingAssembly());

			// Загрузка дополнительных сборок из папки Extensions
			string extensionsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Extensions");
			if (Directory.Exists(extensionsPath))
			{
				foreach (string dll in Directory.GetFiles(extensionsPath, "*.dll"))
				{
					try
					{
						Assembly assembly = Assembly.LoadFrom(dll);
						LoadAnimalTypes(assembly);
					}
					catch (Exception ex)
					{
						Console.WriteLine($"Error loading assembly {dll}: {ex.Message}");
					}
				}
			}
		}

		private static void LoadAnimalTypes(Assembly assembly)
		{
			var animalType = typeof(Animal);

			foreach (var type in assembly.GetTypes())
			{
				if (type.IsClass && !type.IsAbstract && animalType.IsAssignableFrom(type))
				{
					AnimalTypes[type.Name] = type;
				}
			}
		}

		public static IEnumerable<string> GetAvailableAnimalTypes()
		{
			return AnimalTypes.Keys.OrderBy(x => x);
		}

		// Остальной код остается без изменений
		public static Animal CreateAnimal(string animalType, string imagePath, params object[] additionalParameters)
		{
			if (AnimalTypes.TryGetValue(animalType, out Type type))
			{
				var parameters = new List<object> { imagePath };
				if (additionalParameters != null)
				{
					parameters.AddRange(additionalParameters);
				}

				return (Animal)Activator.CreateInstance(type, parameters.ToArray());
			}
			throw new ArgumentException($"Unknown animal type: {animalType}");
		}
	}
}