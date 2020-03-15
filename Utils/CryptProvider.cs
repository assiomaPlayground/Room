using RoomService.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RoomService.Utils
{
    public class CrypProvider
    {
        private readonly string PasswordHash;
        private readonly string SaltKey;
        private readonly string VIKey;
        public CrypProvider(IAppSettings settings) 
		{
			PasswordHash = settings.PasswordHash;
			SaltKey      = settings.SaltKey;
			VIKey        = settings.VIKey;
		}


		public string Encrypt(string plainText)
		{

			// Convert our plaintext into a byte array.
			// Let us assume that plaintext contains UTF8-encoded characters
			byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);



			byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
			var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.Zeros };
			var encryptor = symmetricKey.CreateEncryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));

			byte[] cipherTextBytes;

			// Define memory stream which will be used to hold encrypted data.
			using (var memoryStream = new MemoryStream())
			{
				// Define cryptographic stream (always use Write mode for encryption)
				using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
				{
					// Start encrypting.
					cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);

					// Finish encrypting.
					cryptoStream.FlushFinalBlock();
					// Convert our encrypted data from a memory stream into a byte array.

                    cipherTextBytes = memoryStream.ToArray();

					// Close both streams

					cryptoStream.Close();
				}
				memoryStream.Close();
			}

			// Convert encrypted data into a base64-encoded string.

			return Convert.ToBase64String(cipherTextBytes);
		}

		public string Decrypt(string encryptedText)
		{

			// Convert our ciphertext into a byte array.
            byte[] cipherTextBytes = Convert.FromBase64String(encryptedText);

			byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
			var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.None };

			var decryptor = symmetricKey.CreateDecryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));
			var memoryStream = new MemoryStream(cipherTextBytes);
			var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
			byte[] plainTextBytes = new byte[cipherTextBytes.Length];

			// Start decrypting.
            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
			
			// Close both streams.
            memoryStream.Close();
			cryptoStream.Close();
			return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount).TrimEnd("\0".ToCharArray());
		}
	}
}
