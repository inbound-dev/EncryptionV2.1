using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Drawing.Text;
using System.Linq;
using System.Net.Security;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace Encryption.HashFunction
{
    internal class Hash
    {
        //message schedule is global as it is used in multiple places
        private static List<String> message = new List<String>();

        //8 working variables
        private static String a,b,c,d,e,f,g,h;
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
            while (binaryRep.Length < 480)
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
                    message.Add(binaryRep);
                }
            }

            //takes the message list and returns the wordSchedule list
            wordSchedulde = CreateWordSchedule(message);

            //initialize 8 working variables
            a = HexToBinary(initHVals[0]);
            b = HexToBinary(initKVals[1]);
            c = HexToBinary(initKVals[2]);
            d = HexToBinary(initKVals[3]);
            e = HexToBinary(initKVals[4]);
            f = HexToBinary(initKVals[5]);
            g = HexToBinary(initKVals[6]);
            h = HexToBinary(initKVals[7]); 

            

            return binaryRep;
        }

        //create word schedule
        static List<String> CreateWordSchedule(List<String> input)
        { 
            List<String> result = new List<String>();

            //adds the first 16 words to the schedule
            foreach (var item in input)
            {
                string var = item.Remove(' ');
                result.Add(var);
            }
            //create the rest of the words in the schedule
            int currentPos = result.Count;
            while (currentPos <= 24)
            {
                //formula for each word: w(t) = sigmaOne(w(t-2)) + w(t-7) + SigmaZero(w(t-15)) + w(t-16)
                string currWord = XorGate(SigmaOne(result[currentPos - 2]), result[currentPos - 7], SigmaZero(result[currentPos - 15]), result[currentPos - 16]);

                result.Add(currWord);

                currentPos++;
            }

            return result;
        }

        static string SigmaOne(string input)
        {
            string output = "";
            input = input.Replace(" ", "");

            //right rotate 17
            string stage1 = RotateBinaryString(input, 17);

            //right rotate 19
            string stage2 = RotateBinaryString(stage1, 19);

            //right shift 10
            string stage3 = RightShiftBinaryString(stage2, 10);

            //xor the result of all 3 operations
            output = XorGate(stage1, stage2, stage3);
            return output; 
        }

        static string SigmaZero(string input)
        {
            string output = "";
            input = input.Replace(" ", "");
            input += "0";
            
            //right rotate 7
            string stage1 = RotateBinaryString(input, 7);

            //right rotate 18
            string stage2 = RotateBinaryString(stage1, 18);

            //right shift 3
            string stage3 = RightShiftBinaryString(stage2, 3);

            //xor the result of all 3 operations
            output = XorGate(stage1, stage2, stage3);

            return output;
        }

        //shifts any given binary string to the right
        public static string RightShiftBinaryString(string binary, int shiftAmount)
        {
            // Ensure the binary string is not null or empty
            if (string.IsNullOrEmpty(binary))
                throw new ArgumentException("Binary string must not be null or empty.");

            // Clamp shiftAmount to the length of the string
            shiftAmount = Math.Min(shiftAmount, binary.Length);

            // Perform the shift
            string shifted = new string('0', shiftAmount) + binary.Substring(0, binary.Length - shiftAmount);
            return shifted;
        }

        //takes given binary string and rotates it
        public static string RotateBinaryString(string binary, int rotationAmount)
        {
            int length = binary.Length;

            rotationAmount = rotationAmount % length;

            if (rotationAmount == 0)
                return binary;

            string rotated = binary.Substring(length - rotationAmount) + binary.Substring(0, length - rotationAmount);

            return rotated;
        }

        //takes 4 binary strings and xors them
        static string XorGate(string input1, string input2, string input3, string input4)
        {
            StringBuilder output = new StringBuilder(input1.Length);

            if (input1.Length != input2.Length && input2.Length != input3.Length && input3.Length != input4.Length)
            {
                Console.WriteLine(input1.Length);
                Console.WriteLine(input2.Length);
                Console.WriteLine(input3.Length);
                Console.WriteLine(input4.Length);
                throw new Exception("XOR inputs are not the same length!");
            }

            for (int i = 0; i < input1.Length; i++)
            {
                int bit1 = input1[i] - '0';
                int bit2 = input2[i] - '0';
                int bit3 = input3[i] - '0';
                int bit4 = input4[i] - '0';

                int xorResult = bit1 ^ bit2 ^ bit3 ^ bit4; // XOR of 4 bits

                output.Append(xorResult);
            }

            return output.ToString();
        }

        //takes three strings of binary and xors them
        static string XorGate(string input1, string input2, string input3)
        {
            StringBuilder output = new StringBuilder(input1.Length);

            for (int i = 0; i < input1.Length; i++)
            {
                int bit1 = input1[i] - '0';
                int bit2 = input2[i] - '0';
                int bit3 = input3[i] - '0';

                int xorResult = bit1 ^ bit2 ^ bit3;

                output.Append(xorResult);
            }

            return output.ToString();
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

        static string HexToBinary(String hexInput)
        {
            // Convert hex to unsigned 32-bit integer
            uint number = Convert.ToUInt32(hexInput, 16);

            // Convert to binary string, padded to 32 bits
            string binaryString = Convert.ToString(number, 2).PadLeft(32, '0');

            return binaryString;
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