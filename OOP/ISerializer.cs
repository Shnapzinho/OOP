namespace OOP
{
	public interface ISerializer
	{
		string Serialize(List<Animal> animals);
		List<Animal> Deserialize(string data);
	}
}