using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OOP
{
	public class JsonAnimalSerializer : ISerializer
	{
		private static readonly Dictionary<string, Type> AnimalTypes;

		static JsonAnimalSerializer()
		{
			AnimalTypes = LoadAnimalTypes();
		}

		private static Dictionary<string, Type> LoadAnimalTypes()
		{
			var types = new Dictionary<string, Type>();
			var animalType = typeof(Animal);

			// Загрузка из текущей сборки
			foreach (var type in Assembly.GetExecutingAssembly().GetTypes())
			{
				if (type.IsClass && !type.IsAbstract && animalType.IsAssignableFrom(type))
				{
					types.Add(type.Name, type);
				}
			}

			// Загрузка из дополнительных сборок
			string extensionsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Extensions");
			if (Directory.Exists(extensionsPath))
			{
				foreach (string dll in Directory.GetFiles(extensionsPath, "*.dll"))
				{
					try
					{
						Assembly assembly = Assembly.LoadFrom(dll);
						foreach (var type in assembly.GetTypes())
						{
							if (type.IsClass && !type.IsAbstract && animalType.IsAssignableFrom(type))
							{
								types.Add(type.Name, type);
							}
						}
					}
					catch { /* Игнорируем ошибки */ }
				}
			}

			return types;
		}

		public string Serialize(List<Animal> animals)
		{
			var options = new JsonSerializerOptions
			{
				WriteIndented = true,
				Converters = { new AnimalConverter() }
			};
			return JsonSerializer.Serialize(animals, options);
		}

		public List<Animal> Deserialize(string data)
		{
			var options = new JsonSerializerOptions
			{
				Converters = { new AnimalConverter() }
			};
			return JsonSerializer.Deserialize<List<Animal>>(data, options) ?? new List<Animal>();
		}

		private class AnimalConverter : JsonConverter<Animal>
		{
			public override bool CanConvert(Type typeToConvert)
			{
				return typeof(Animal).IsAssignableFrom(typeToConvert);
			}

			public override Animal Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
			{
				using var doc = JsonDocument.ParseValue(ref reader);
				var root = doc.RootElement;

				if (!root.TryGetProperty("TypeName", out var typeNameElement))
					throw new JsonException("TypeName property is missing");

				string typeName = typeNameElement.GetString();
				if (!AnimalTypes.TryGetValue(typeName, out Type animalType))
					throw new JsonException($"Unknown animal type: {typeName}");

				var animal = (Animal)Activator.CreateInstance(animalType);

				foreach (var prop in animalType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
				{
					if (root.TryGetProperty(prop.Name, out var propValue))
					{
						try
						{
							var value = JsonSerializer.Deserialize(
								propValue.GetRawText(),
								prop.PropertyType,
								options);

							prop.SetValue(animal, value);
						}
						catch (Exception ex)
						{
							throw new JsonException($"Error setting property {prop.Name}: {ex.Message}");
						}
					}
				}

				return animal;
			}

			public override void Write(Utf8JsonWriter writer, Animal value, JsonSerializerOptions options)
			{
				writer.WriteStartObject();
				writer.WriteString("TypeName", value.GetType().Name);

				foreach (var prop in value.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
				{
					if (prop.CanRead && prop.GetIndexParameters().Length == 0)
					{
						writer.WritePropertyName(prop.Name);
						JsonSerializer.Serialize(writer, prop.GetValue(value), prop.PropertyType, options);
					}
				}

				writer.WriteEndObject();
			}
		}
	}
}