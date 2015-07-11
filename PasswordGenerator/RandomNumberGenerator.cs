namespace PasswordRandomGenerator
{
    using System;
    using System.Security.Cryptography;

    /// <summary>
    /// A class which provides a method for getting a random number.
    /// </summary>
    public static class RandomNumberGenerator
    {
        private static readonly RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();

        /// <summary>
        /// The method generates a random number depending the given range size.
        /// </summary>
        /// <param name="minSize">A minimum size for the generated number.</param>
        /// <param name="maxSize">A maximum size for the generated number.</param>
        /// <returns>An integer random number.</returns>
        public static int GetRandomNumber(int minSize, int maxSize)
        {
            byte[] randomBytes = new byte[4];

            rng.GetBytes(randomBytes);

            // Conver 4 bytes into a 32-bit integer value
            int seed = BitConverter.ToInt32(randomBytes, 0);

            Random random = new Random(seed);

            int randomValue = 0;
            randomValue = random.Next(minSize, maxSize);

            return randomValue;
        }
    }
}
