using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace Auth.Utils
{
    public class PasswordHasher
    {
        public static (string hash, string salt) HashPassword(string password)
        {
            // Генерация соли
            byte[] saltBytes = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(saltBytes);
            }

            string salt = Convert.ToBase64String(saltBytes);

            // Хеширование пароля с солью
            string hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: saltBytes,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 32));

            return (hash, salt);
        }

        public static bool VerifyPassword(string password, string hash, string salt)
        {
            // Преобразование соли обратно в байтовый массив
            byte[] saltBytes = Convert.FromBase64String(salt);

            // Хеширование пароля с полученной солью
            string hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: saltBytes,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 32));

            // Сравнение хеша
            return hash == hashedPassword;
        }
    }
}
