using System;
using System.Numerics;
using System.Collections;

namespace ProjectEuler.Math
{
    public static class BigPrimes
    {
        private static BigInteger[] _cache;
        private static uint _cacheIndex;
        private static uint _cacheSize;

        /// <summary>
        /// Initializes a new cache with the specified cache size.
        /// </summary>
        public static void InitializeCache(uint cacheSize)
        {
            if(cacheSize <= 0)
                throw new ArgumentOutOfRangeException("cacheSize must be > 0.");
            _cache = new BigInteger[cacheSize];
            _cacheSize = cacheSize;
            _cacheIndex = 0;
        }

        /// <summary>
        /// Checks if the specified number is prime without caching known primes.
        /// </summary>
        public static bool IsPrime(BigInteger number)
        {
            return SqrSieve(number);
        }

        /// <summary>
        /// Checks if the specified number is prime, caching known primes.
        /// Call InitializeCache before calling this function.
        /// </summary>
        public static bool CachedIsPrime(BigInteger number)
        {
            if(Array.BinarySearch(_cache, number) > 0)
            {
                return true;
            }
            else
            {
                bool isPrime = SqrSieve(number);
                if(isPrime && _cacheIndex < _cacheSize)
                {
                    _cache[_cacheIndex] = number;
                    Array.Sort(_cache);
                    _cacheIndex++;
                }
                return isPrime;
            }
        }

        private static bool SqrSieve(BigInteger number)
        {
            if (number <= 1) return false;
            if (number < 4) return true;
            if ((number % 2) == 0) return false;
            if (number < 9) return true;
            if ((number % 3) == 0) return false;

            BigInteger boundary = number.Sqrt();
            BigInteger i = 5;
            while (i <= boundary)
            {
                if ((number % i) == 0)  return false;
                if ((number % (i + 2)) == 0) return false;
                i += + 6;
            }

            return true;
        }
    }

    public static class Primes
    {
        private static ulong[] _cache;
        private static ulong _cacheIndex;
        private static ulong _cacheSize;

        /// <summary>
        /// Initializes a new cache with the specified cache size.
        /// </summary>
        public static void InitializeCache(ulong cacheSize)
        {
            if(cacheSize <= 0)
                throw new ArgumentOutOfRangeException("cacheSize must be > 0.");
            _cache = new ulong[cacheSize];
            _cacheSize = cacheSize;
            _cacheIndex = 0;
        }

        /// <summary>
        /// Checks if the specified number is prime, caching known primes.
        /// </summary>
        public static bool IsPrime(ulong number)
        {
            return SqrSieve(number);
        }

        /// <summary>
        /// Checks if the specified number is prime, caching known primes.
        /// Call InitializeCache before calling this function.
        /// </summary>
        public static bool CachedIsPrime(ulong number)
        {
            if(Array.BinarySearch(_cache, number) > 0)
            {
                return true;
            }
            else
            {
                bool isPrime = SqrSieve(number);
                if(isPrime && _cacheIndex < _cacheSize)
                {
                    _cache[_cacheIndex] = number;
                    Array.Sort(_cache);
                    _cacheIndex++;
                }
                return isPrime;
            }
        }

        private static bool SqrSieve(ulong number)
        {
            if (number <= 1) return false;
            if (number < 4) return true;
            if ((number % 2) == 0) return false;
            if (number < 9) return true;
            if ((number % 3) == 0) return false;

            ulong boundary = (ulong)System.Math.Floor(System.Math.Sqrt(number));
            ulong i = 5;
            while (i <= boundary)
            {
                if ((number % i) == 0)  return false;
                if ((number % (i + 2)) == 0) return false;
                i += + 6;
            }

            return true;
        }
    }

}
