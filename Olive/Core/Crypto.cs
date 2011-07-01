// -----------------------------------------------------------------------
// <copyright file="Crypto.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Olive
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Crypto
    {
        public static string CreateSalt(int byteCount = 128)
        {
            Contract.Requires<ArgumentException>(byteCount > 0, "byteCount");
            Contract.Ensures(!string.IsNullOrEmpty(Contract.Result<string>()));

            var resultBytes = new byte[byteCount];

            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetNonZeroBytes(resultBytes);
            }

            return Convert.ToBase64String(resultBytes);
        }

        public static string GetHash(string password, string salt)
        {
            using (var hashAlgo = new SHA512Managed())
            {
                return Convert.ToBase64String(hashAlgo.ComputeHash(Encoding.UTF8.GetBytes(password + salt)));
            }
        }
    }
}
