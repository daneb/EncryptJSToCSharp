using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace AESEncryption
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine(DecryptStringAES("T7rERZqv4Rh01Ec5lrswHg=="));
            Console.WriteLine(EncryptStringAES("my message"));
        }

        public static string EncryptStringAES(string encryptedValue)
        {
            var keybytes = System.Text.Encoding.UTF8.GetBytes("8056483646328763");
            var iv = Encoding.UTF8.GetBytes("8056483646328763");
            var message = "my message";
            //DECRYPT FROM CRIPTOJS
            var encryptedBytes = EncryptString(message, keybytes, iv);
            var ciphertext = Convert.ToBase64String(encryptedBytes); ;
            return ciphertext;

        }

        public static string DecryptStringAES(string encryptedValue)
        {
            var keybytes = System.Text.Encoding.UTF8.GetBytes("8056483646328763");
            var iv = Encoding.UTF8.GetBytes("8056483646328763");
            //DECRYPT FROM CRIPTOJS
            var encrypted = Convert.FromBase64String(encryptedValue);
            var decryptedFromJavascript = DecryptStringFromBytes(encrypted, keybytes, iv);
            return decryptedFromJavascript;
        }

        public static byte[] EncryptString(string text, byte[] key, byte[] iv)
        {
            // Check arguments.
            if (text == null || text.Length <= 0)
            {
                throw new ArgumentNullException("cipherText");
            }
            if (key == null || key.Length <= 0)
            {
                throw new ArgumentNullException("key");
            }
            if (iv == null || iv.Length <= 0)
            {
                throw new ArgumentNullException("key");
            }
            // Declare the string used to hold
            // the decrypted text.
            byte[] ciphertext = null;
            // Create an RijndaelManaged object
            // with the specified key and IV.
            using (var rijAlg = new RijndaelManaged())
            {
                //Settings
                rijAlg.Mode = CipherMode.CBC;
                rijAlg.Padding = PaddingMode.PKCS7;
                rijAlg.FeedbackSize = 128;
                rijAlg.Key = key;
                rijAlg.IV = iv;
                // Create a decrytor to perform the stream transform.
                var encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);
                // Create the streams used for decryption.
                using (var msEncrypt = new MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (var srEncrypt = new StreamWriter(csEncrypt))
                        {
                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            srEncrypt.Write(text);
                        }
                        ciphertext = msEncrypt.ToArray();

                    }
                }
            }

            return ciphertext;
        }

        public static string DecryptStringFromBytes(byte[] cipherText, byte[] key, byte[] iv)
        {
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
            {
                throw new ArgumentNullException("cipherText");
            }
            if (key == null || key.Length <= 0)
            {
                throw new ArgumentNullException("key");
            }
            if (iv == null || iv.Length <= 0)
            {
                throw new ArgumentNullException("key");
            }
            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;
            // Create an RijndaelManaged object
            // with the specified key and IV.
            using (var rijAlg = new RijndaelManaged())
            {
                //Settings
                rijAlg.Mode = CipherMode.CBC;
                rijAlg.Padding = PaddingMode.PKCS7;
                rijAlg.FeedbackSize = 128;
                rijAlg.Key = key;
                rijAlg.IV = iv;
                // Create a decrytor to perform the stream transform.
                var decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);
                // Create the streams used for decryption.
                using (var msDecrypt = new MemoryStream(cipherText))
                {
                    using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (var srDecrypt = new StreamReader(csDecrypt))
                        {
                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }

            return plaintext;
        }
    }
}
