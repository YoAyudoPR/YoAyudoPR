using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Security.Cryptography;

namespace YoAyudoPR.Web.Domain.Security
{
    public class PasswordSecurity
    {
        public string CreateSalt()
        {
            byte[] randomBytes = new byte[128 / 8];
            using (var generator = RandomNumberGenerator.Create())
            {
                generator.GetBytes(randomBytes);
                return Convert.ToBase64String(randomBytes);
            }
        }

        /// <summary>
        /// Creates the Password Hash for the password.
        /// </summary>
        /// <param name="pwd">Password from User</param>
        /// <param name="salt">Salt created by the buff</param>
        /// <returns></returns>
        public string CreatePasswordHash(string pwd, string salt)
        {
            byte[] byteSalt = Convert.FromBase64String(salt);
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: pwd,
                salt: byteSalt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
            return hashed;
        }
    }
}
