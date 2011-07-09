namespace Olive
{
    /// <summary>
    ///   Defines helper methods to perform hashing.
    /// </summary>
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
}