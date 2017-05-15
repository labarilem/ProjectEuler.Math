namespace ProjectEuler.Math
{
    public static class Combinatorics
    {
        /// <summary>
        /// Checks if the specified string is palindromic.
        /// </summary>
        public static bool IsPalindromic(string s)
        {
            for(int i = 0; i < s.Length / 2; i++)
            {
                if(s[i] != s[s.Length - 1 - i])
                    return false;
            }
            return true;
        }
    }
}