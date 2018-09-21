﻿using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace Rhisis.Core.Cryptography
{
    public static class Rijndael
    {
        /// <summary>
        /// Decrypt data with a key.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string DecryptData(byte[] data, byte[] key)
        {
            string decrypted = string.Empty;

            using (var aes = Aes.Create())
            {
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.Zeros;
                aes.Key = key;
                aes.IV = Enumerable.Repeat<byte>(0, 16).ToArray();

                var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (var memoryStream = new MemoryStream(data))
                using (var crypto = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                using (StreamReader sr = new StreamReader(crypto))
                    decrypted = sr.ReadToEnd();
            }

            return decrypted;
        }
    }
}
