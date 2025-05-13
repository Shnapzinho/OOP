using System;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using System.Windows.Forms;
using System.Reflection;

namespace OOP
{
	public class XmlAnimalSerializer : ISerializer
	{
		public class AnimalListWrapper
		{
			public List<Animal> Animals { get; set; } = new List<Animal>();
		}

		public string Serialize(List<Animal> animals)
		{
			try
			{
				var wrapper = new AnimalListWrapper { Animals = animals };             
				var serializer = new XmlSerializer(typeof(AnimalListWrapper), GetAnimalTypes());
				using var writer = new StringWriter();
				serializer.Serialize(writer, wrapper);
				return writer.ToString();
			}
			catch (Exception ex)
			{
				MessageBox.Show($"XML Serialization Error: {ex.Message}\n\nDetails: {ex.InnerException?.Message}",
					"Serialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return null;
			}
		}

		public List<Animal> Deserialize(string data)
		{
			try
			{
				var serializer = new XmlSerializer(typeof(AnimalListWrapper), GetAnimalTypes());
				using var reader = new StringReader(data);
				var wrapper = (AnimalListWrapper)serializer.Deserialize(reader);
				return wrapper?.Animals ?? new List<Animal>();
			}
			catch (Exception ex)
			{
				MessageBox.Show($"XML Deserialization Error: {ex.Message}\n\nIs the file corrupted?",
					"Deserialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return new List<Animal>();
			}
		}

		private Type[] GetAnimalTypes()
		{
			return Assembly.GetExecutingAssembly()
				.GetTypes()
				.Where(t => t.IsClass && !t.IsAbstract && typeof(Animal).IsAssignableFrom(t))
				.ToArray();
		}
	}
}