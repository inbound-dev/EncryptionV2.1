﻿using System;
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
            string hashedString = HashGivenString(inputKey);

            return hashedString;
        }

        private static string HashGivenString(string input)
        {
            //take the input and pad it
            

            //convert to uppercase
            string finalProduct = input.ToUpper();

            for(int i = 0; i < 3; i++)
            {

                //turns the given string into its ASCII representation
                string numericalRep = string.Empty;
                foreach (char key in finalProduct)
                {
                    numericalRep += (int)key;
                }

                //turns given string into its binary representation
                string binaryRep = StringToBinary(numericalRep);

                //turns it back into a string of ascii numbers
                string asciiString = BinaryToAsciiString(binaryRep);

                //converts the string of ascii numbers into ascii characters
                finalProduct = AsciiToString(asciiString);

                Console.WriteLine("Binary: " + binaryRep.ToString());
                Console.WriteLine("Ascii: " + asciiString + " " + asciiString.Replace(" ", "").Length);
            }

            return finalProduct;
        }

        //turns acsii numbers into ascii characters
        public static string AsciiToString(string input)
        {
            // Split the input string into an array of strings, each representing a decimal value
            string[] decimalValues = input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            // Convert each decimal value to its corresponding ASCII character and join them into a single string
            string asciiString = string.Concat(decimalValues.Select(decimalValue => (char)int.Parse(decimalValue)));

            return asciiString;
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

        static string BinaryToAsciiString(string binary)
        {
            binary = binary.Replace(" ", "");

            if (binary.Length % 8 != 0)
                throw new ArgumentException("Binary string length must be a multiple of 8.");

            StringBuilder textBuilder = new StringBuilder();
            int tracking = 0;
            for (int i = 0; i < binary.Length; i += 8)
            {
                string byteString = binary.Substring(i, 8);
                int asciiValue = Convert.ToInt32(byteString, 2);
                textBuilder.Append((char)asciiValue);
                tracking++;

                if (tracking == 2)
                {
                    textBuilder.Append(' ');
                    tracking = 0;
                }
            }

            return textBuilder.ToString();

        }

        //converts characters from ASCII to binary
        private static string AsciiToBinary(int ascii)
        {
            return Convert.ToString(ascii, 2).PadLeft(8, '0'); // Converts to binary with 8-bit padding
        }

        //takes the oringnal string and makes it 64 characters long
        private static string PadInput(string input)
        {
            string output = string.Empty;

            //turns the given string into its ASCII representation
            string numericalRep = string.Empty;
            foreach (char key in input)
            {
                numericalRep += (int)key;
            }

            //turns given string into its binary representation
            string binaryRep = StringToBinary(numericalRep);

            //captures the length of the message before padding
            int length = binaryRep.Length;

            //does the padding
            binaryRep += '1';
            while (binaryRep.Length < 488) 
            {
                binaryRep += '0';
            }

            //add the length of the message
            int lengthAsAscii = (int)binaryRep.Length;
            binaryRep += StringToBinary(lengthAsAscii.ToString());

            //add spaces every 16 chars so we can use the function I already made
            for (int i = 0; i <= binaryRep.Length; i++)
            {
                if ((i % 16) == 0)
                {
                    binaryRep = binaryRep.Insert(i, " ");
                }
            }

            string binaryOutput = BinaryToAsciiString(binaryRep);
            output = AsciiToString(binaryOutput);

            //Console.WriteLine(binaryRep + " " + binaryRep.Replace(" ", "").Length);
            Console.WriteLine(output);

            return output;
        }
    }
}