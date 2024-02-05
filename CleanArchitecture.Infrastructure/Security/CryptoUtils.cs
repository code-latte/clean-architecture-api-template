using CleanArchitecture.Domain.Interfaces.Configuration;
using CleanArchitecture.Domain.Interfaces.Security;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace CleanArchitecture.Infrastructure.Security
{
    public class CryptoUtils : ICryptoUtils
    {
        private ICryptoConfiguration _cryptoConfiguration;

        public CryptoUtils(ICryptoConfiguration cryptoConfiguration)
        {
            _cryptoConfiguration = cryptoConfiguration;
        }

        public string Encrypt(string plainText)
        {
            byte[] encrypted;
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(_cryptoConfiguration.AesKey);

                // get the first 16 bytes of the key
                byte[] iv = new byte[16];
                for (int i = 0; i < 16; i++)
                {
                    iv[i] = aesAlg.Key[i];
                }

                aesAlg.IV = iv;
                aesAlg.Mode = CipherMode.CBC;
                aesAlg.Padding = PaddingMode.PKCS7;
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (
                        CryptoStream csEncrypt = new CryptoStream(
                            msEncrypt,
                            encryptor,
                            CryptoStreamMode.Write
                        )
                    )
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            // return bytes to url encoded string
            return HttpUtility.UrlEncode(Convert.ToBase64String(encrypted));
        }

        // decrypt a string using AES
        public string Decrypt(string cipherText)
        {
            try
            {
                string plaintext = null;
                byte[] cipherBytes = Convert.FromBase64String(HttpUtility.UrlDecode(cipherText));
                using (Aes aesAlg = Aes.Create())
                {
                    aesAlg.Key = Encoding.UTF8.GetBytes(_cryptoConfiguration.AesKey);

                    // get the first 16 bytes of the key
                    byte[] iv = new byte[16];
                    for (int i = 0; i < 16; i++)
                    {
                        iv[i] = aesAlg.Key[i];
                    }

                    aesAlg.IV = iv;
                    aesAlg.Mode = CipherMode.CBC;
                    aesAlg.Padding = PaddingMode.PKCS7;
                    ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                    using (MemoryStream msDecrypt = new MemoryStream(cipherBytes))
                    {
                        using (
                            CryptoStream csDecrypt = new CryptoStream(
                                msDecrypt,
                                decryptor,
                                CryptoStreamMode.Read
                            )
                        )
                        {
                            using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                            {
                                plaintext = srDecrypt.ReadToEnd();
                            }
                        }
                    }
                    return plaintext;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public string HashPassword(string password)
        {
            StringBuilder sb = new StringBuilder();
            using (HashAlgorithm algorithm = SHA256.Create())
            {
                byte[] bytes = algorithm.ComputeHash(Encoding.UTF8.GetBytes(password));

                foreach (byte b in bytes)
                    sb.Append(b.ToString("X2"));
            }

            return sb.ToString();
        }

        public bool VerifyPassword(string password, string passwordHash)
        {
            return HashPassword(password).ToUpper() == passwordHash.ToUpper();
        }
    }
}
