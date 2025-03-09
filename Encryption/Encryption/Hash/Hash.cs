using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encryption.HashFunction
{
    internal class Hash
    {

        public String NewHash(string inputKey)
        {
            String finalKey = "";

            //extend length to 32 chars
            if (inputKey.Length < 32)
            {
                Console.WriteLine("hey");
                for (int i = 0; i < (32 / inputKey.Length); i++)
                {
                    inputKey.Insert(inputKey.Length, "q");
                }
                finalKey = inputKey;//.Length.ToString();
            }


            return finalKey;
        }
    }
}
