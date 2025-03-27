using OOP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public static class AnimalFactory
{
	private static readonly Dictionary<string, Type> AnimalTypes;

	static AnimalFactory()
	{
		AnimalTypes = new Dictionary<string, Type>();
		var assembly = Assembly.GetExecutingAssembly();
		var animalType = typeof(Animal);

		foreach (var type in assembly.GetTypes())
		{
			if (type.IsClass && !type.IsAbstract && animalType.IsAssignableFrom(type))
			{
				AnimalTypes.Add(type.Name, type);
			}
		}
	}

	public static Animal CreateAnimal(string animalType, string imagePath, params object[] additionalParameters)
	{
		if (AnimalTypes.TryGetValue(animalType, out Type type))
		{
			var parameters = new List<object> { imagePath };
			if (additionalParameters != null)
			{
				parameters.AddRange(additionalParameters);
			}

			var animal = (Animal)Activator.CreateInstance(type, parameters.ToArray());
			return animal;
		}
		throw new ArgumentException($"Unknown animal type: {animalType}");
	}
}