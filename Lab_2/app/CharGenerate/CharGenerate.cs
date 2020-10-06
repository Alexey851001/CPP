using System;
using System.Collections.Generic;
using app.Faker;
using app.Generate;
using app.TestClasses;

namespace CharGenerate
{
    public class CharGenerate : IPlugin
    {
        public object Generate()
        {
            const string ALPHABET = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890";
            Random random = new Random();
            return ALPHABET[random.Next(0, ALPHABET.Length)];
        }

        public object GenericGenerate(Type type, Dictionary<Type, IGenerate> dictionary)
        {
            return null;
        }

        public Type GetGeneratorType()
        {
            return typeof(char);
        }
    }
}