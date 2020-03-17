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
	/// <summary>
	/// Utility service for string encryption
	/// </summary>
    public class CrypProvider
    {
		/// <summary>
		/// Password Hash
		/// </summary>
        private readonly string PasswordHash;
		/// <summary>
		/// Salt key
		/// </summary>
        private readonly string SaltKey;
		/// <summary>
		/// VI Key
		/// </summary>
        private readonly string VIKey;
		/// <summary>
		/// Constructor
		/// Should use RSA for encrypt keys like SSL?
		/// </summary>
		/// <param name="settings">The settings where keys are stored</param>
        public CrypProvider(IAppSettings settings) 
		{
			PasswordHash = settings.PasswordHash;
			SaltKey      = settings.SaltKey;
			VIKey        = settings.VIKey;
		}
		/// <summary>
		/// Encrypt a text
		/// </summary>
		/// <param name="plainText">The text in UTF8 Encoding to encrypt</param>
		/// <returns>The encryped text result in ASCII base64 Encoding</returns>
		public string Encrypt(string plainText)
		{
			// Convert our plaintext into a byte array.
			byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
			//Create encryptor using settings data and Managed classes
			byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
			var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.Zeros };
			var encryptor = symmetricKey.CreateEncryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));
			//Result container declaration
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
		/// <summary>
		/// Decrypt a text
		/// </summary>
		/// <param name="encryptedText">Text to decrypt in ASCII base64 string encoding</param>
		/// <returns>The UTF8 encoding decryped string</returns>
		public string Decrypt(string encryptedText)
		{
			// Convert our ciphertext into a byte array.
            byte[] cipherTextBytes = Convert.FromBase64String(encryptedText);
			//Create decryptor using settings data and Managed classes
			byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
			var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.None };
			var decryptor = symmetricKey.CreateDecryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));
			var memoryStream = new MemoryStream(cipherTextBytes);
			var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
			//Target container
			byte[] plainTextBytes = new byte[cipherTextBytes.Length];
			// Start decrypting.
            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
			// Close both streams.
            memoryStream.Close();
			cryptoStream.Close();
			// Convert decryped data to UTF8 encoding string
			return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount).TrimEnd("\0".ToCharArray());
		}
	}
}
