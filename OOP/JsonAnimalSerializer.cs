using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows.Forms; // Add this using directive for MessageBox and Application

namespace OOP
{
	public class JsonAnimalSerializer : ISerializer
	{
		private readonly Form1 _mainForm;

		public JsonAnimalSerializer(Form1 mainForm)
		{
			_mainForm = mainForm;
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
			try
			{
				// Проверяем наличие контрольной суммы
				if (data.Contains("\"Checksum\":"))
				{
					bool checksumPluginActive = false;

					// Проверяем, активен ли плагин Checksum в главной форме
					var mainForm = Application.OpenForms.OfType<Form1>().FirstOrDefault();
					if (mainForm != null)
					{
						checksumPluginActive = mainForm.IsPluginActive("Checksum Validator");
					}

					if (!checksumPluginActive)
					{
						MessageBox.Show("This file contains checksum protection. Please enable the Checksum Validator plugin to load it.",
							"Checksum Protection", MessageBoxButtons.OK, MessageBoxIcon.Error);
						return new List<Animal>();
					}
				}

				var options = new JsonSerializerOptions
				{
					Converters = { new AnimalConverter() }
				};
				return JsonSerializer.Deserialize<List<Animal>>(data, options) ?? new List<Animal>();
			}
			catch (Exception ex)
			{
				MessageBox.Show($"JSON Deserialization Error: {ex.Message}\n\nIs the file corrupted?",
					"Deserialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return new List<Animal>();
			}
		}

		private class AnimalConverter : JsonConverter<Animal>
		{
			// Instead of a static dictionary, we'll get types from AnimalFactory dynamically
			private Dictionary<string, Type> GetAnimalTypes()
			{
				return AnimalFactory.GetAvailableAnimalTypes()
									.Select(typeName => AnimalFactory.GetAnimalType(typeName))
									.Where(type => type != null)
									.ToDictionary(type => type.Name, type => type);
			}

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
				// Get AnimalTypes dynamically for each Read operation
				if (!GetAnimalTypes().TryGetValue(typeName, out Type animalType))
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