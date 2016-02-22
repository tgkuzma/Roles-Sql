using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using Business.Interfaces;

namespace Business
{
    public class HashManager : IHashManager
    {
        public const int SALT_BYTE_SIZE = 24;
        public const int HASH_BYTE_SIZE = 24;
        public const int PBKDF2_ITERATIONS = 1000;

        public const int ITERATION_INDEX = 0;
        public const int SALT_INDEX = 1;
        public const int PBKDF2_INDEX = 2;

        public string CreateHashFromString(string password)
        {
            var cryptoServiceProvider = new RNGCryptoServiceProvider();
            var salt = new byte[SALT_BYTE_SIZE];
            cryptoServiceProvider.GetBytes(salt);

            var hash = PBKDF2(password, salt, PBKDF2_ITERATIONS, HASH_BYTE_SIZE);
            return PBKDF2_ITERATIONS + ":" +
                Convert.ToBase64String(salt) + ":" +
                Convert.ToBase64String(hash);
        }

        public bool ValidateHash(string password, string correctHash)
        {
            char[] delimiter = { ':' };
            var split = correctHash.Split(delimiter);
            var iterations = int.Parse(split[ITERATION_INDEX]);
            var salt = Convert.FromBase64String(split[SALT_INDEX]);
            var hash = Convert.FromBase64String(split[PBKDF2_INDEX]);

            var testHash = PBKDF2(password, salt, iterations, hash.Length);
            return SlowEquals(hash, testHash);
        }

        private static bool SlowEquals(IReadOnlyList<byte> hash, IReadOnlyList<byte> testHash)
        {
            var diff = (uint)hash.Count ^ (uint)testHash.Count;
            for (var i = 0; i < hash.Count && i < testHash.Count; i++)
                diff |= (uint)(hash[i] ^ testHash[i]);
            return diff == 0;
        }

        private static byte[] PBKDF2(string password, byte[] salt, int iterations, int outputBytes)
        {
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt) {IterationCount = iterations};
            return pbkdf2.GetBytes(outputBytes);
        }
    }
}