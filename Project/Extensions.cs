using System;
using System.Numerics;

namespace ProjectEuler.Math
{
    public static class Extensions
    {
        /// <summary>
        /// Computes the square root of this number using Newton's algorithm.
        /// </summary>
        public static BigInteger Sqrt(this BigInteger number)
        {
            if (number.Sign < 0)
                throw new ArithmeticException("Value must be >= 0.");

            if (number < 2)
                return number;

            BigInteger squareRoot = number / 2;

            while (true)
            {
                BigInteger lowerBound = BigInteger.Pow(squareRoot, 2);
                BigInteger upperSquareRoot = squareRoot + 1;
                BigInteger upperBound = BigInteger.Pow(upperSquareRoot, 2);

                if (lowerBound == number)
                    return squareRoot;
                else if (upperBound == number)
                    return upperSquareRoot;
                else if ((lowerBound < number) && (number < upperBound))
                    return squareRoot;

                squareRoot += (number / squareRoot);
                squareRoot /= 2;
            }
        }
    }
}