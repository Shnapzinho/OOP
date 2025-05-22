using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Windows.Forms;
using OOP;

namespace ChecksumPlugin
{
	public class ChecksumPlugin : IDataProcessorPlugin
	{
		public string Name => "Checksum Validator";

		public string ProcessBeforeSave(string data)
		{
			using var sha256 = SHA256.Create();
			byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(data));
			string hashString = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();

			// Для JSON
			if (data.TrimStart().StartsWith("{"))
			{
				var jsonDoc = JsonDocument.Parse(data);
				using var stream = new MemoryStream();
				using (var writer = new Utf8JsonWriter(stream, new JsonWriterOptions { Indented = true }))
				{
					writer.WriteStartObject();
					writer.WritePropertyName("Checksum");
					writer.WriteStringValue(hashString);
					writer.WritePropertyName("Data");
					jsonDoc.RootElement.WriteTo(writer);
					writer.WriteEndObject();
				}
				return Encoding.UTF8.GetString(stream.ToArray());
			}
			// Для XML
			else
			{
				return $"{data}\n<!-- CHECKSUM:{hashString} -->";
			}
		}

		public string ProcessAfterLoad(string data)
		{
			try
			{
				if (data.TrimStart().StartsWith("{"))
				{
					var jsonDoc = JsonDocument.Parse(data);
					if (jsonDoc.RootElement.TryGetProperty("Checksum", out var checksumProp))
					{
						string storedChecksum = checksumProp.GetString();
						if (jsonDoc.RootElement.TryGetProperty("Data", out var dataProp))
						{
							string originalData = dataProp.GetRawText();

							using var sha256 = SHA256.Create();
							byte[] currentHashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(originalData));
							string currentHashString = BitConverter.ToString(currentHashBytes).Replace("-", "").ToLower();

							if (currentHashString != storedChecksum)
							{
								throw new Exception("Checksum verification failed! The file may have been corrupted.");
							}

							return originalData;
						}
						else
						{
							// Если нет свойства Data – считаем, что файл некорректен
							throw new Exception("The JSON file is missing the required 'Data' property.");
						}
					}
					else
					{
						// Если плагин должен требовать контрольную сумму, выбрасываем ошибку
						throw new Exception("Checksum protection is enabled, but the file does not contain a checksum.");
					}
				}
				else
				{
					// XML branch
					int checksumIndex = data.LastIndexOf("\n<!-- CHECKSUM:");
					if (checksumIndex == -1)
					{
						// Если плагин должен требовать контрольную сумму, выбрасываем ошибку
						throw new Exception("Checksum protection is enabled, but the file does not contain a checksum.");
					}

					string originalData = data.Substring(0, checksumIndex);
					string checksumPart = data.Substring(checksumIndex + 15);

					if (checksumPart.Length < 64)
					{
						throw new Exception("Invalid checksum format.");
					}

					string storedChecksum = checksumPart.Substring(0, 64);
					using var sha256 = SHA256.Create();
					byte[] currentHashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(originalData));
					string currentHashString = BitConverter.ToString(currentHashBytes).Replace("-", "").ToLower();

					if (currentHashString != storedChecksum)
					{
						throw new Exception("Checksum verification failed! The file may have been corrupted.");
					}

					return originalData;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Error: {ex.Message}\n\nData loading aborted.",
								  "Checksum Validation Failed",
								  MessageBoxButtons.OK,
								  MessageBoxIcon.Error);
				return null;
			}
		}
	}
}