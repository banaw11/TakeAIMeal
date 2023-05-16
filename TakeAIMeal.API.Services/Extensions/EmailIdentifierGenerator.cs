using System.Security.Cryptography;
using System.Text;

namespace TakeAIMeal.API.Services.Extensions
{
    /// <summary>
    /// Provides extension methods for generating unique identifiers based on email addresses.
    /// </summary>
    public static class EmailIdentifierGenerator
    {
        /// <summary>
        /// Generates a unique identifier based on the specified <paramref name="emailAddress"/>.
        /// </summary>
        /// <param name="emailAddress">The email address to generate the identifier from.</param>
        /// <returns>A unique identifier derived from the email address.</returns>
        public static string GenerateIdentifier(string emailAddress)
        {
            using (var md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(emailAddress);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder stringBuilder = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    stringBuilder.Append(hashBytes[i].ToString("X2"));
                }

                return stringBuilder.ToString();
            }
        }
    }
}
