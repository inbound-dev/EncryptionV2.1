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
        //the magic dictionary
        private static Dictionary<char, char> letterMap = new Dictionary<char, char>
        {
            {'A', 'Þ'}, {'B', 'M'}, {'C', 'Q'}, {'D', 'Z'}, {'E', 'L'}, {'F', 'T'}, {'G', 'Y'}, {'H', 'Œ'}, {'I', 'S'}, {'J', 'B'},
            {'K', 'N'}, {'L', 'H'}, {'M', 'E'}, {'N', 'W'}, {'O', 'C'}, {'P', 'U'}, {'Q', 'A'}, {'R', 'K'}, {'S', 'D'}, {'T', 'F'},
            {'U', 'P'}, {'V', 'J'}, {'W', 'G'}, {'X', 'R'}, {'Y', 'V'}, {'Z', 'I'}, {'0', '!'}, {'1', '@'}, {'2', '#'}, {'3', '$'},
            {'4', '%'}, {'5', '^'}, {'6', '&'}, {'7', '*'}, {'8', '('}, {'9', ')'}, {'!', '0'}, {'@', '1'}, {'#', '2'}, {'$', '3'},
            {'%', '4'}, {'^', '5'}, {'&', '6'}, {'*', '7'}, {'(', '8'}, {')', '9'}, {'Á', 'X'}, {'É', 'Ð'}, {'Í', 'Ø'}, {'Ó', 'Æ'},
            {'Ú', 'Ñ'}, {'Ü', 'ß'}, {'Ç', 'Ğ'}, {'Ñ', 'Ş'}, {'Å', 'Œ'}, {'Ø', 'Ž'}, {'?', 'Ğ'}, {'.', 'Œ'}
        };

        public String NewHash(string inputKey)
        {
            string proccessedString = string.Empty;
            string hashedString;

            proccessedString = RepeatOrTrimString(inputKey);
            hashedString = HashGivenString(proccessedString);

            return hashedString;
        }

        private static string HashGivenString(string input)
        {
            string finalProduct = input.ToUpper();

            for(int i = 0; i < 2; i++)
            {
                //everything is out of order, order is, run through dictionary,
                //then convert to ascii, then create binary of that, 
                //xor them togther, invert that binary, convert that into a string,
                //then run that through the dictionary


                //runs the input through the dictionary
                string processedInput = string.Empty;
                foreach (char val in finalProduct)
                {
                    processedInput += EncodeString(val);
                }

                //turns the given string into its ASCII representation
                string numericalRep = string.Empty;
                foreach (char key in processedInput)
                {
                    numericalRep += (int)key;
                }

                //turns given string into its binary representation
                string binaryRep = StringToBinary(numericalRep);

                //inverts the given binary rep
                string invertedBinary = BinaryInverter(binaryRep);

                //peform xor using original and inverted string
                string binaryAfterXor = XorGate(invertedBinary, binaryRep);

                //turns it back into a string for use next round
                finalProduct = BinaryToString(binaryRep);

                Console.WriteLine(invertedBinary.ToString());
                Console.WriteLine("-----------");
                Console.WriteLine(binaryRep.ToString());
                Console.WriteLine("***********");
                Console.WriteLine(binaryAfterXor.ToString());
                Console.WriteLine("//////////////");
                Console.WriteLine(finalProduct);

            }

            return finalProduct;
        }

        private static string StringToBinary(string input)
        {
            string output = string.Empty;

            foreach (char val in input)
            {
                output += AsciiToBinary(val);
            }

            return output;
        }

        static string BinaryToString(string binary)
        {
            if (binary.Length % 8 != 0)
                throw new ArgumentException("Binary string length must be a multiple of 8.");

            StringBuilder textBuilder = new StringBuilder();
            for (int i = 0; i < binary.Length; i += 8)
            {
                string byteString = binary.Substring(i, 8);
                int asciiValue = Convert.ToInt32(byteString, 2);
                textBuilder.Append((char)asciiValue);
            }

            return textBuilder.ToString();
        }
        //Xor gate
        private static string XorGate(string input1, string input2)
        {
            string output = string.Empty;

            for (int i = 0; i < input1.Length; i++)
            { 
                if (input1[i] == '1' && input2[i] == '1')
                {
                    output += '0';
                }
                else if (input1[i] == '0' && input2[i] == '0')
                {
                    output += '0';
                }
                else if (input1[i] == '1' && input2[i] == '0')
                {
                    output += '1';
                }
                else if (input1[i] == '0' && input2[i] == '1')
                {
                    output += '1';
                }
            }

           // Console.WriteLine(output);

            return output;
        }

        //takes a given binary string and inverts it
        private static string BinaryInverter(string input)
        {
            string output = string.Empty;

            foreach (char item in input)
            {
                if (item == '1')
                {
                    output += '0';
                }
                else
                {
                    output += '1';
                }
            }

            return output;
        }

        //converts characters from ASCII to binary
        private static string AsciiToBinary(int ascii)
        {
            return Convert.ToString(ascii, 2).PadLeft(8, '0'); // Converts to binary with 8-bit padding
        }

        //takes the oringnal string and makes it 32 characters long
        private static string RepeatOrTrimString(string input)
        {
            if (string.IsNullOrEmpty(input)) return new string(' ', 32); 

            while (input.Length < 32)
            {
                input += input;
            }

            return input.Substring(0, 32);
        }

        // takes a given character and swaps it with its character from the dictionary
        private static char EncodeString(char input)
        {
            string modInput = input.ToString();
            char result = ' ';
            foreach (char c in modInput)
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
