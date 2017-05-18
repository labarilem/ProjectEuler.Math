using System;
using Xunit;
using ProjectEuler.Math;

namespace Tests
{
    public class Primes_IsPrimeShould
    {
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void ReturnFalseGivenValuesLessThan2(ulong number)
        {
            bool result = Primes.IsPrime(number);
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
        public void RecognizePrimeNumbers(ulong number)
        {
            bool result = Primes.IsPrime(number);
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
        public void RecognizeCompositeNumbers(ulong number)
        {
            bool result = Primes.IsPrime(number);
            Assert.False(result, number.ToString() + "should not be prime");
        }
    }
}
