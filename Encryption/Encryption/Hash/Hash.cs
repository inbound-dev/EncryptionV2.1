using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Encryption.HashFunction
{
    internal class Hash
    {

        public String NewHash(string inputKey)
        {
            string proccessedString = "";
            string hashedString;

            proccessedString = RepeatOrTrimString(inputKey);
            hashedString = HashGivenString(proccessedString);

            return hashedString;
        }

        public static string HashGivenString(string input)
        {
            //turns the given string into its ASCII representation
            string numericalRep = "";
            foreach (char key in input)
            {
                numericalRep += (int)key;
            }

            //turns given string into its binary representation
            string binaryRep = "";
            foreach (char val in numericalRep)
            {
                binaryRep += AsciiToBinary(val);
            }

            return binaryRep;
        }

        //converts characters from ASCII to binary
        public static string AsciiToBinary(int ascii)
        {
            return Convert.ToString(ascii, 2).PadLeft(8, '0'); // Converts to binary with 8-bit padding
        }

        //takes the oringnal string and makes it 32 characters long
        public static string RepeatOrTrimString(string input)
        {
            if (string.IsNullOrEmpty(input)) return new string(' ', 32); // Handle empty input

            while (input.Length < 32)
            {
                input += input; // Repeat the string
            }

            return input.Substring(0, 32); // Trim to 32 characters
        }

    }
}
