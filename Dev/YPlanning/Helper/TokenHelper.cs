using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace YPlanning.Helper
{
    public static class TokenHelper
    {
        private static readonly byte[] KEY;
        private static readonly byte[] IV;

        static TokenHelper()
        {
            string? keyBase64 = Environment.GetEnvironmentVariable("AES_KEY");
            string? ivBase64 = Environment.GetEnvironmentVariable("AES_IV");

            if (string.IsNullOrEmpty(keyBase64) || string.IsNullOrEmpty(ivBase64))
                throw new InvalidOperationException("Environment variables AES_KEY and AES_IV must be set.");

            KEY = Convert.FromBase64String(keyBase64);
            IV = Convert.FromBase64String(ivBase64);

            if (KEY.Length != 32 || IV.Length != 16)
                throw new InvalidOperationException("Invalid KEY or IV length. KEY must be 32 bytes and IV must be 16 bytes.");
        }

        public static string GenerateSimpleToken(int length)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string EncryptToken(string token)
        {
            using (var aes = Aes.Create())
            {
                aes.Key = KEY;
                aes.IV = IV;
                aes.Padding = PaddingMode.PKCS7;

                using (var encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
                {
                    byte[] tokenBytes = Encoding.UTF8.GetBytes(token);
                    using (var ms = new MemoryStream())
                    {
                        using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                        {
                            cs.Write(tokenBytes, 0, tokenBytes.Length);
                            cs.FlushFinalBlock();
                        }
                        return Convert.ToBase64String(ms.ToArray());
                    }
                }
            }
        }

        public static string DecryptToken(string encryptedToken)
        {
            using (var aes = Aes.Create())
            {
                aes.Key = KEY;
                aes.IV = IV;
                aes.Padding = PaddingMode.PKCS7;

                using (var decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
                {
                    byte[] encryptedTokenBytes = Convert.FromBase64String(encryptedToken);
                    using (var ms = new MemoryStream())
                    {
                        using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Write))
                        {
                            cs.Write(encryptedTokenBytes, 0, encryptedTokenBytes.Length);
                            cs.FlushFinalBlock();
                        }
                        return Encoding.UTF8.GetString(ms.ToArray());
                    }
                }
            }
        }
    }
}
