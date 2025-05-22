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
			string? originalData = null;
			try
			{
				int checksumIndex = data.LastIndexOf("\n<!-- CHECKSUM:");
				if (checksumIndex == -1) return data;

				originalData = data.Substring(0, checksumIndex);
				string storedChecksum = data.Substring(checksumIndex + 15, 64);

				using var sha256 = SHA256.Create();
				byte[] currentHashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(originalData));
				string currentHashString = BitConverter.ToString(currentHashBytes).Replace("-", "").ToLower();

				if (currentHashString != storedChecksum)
				{
					throw new Exception("Data checksum verification failed! The file may have been uncorrect.");
				}
			}
			catch (Exception ex) {
				MessageBox.Show($"Warning: {ex.Message}",
						"Checksum Mismatch", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
			return originalData;
		}
	}
}