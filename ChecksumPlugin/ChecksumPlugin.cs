using System.Security.Cryptography;
using System.Text;
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
			return $"{data}\n<!-- CHECKSUM:{hashString} -->";
		}

		public string ProcessAfterLoad(string data)
		{
			try
			{
				int checksumIndex = data.LastIndexOf("\n<!-- CHECKSUM:");
				if (checksumIndex == -1)
				{
					// Если плагин включен, но контрольной суммы нет - ошибка
					throw new Exception("Checksum not found. The file was saved without checksum validation.");
				}

				string originalData = data.Substring(0, checksumIndex);
				string checksumPart = data.Substring(checksumIndex + 15); // Пропускаем "\n<!-- CHECKSUM:"

				// Извлекаем только хеш (первые 64 символа)
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