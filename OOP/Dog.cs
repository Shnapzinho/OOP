using OOP;
using System;

public class Dog : Animal
{
	public string Name { get; set; }
	public string Breed { get; set; }

	public Dog(string imagePath, string name, string breed) : base(imagePath)
	{
		Species = "Dog";
		Type = "Mammal";
		Name = name;
		Breed = breed;
	}

	public override string Sound()
	{
		return "woof";
	}

	public override string Move()
	{
		return "walking";
	}

	public override string ToString()
	{
		return $"{Name} {base.ToString()}, Breed: {Breed} - {Sound()}, {Move()}";
	}
}