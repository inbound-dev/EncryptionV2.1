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

        private static Dictionary<char, char> letterMap = new Dictionary<char, char>
        {
            {'A', 'X'}, {'B', 'M'}, {'C', 'Q'}, {'D', 'Z'}, {'E', 'L'}, {'F', 'T'}, {'G', 'Y'}, {'H', 'O'}, {'I', 'S'}, {'J', 'B'},
            {'K', 'N'}, {'L', 'H'}, {'M', 'E'}, {'N', 'W'}, {'O', 'C'}, {'P', 'U'}, {'Q', 'A'}, {'R', 'K'}, {'S', 'D'}, {'T', 'F'},
            {'U', 'P'}, {'V', 'J'}, {'W', 'G'}, {'X', 'R'}, {'Y', 'V'}, {'Z', 'I'},
            {'0', '!'}, {'1', '@'}, {'2', '#'}, {'3', '$'}, {'4', '%'}, {'5', '^'}, {'6', '&'}, {'7', '*'}, {'8', '('}, {'9', ')'},
            {'!', '0'}, {'@', '1'}, {'#', '2'}, {'$', '3'}, {'%', '4'}, {'^', '5'}, {'&', '6'}, {'*', '7'}, {'(', '8'}, {')', '9'}
        };

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
            string finalProduct = "";

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

            //

            return finalProduct;
        }

        //converts characters from ASCII to binary
        public static string AsciiToBinary(int ascii)
        {
            return Convert.ToString(ascii, 2).PadLeft(8, '0'); // Converts to binary with 8-bit padding
        }

        //takes the oringnal string and makes it 32 characters long
        public static string RepeatOrTrimString(string input)
        {
            if (string.IsNullOrEmpty(input)) return new string(' ', 32); 

            while (input.Length < 32)
            {
                input += input;
            }

            return input.Substring(0, 32);
        }

        // takes a given character and swaps it with its character from the dictionary
        static string EncodeString(string input)
        {
            string result = "";
            foreach (char c in input)
            {
                if (letterMap.ContainsKey(c))
                    result += letterMap[c];
                else
                    result += c;
            }
            return result;
        }

    }
}
