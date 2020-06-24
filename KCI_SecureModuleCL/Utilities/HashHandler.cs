using System;
using System.Security.Cryptography;
using System.Text;

namespace KCI_SecureModuleCL.Utilities
{
    public class HashHandler
    {
        public static string CreateHash(string Text)
        {
            return CreateHash(Text, CreateSalt());
        }

        private static string CreateHash(string Text, string Salt)
        {
            string SaltAndPwd = String.Concat(Text, Salt);
            string HashedText = GetHashString(SaltAndPwd);
            var SaltPosition = 5;
            HashedText = HashedText.Insert(SaltPosition, Salt);
            return HashedText;
        }

        public static bool Validate(string TextInserted, string TextHashed)
        {
            var saltPosition = 5;
            var saltSize = 10;
            var salt = TextHashed.Substring(saltPosition, saltSize);
            var hashedPassword = CreateHash(TextInserted, salt);
            return hashedPassword == TextHashed;
        }

        private static string CreateSalt()
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[20];
            rng.GetBytes(buff);
            var saltSize = 10;
            string salt = Convert.ToBase64String(buff);
            if (salt.Length > saltSize)
            {
                salt = salt.Substring(0, saltSize);
                return salt.ToUpper();
            }

            var saltChar = '^';
            salt = salt.PadRight(saltSize, saltChar);
            return salt.ToUpper();
        }

        private static string GetHashString(string Text)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHash(Text))
                sb.Append(b.ToString("X2"));
            return sb.ToString();
        }

        private static byte[] GetHash(string Text)
        {
            SHA384 sha = new SHA384CryptoServiceProvider();
            return sha.ComputeHash(Encoding.UTF8.GetBytes(Text));
        }
    }
}
