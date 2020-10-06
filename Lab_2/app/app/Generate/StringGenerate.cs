using System;
using System.Collections.Generic;

namespace app.Generate
{
    public class StringGenerate : IGenerate
    {
        public object Generate()
        {
            const string ALPHABET = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890";
            const int MAX_STRING_SIZE = 50;
            
            Random random = new Random();
            
            int stringLength = random.Next(0,MAX_STRING_SIZE);
            List<char> charList = new List<char>();
            for (int j = 0; j < stringLength; j++)
            {
                charList.Add(ALPHABET[random.Next(0,ALPHABET.Length)]);
            }
            return new string(charList.ToArray());
        }

        public object GenericGenerate(Type type, Dictionary<Type,IGenerate> dictionary)
        {
            return null;
        }
    }
}