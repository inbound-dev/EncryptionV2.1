using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Encryption.HashFunction;

namespace Encryption.EncryptionFiles
{
    internal class Encryption
    {
        private static Dictionary<char, char> letterMap = new Dictionary<char, char>
{
            {'A', 'Þ'}, {'B', 'M'}, {'C', 'Q'}, {'D', 'Z'}, {'E', 'L'}, {'F', 'T'}, {'G', 'Y'}, {'H', 'Œ'}, {'I', 'S'}, {'J', 'B'},
            {'K', 'N'}, {'L', 'H'}, {'M', 'E'}, {'N', 'W'}, {'O', 'C'}, {'P', 'U'}, {'Q', 'A'}, {'R', 'K'}, {'S', 'D'}, {'T', 'F'},
            {'U', 'P'}, {'V', 'J'}, {'W', 'G'}, {'X', 'R'}, {'Y', 'V'}, {'Z', 'I'},

            {'a', 'ü'}, {'b', 'ğ'}, {'c', 'ș'}, {'d', 'å'}, {'e', 'é'}, {'f', 'î'}, {'g', 'ø'}, {'h', 'ñ'}, {'i', 'č'}, {'j', 'ß'},
            {'k', 'œ'}, {'l', 'æ'}, {'m', 'ž'}, {'n', 'á'}, {'o', 'í'}, {'p', 'ð'}, {'q', 'ù'}, {'r', 'ô'}, {'s', 'ò'}, {'t', 'ë'},
            {'u', 'â'}, {'v', 'ê'}, {'w', 'î'}, {'x', 'û'}, {'y', 'ý'}, {'z', 'ã'},

            {'0', '!'}, {'1', '@'}, {'2', '#'}, {'3', '$'}, {'4', '%'}, {'5', '^'}, {'6', '&'}, {'7', '*'}, {'8', '('}, {'9', ')'},

            {'!', '0'}, {'@', '1'}, {'#', '2'}, {'$', '3'}, {'%', '4'}, {'^', '5'}, {'&', '6'}, {'*', '7'}, {'(', '8'}, {')', '9'},

            {'Á', 'X'}, {'É', 'Ð'}, {'Í', 'Ø'}, {'Ó', 'Æ'}, {'Ú', 'Ñ'}, {'Ü', 'ß'}, {'Ç', 'Ğ'}, {'Ñ', 'Ş'}, {'Å', 'Œ'}, {'Ø', 'Ž'},

            {'.', 'Ç'}, {',', 'Ý'}, {';', 'Ł'}, {':', 'ŧ'}, {'"', 'ß'}, {'\'', 'ø'}, {'?', 'Ğ'}, {'/', 'Ŋ'}, {'\\', 'Ǝ'}, {'|', 'Ð'},
            {'[', '«'}, {']', '»'}, {'{', '‹'}, {'}', '›'}, {'<', '©'}, {'>', '®'}, {'-', '±'}, {'_', '¬'}, {'=', '≠'}, {'+', 'µ'},

            {'`', '¿'}, {'~', '¥'}, {' ', '¤'}
        };

        //defualt constructor runs as soon as object is created
        public Encryption(String fileName, String key)
        {

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
