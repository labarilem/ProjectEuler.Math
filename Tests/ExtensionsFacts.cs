using System;
using Xunit;
using ProjectEuler.Math;
using System.Numerics;
using System.Collections.Generic;

namespace Tests
{
  public class ExtensionsFacts
  {
    public class BigIntegerExtensions
    {
      public class SqrtMethod
      {
        [Theory]
        [InlineData(-1)]
        [InlineData(-50)]
        public void ThrowOnNegativeInput(int number)
        {
          BigInteger big = (BigInteger)number;
          Exception exception = Record.Exception(() => big.Sqrt());
          Assert.IsType(typeof(ArithmeticException), exception);
        }

        public static IEnumerable<object[]> PerfectData
        {
          get
          {
            return new[]
            {
              new object[] { 4, 2 },
              new object[] { 9, 3 },
              new object[] { 16, 4 },
              new object[] { 25, 5 },
              new object[] { 49, 7 },
              new object[] { 121, 11 },
              new object[] { 12557, 112 }
          };
          }
        }
        [Theory, MemberData(nameof(PerfectData))]
        public void CorrectlyComputeValues(int square, int sqrt)
        {
          BigInteger bigSquare = (BigInteger)square;
          BigInteger bigSqrt = (BigInteger)sqrt;
          BigInteger result = bigSquare.Sqrt();

          Assert.True(result == bigSqrt, String.Format("expected not sqrt({0})={1}", bigSquare, result));
        }

        public static IEnumerable<object[]> ApproxData
        {
          get
          {
            return new[]
            {
              new object[] { 90, 12 },
              new object[] { 543, 542 },
              new object[] { 12321, 1215 }
            };
          }
        }
        [Theory, MemberData(nameof(ApproxData))]
        public void CorrectlyApproximateValues(int greater, int lesser)
        {
          BigInteger bigGreater = ((BigInteger)greater).Sqrt();
          BigInteger bigLesser = ((BigInteger)lesser).Sqrt();

          Assert.True(bigGreater >= bigLesser, String.Format("expected {0} > {1}", bigGreater, bigLesser));
        }
      }
    }
  }
}
