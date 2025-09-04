using System.Text;
using System;

namespace eGift.Store.Razor.Utility
{
    public static class RandomNumberOrString
    {
        #region Variables
        // Instantiate random number generator.  
        private static readonly Random random = new Random();
        #endregion

        #region Auto Generate Functions
        // Generates a random number within a range.      
        public static int RandomNumber(int min, int max)
        {
            return random.Next(min, max);
        }

        // Generates a 4 digit random number text.      
        public static string FourDigitRandomNumber()
        {
            int min = 1;
            int max = 9999;
            return String.Format("{0:0000}", random.Next(min, max));
        }

        // Generates a random string with a given size.    
        public static string RandomString(int size, bool lowerCase = false)
        {
            var builder = new StringBuilder(size);

            // Unicode/ASCII Letters are divided into two blocks
            // (Letters 65–90 / 97–122):
            // The first group containing the uppercase letters and
            // the second group containing the lowercase.  

            // char is a single Unicode character  
            char offset = lowerCase ? 'a' : 'A';
            const int lettersOffset = 26; // A...Z or a..z: length=26  

            for (var i = 0; i < size; i++)
            {
                var @char = (char)random.Next(offset, offset + lettersOffset);
                builder.Append(@char);
            }

            return lowerCase ? builder.ToString().ToLower() : builder.ToString();
        }
        #endregion
    }
}
