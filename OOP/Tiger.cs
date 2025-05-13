namespace OOP
{
	[Serializable]
	public class Tiger : Mammal
	{
		public Tiger() : this("") { }  // Пустой конструктор

		public Tiger(string imagePath) : base(imagePath)
		{
			Species = "Tiger";
		}

		public override string Sound() => "rrrr";

		public override string ToString()
		{
			return $"{base.ToString()} - {Sound()}, {Move()}";
		}
	}
}