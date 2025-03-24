using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Drawing.Text;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace Encryption.HashFunction
{
    internal class Hash
    {
        //message is global as it is used in multiple places
        private static List<String> message = new List<String>();
        public String NewHash(string inputKey)
        {
            List<String> wordSchedulde = new List<String>();
            List<String> initHVals = SetHVals();
            List<String> initKVals = SetKVals();

            //turns the given string into its ASCII representation
            string numericalRep = string.Empty;
            foreach (char key in inputKey)
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

            //add spaces every 32 chars
            for (int i = 0; i <= binaryRep.Length; i++)
            {
                if ((i % 32) == 0)
                {
                    binaryRep = binaryRep.Insert(i, " ");
                    message.Add(binaryRep);
                }
            }

            //foreach (var item in )
            //{
                
            //}



            //takes the message list and returns the wordSchedule list
            wordSchedulde = CreateWordSchedule(message);



            return binaryRep;
        }

        //create word schedule
        static List<String> CreateWordSchedule(List<String> input)
        { 
            List<String> result = new List<String>();

            //adds the first 16 words to the schedule
            foreach (var item in input)
            {
                result.Add(item);
                Console.WriteLine("Word: " + item.Length + " " + item);
            }

            //create the rest of the words in the schedule
            int currentPos = result.Count;
            while (currentPos < 24)
            {

                Console.WriteLine("wordlist length: " + result.Count);
                Console.WriteLine("current pos: " + currentPos);

                //formula for each word: w(t) = sigmaOne(w(t-2)) + w(t-7) + SigmaZero(w(t-15)) + w(t-16)
                string currWord = SigmaOne(result[currentPos - 2]) + result[currentPos-7] + SigmaZero(result[currentPos-15]) + result[currentPos-16];

                result.Add(currWord);
                Console.WriteLine(currWord);
                Console.WriteLine(currWord.Length);

                currentPos++;
            }

            return result;
        }

        static string SigmaOne(string input)
        {
            string output = "";

            //right rotate 17
            string stage1 = RotateBinaryString(input, 17);

            //right rotate 19
            string stage2 = RotateBinaryString(stage1, 19);

            //right shift 10
            string stage3 = RightShiftBinaryString(stage2, 10);

            //xor the result of all 3 operations
            output = TripleInputXorGate(stage1, stage2, stage3);

            return output;
        }

        static string SigmaZero(string input)
        {
            string output = "";

            //right rotate 7
            string stage1 = RotateBinaryString(input, 7);

            //right rotate 18
            string stage2 = RotateBinaryString(stage1, 18);

            //right shift 3
            string stage3 = RightShiftBinaryString(stage2, 3);

            //xor the result of all 3 operations
            output = TripleInputXorGate(stage1, stage2, stage3);

            return output;
        }

        //shifts any given binary string to the right
        static string RightShiftBinaryString(string binary, int shiftAmount)
        {
            int length = binary.Length;
            shiftAmount %= length; // Ensure shift doesn't exceed the string length

            string shifted = new string('0', shiftAmount) + binary.Substring(0, length - shiftAmount);
            return shifted;
        }

        //takes given binary string and rotates it
        static string RotateBinaryString(string binary, int shift)
        {
            int length = binary.Length;
            shift %= length; // Ensure shift is within bounds
            return binary.Substring(shift) + binary.Substring(0, shift);
        }

        //takes three strings of binary and xors them
        static string TripleInputXorGate(string input1, string input2, string input3)
        {
            string output = "";
             
            for(int i = 0; i < input1.Length; i++)
            {
                if (input1[i] == '0' && input2[i] == '0' && input3[3] == '0')
                {
                    output += "0";
                }
                if (input1[i] == '0' && input2[i] == '0' && input3[3] == '1')
                {
                    output += "1";
                }
                if (input1[i] == '0' && input2[i] == '1' && input3[3] == '0')
                {
                    output += "1";
                }
                if (input1[i] == '1' && input2[i] == '0' && input3[3] == '0')
                {
                    output += "1";
                }
                if (input1[i] == '0' && input2[i] == '1' && input3[3] == '1')
                {
                    output += "0";
                }
                if (input1[i] == '1' && input2[i] == '1' && input3[3] == '0')
                {
                    output += "0";
                }
                if (input1[i] == '1' && input2[i] == '0' && input3[3] == '1')
                {
                    output += "0";
                }
                if (input1[i] == '1' && input2[i] == '1' && input3[3] == '1')
                {
                    output += "1";
                }
                else
                {
                    output += "";
                }
            }

            return output;
        }

        //returns the K val for the first 64 prime numbers
        static List<String> SetKVals()
        {

            //first get the first 8 prime numbers
            int count = 0;
            Int16 num = 2;
            List<Int16> primeNums = new List<Int16>();

            while (count < 64)
            {
                if (IsPrime(num))
                {
                    primeNums.Add(num);
                    count++;
                }
                num++;
            }

            //then get their cubed root
            List<double> sqrts = new List<double>();

            foreach (var item in primeNums)
            {
                sqrts.Add(GetCubeRootFractionalPart(item));
            }

            //then convert only the fractional part to binary
            List<String> binary = new List<String>();

            foreach (var item in sqrts)
            {
                binary.Add(ConvertFractionToBinary(item));
            }

            //then convert all that binary to hex for the final words
            List<String> outputWords = new List<String>();
            foreach (var item in binary)
            {
                outputWords.Add(BinaryToHex(item));
            }

            return outputWords;
        }

       //returns the H val for the first 8 prime numbers
       static List<String> SetHVals()
       {

            //first get the first 8 prime numbers
            int count = 0;
            Int16 num = 2;
            List<Int16> primeNums = new List<Int16>();

            while (count < 8)
            {
                if (IsPrime(num))
                {
                    primeNums.Add(num);
                    count++;
                }
                num++;
            }

            //then get their square root
            List<double> sqrts = new List<double>();

            foreach (var item in primeNums)
            {
                sqrts.Add(GetSquareRootFractionalPart(item));
            }

            //then convert only the fractional part to binary
            List<String> binary = new List<String>();

            foreach (var item in sqrts)
            {
                binary.Add(ConvertFractionToBinary(item));
            }

            //then convert all that binary to hex for the final words
            List<String> outputWords = new List<String>();
            foreach (var item in binary)
            {
                outputWords.Add(BinaryToHex(item));
            }

            return outputWords;
        }

        //converts any given double's fractional section into binary
        static string ConvertFractionToBinary(double number)
        {
            double fractionalPart = number - Math.Floor(number); // Extract fractional part
            if (fractionalPart == 0) return "0"; // If no fractional part, return "0"

            string binary = "";
            int precision = 32; // Limit precision to avoid infinite loops for repeating fractions

            while (fractionalPart > 0 && precision > 0)
            {
                fractionalPart *= 2;
                int bit = (int)Math.Floor(fractionalPart);
                binary += bit.ToString();
                fractionalPart -= bit;
                precision--;
            }

            return binary;
        }

        //gets the fractional section of the square root of any number
        static double GetSquareRootFractionalPart(int num)
        {
            double sqrtValue = Math.Sqrt(num);
            return sqrtValue - Math.Floor(sqrtValue);
        }

        //gets the fractional section of the cubed root of any number
        static double GetCubeRootFractionalPart(int num)
        {
            double cubeRoot = Math.Pow(num, 1.0 / 3.0);
            return cubeRoot - Math.Floor(cubeRoot);
        }

        //checks if given number is a prime number
        static bool IsPrime(int number)
        {
            if (number < 2)
                return false;

            for (int i = 2; i * i <= number; i++)
            {
                if (number % i == 0)
                    return false;
            }
            return true;
        }

        //turns any given binary string into hex
        static string BinaryToHex(string binary)
        {
            if (string.IsNullOrEmpty(binary))
                throw new ArgumentException("Input cannot be null or empty");

            // Ensure the length is a multiple of 4 for proper conversion
            int remainder = binary.Length % 4;
            if (remainder != 0)
            {
                binary = binary.PadLeft(binary.Length + (4 - remainder), '0');
            }

            // Convert binary to integer and then to hexadecimal
            int decimalValue = Convert.ToInt32(binary, 2);
            return decimalValue.ToString("X");
        }

        //turns any given string into its acscii values
        private static string StringToBinary(string input)
        {
            string output = string.Empty;

            foreach (char val in input)
            {
                output += AsciiToBinary(val);
            }

            return output;
        } 

        //converts characters from ASCII to binary
        private static string AsciiToBinary(int ascii)
        {
            return Convert.ToString(ascii, 2).PadLeft(8, '0'); // Converts to binary with 8-bit padding
        }
    }
}