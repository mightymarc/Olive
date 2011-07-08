// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Crypto.cs" company="Olive">
//   
// </copyright>
// <summary>
//   Defines the Crypto class.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Olive
{
    using System;
    using System.Diagnostics.Contracts;
    using System.Security.Cryptography;
    using System.Text;

    public interface ICrypto
    {
        /// <summary>
        ///   Creates a salt.
        /// </summary>
        /// <param name = "length">The number of bytes to use in the salt.</param>
        /// <returns>The salt that was created.</returns>
        string CreateSalt(int length);

        /// <summary>
        ///   Creates a salt.
        /// </summary>
        /// <returns>The salt that was created.</returns>
        string CreateSalt();

        /// <summary>
        ///   Generates a hash from the specified password and salt.
        /// </summary>
        /// <param name = "password">The password.</param>
        /// <param name = "salt">The salt to use for hashing.</param>
        /// <returns>The hash that was generated.</returns>
        string GenerateHash(string password, string salt);
    }

    /// <summary>
    ///   Contains helpers to perform hashing.
    /// </summary>
    public class Crypto : ICrypto
    {
        /// <summary>
        ///   Creates a salt.
        /// </summary>
        /// <param name = "length">The number of bytes to use in the salt.</param>
        /// <returns>The salt that was created.</returns>
        public string CreateSalt(int length)
        {
            Contract.Requires<ArgumentException>(length > 0, "length");
            Contract.Ensures(!string.IsNullOrEmpty(Contract.Result<string>()));

            var resultBytes = new byte[length];

            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetNonZeroBytes(resultBytes);
            }

            var result = Convert.ToBase64String(resultBytes);
            Contract.Assume(!string.IsNullOrEmpty(result));

            return result;
        }

        /// <summary>
        ///   Creates a salt.
        /// </summary>
        /// <returns>The salt that was created.</returns>
        public string CreateSalt()
        {
            Contract.Ensures(!string.IsNullOrEmpty(Contract.Result<string>()));

            return this.CreateSalt(128);
        }

        /// <summary>
        ///   Generates a hash from the specified password and salt.
        /// </summary>
        /// <param name = "password">The password.</param>
        /// <param name = "salt">The salt to use for hashing.</param>
        /// <returns>The hash that was generated.</returns>
        public string GenerateHash(string password, string salt)
        {
            using (var hashAlgo = new SHA512Managed())
            {
                return Convert.ToBase64String(hashAlgo.ComputeHash(Encoding.UTF8.GetBytes(password + salt)));
            }
        }
    }
}