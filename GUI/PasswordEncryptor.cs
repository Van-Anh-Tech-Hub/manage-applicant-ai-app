using System;
using System.Security.Cryptography;
using System.Text;

namespace GUI
{
    public class PasswordHasher
    {
        private const char Separator = '$';

        public static string HashPassword(string password)
        {
            var saltBytes = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(saltBytes);
            }
            var salt = Convert.ToBase64String(saltBytes); // Ensure salt is Base64 encoded

            using (var sha256 = SHA256.Create())
            {
                var saltedPassword = Encoding.UTF8.GetBytes(password + salt);
                var hashBytes = sha256.ComputeHash(saltedPassword);
                var hash = Convert.ToBase64String(hashBytes); // Ensure hash is Base64 encoded

                return $"{salt}{Separator}{hash}";
            }
        }

        public static bool VerifyPassword(string password, string storedHash)
        {
            var parts = storedHash.Split(Separator);
            if (parts.Length != 2) return false;

            var salt = parts[0];  // Extract the salt
            var hash = parts[1];   // Extract the hash

            using (var sha256 = SHA256.Create())
            {
                var saltedPassword = Encoding.UTF8.GetBytes(password + salt);
                var inputHashBytes = sha256.ComputeHash(saltedPassword);
                var inputHash = Convert.ToBase64String(inputHashBytes); // Ensure hash is Base64 encoded

                return inputHash == hash; // Compare hashes
            }
        }
    }
}
