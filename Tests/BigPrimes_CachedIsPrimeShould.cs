using System;
using Xunit;
using ProjectEuler.Math;
using System.Numerics;

namespace Tests
{
    public class BigPrimesCacheFixture : IDisposable
    {
        public BigPrimesCacheFixture ()
        {
            BigPrimes.InitializeCache(15);
        }

        public void Dispose()
        {
        }
    }

    public class BigPrimes_CachedIsPrimeShould : IClassFixture<PrimesCacheFixture>
    {
        public void SetFixture(PrimesCacheFixture data)
        {
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void ReturnFalseGivenValuesLessThan2(uint number)
        {
            bool result = BigPrimes.IsPrime((BigInteger)number);
            Assert.False(result, number.ToString() + " should not be prime");
        }

        [Theory]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(5)]
        [InlineData(7)]
        [InlineData(11)]
        [InlineData(103)]
        [InlineData(181)]
        [InlineData(67)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(5)]
        [InlineData(7)]
        [InlineData(11)]
        [InlineData(103)]
        [InlineData(181)]
        [InlineData(67)]
        public void RecognizePrimeNumbers(uint number)
        {
            bool result = BigPrimes.IsPrime((BigInteger)number);
            Assert.True(result, number.ToString() + " should be prime");
        }

        [Theory]
        [InlineData(4)]
        [InlineData(6)]
        [InlineData(8)]
        [InlineData(9)]
        [InlineData(100)]
        [InlineData(200)]
        [InlineData(148)]
        [InlineData(4)]
        [InlineData(6)]
        [InlineData(8)]
        [InlineData(9)]
        [InlineData(100)]
        [InlineData(200)]
        [InlineData(148)]
        public void RecognizeCompositeNumbers(uint number)
        {
            bool result = BigPrimes.IsPrime((BigInteger)number);
            Assert.False(result, number.ToString() + " should not be prime");
        }
    }
}
