using OOP;

public static class AnimalFactory
{
	private static readonly Dictionary<string, Type> AnimalTypes = new Dictionary<string, Type>
	{
		{ "Dog", typeof(Dog) },
		{ "Carp", typeof(Carp) },
		{ "Pike", typeof(Pike) },
		{ "Tiger", typeof(Tiger) },
		{ "Eagle", typeof(Eagle) },
	};

	public static Animal CreateAnimal(string animalType, string imagePath, params object[] additionalParameters)
	{
		if (AnimalTypes.TryGetValue(animalType, out Type type))
		{
			var parameters = new List<object> {imagePath};
			if (additionalParameters != null)
			{
				parameters.AddRange(additionalParameters);
			}

			return (Animal)Activator.CreateInstance(type, parameters.ToArray());
		}
		throw new ArgumentException("Unknown animal type");
	}
}