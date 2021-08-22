using System;
using System.Security.Cryptography;

namespace PasswordGenerator
{
    public class RandomGenerator
    {
        readonly RNGCryptoServiceProvider csp;

        public RandomGenerator()
        {
            csp = new RNGCryptoServiceProvider();
        }

        public int Next(int min, int max)
        {
            if (min >= max)
            {
                throw new ArgumentOutOfRangeException("Min value must be lower than max value!");
            }

            long diff = (long)max - min;
            long upperBound = uint.MaxValue / diff * diff;

            uint ui;

            do
            {
                ui = GetRandomInt();
            } while (ui >= upperBound);

            return (int)(min + (ui % diff));
        }

        private uint GetRandomInt()
        {
            var randomBytes = GenerateRandomBytes(sizeof(uint));
            return BitConverter.ToUInt32(randomBytes, 0);
        }

        private byte[] GenerateRandomBytes(int bytesNumber)
        {
            byte[] buffer = new byte[bytesNumber];
            csp.GetBytes(buffer);

            return buffer;
        }
    }
}

// https://stackoverflow.com/questions/42426420/how-to-generate-a-cryptographically-secure-random-integer-within-a-range
// I know that is a big mistake, but no, I do not understand all of this for now.