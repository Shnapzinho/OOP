using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using System.Windows.Forms;
using System.Reflection;

namespace OOP
{
	public class XmlAnimalSerializer : ISerializer
	{
		private readonly Form1 _mainForm;

		public XmlAnimalSerializer(Form1 mainForm)
		{
			_mainForm = mainForm;
		}
		public class AnimalListWrapper
		{
			public List<Animal> Animals { get; set; } = new List<Animal>();
		}

		public string Serialize(List<Animal> animals)
		{
			try
			{
				var wrapper = new AnimalListWrapper { Animals = animals };
				// Передаем актуальные типы каждый раз при сериализации
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
				// Проверяем наличие контрольной суммы
				if (data.Contains("<!-- CHECKSUM:"))
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

		// Этот метод должен быть публичным, чтобы AnimalFactory мог передавать типы
		// Или же AnimalFactory должен иметь способ получать все загруженные сборки,
		// что уже реализовано внутренне в AnimalFactory.GetAvailableAnimalTypes()
		private Type[] GetAnimalTypes()
		{
			// Вместо того чтобы самому сканировать сборки, AnimalFactory уже знает обо всех загруженных типах.
			// Используем его для получения всех известных типов Animal.
			return AnimalFactory.GetAvailableAnimalTypes()
								.Select(typeName => AnimalFactory.GetAnimalType(typeName))
								.Where(type => type != null)
								.ToArray();
		}
	}
}