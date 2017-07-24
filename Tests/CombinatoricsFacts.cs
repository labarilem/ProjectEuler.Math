using System;
using Xunit;
using ProjectEuler.Math;

namespace Tests
{
  public class CombinatoricsFacts
  {
    public class IsPalindromicMethod
    {
      [Fact]
      public void ReturnsTrueOnEmptyString()
      {
        bool result = Combinatorics.IsPalindromic("");
        Assert.True(result, "'' should be palindromic");
      }

      [Theory]
      [InlineData("1")]
      [InlineData("aa")]
      [InlineData("aba")]
      [InlineData("itopinonavevanonipoti")]
      [InlineData("itopinonavevanonipotiitopinonavevanonipoti")]
      [InlineData("1212itopinonavevanonipoti2121")]
      public void RecognizesPalindromes(string s)
      {
        bool result = Combinatorics.IsPalindromic(s);
        Assert.True(result, s + " should be palindromic");
      }

      [Theory]
      [InlineData("12")]
      [InlineData("123")]
      [InlineData("iTopiNonAvevanoNipoti12")]
      [InlineData("12iTopiNonAvevanoNipoti")]
      public void RecognizesNonPalindromicStrings(string s)
      {
        bool result = Combinatorics.IsPalindromic(s);
        Assert.False(result, s + " should be palindromic");
      }
    }
  }

}
