using System;
using System.Collections.Generic;
using app.Faker;
using app.Generate;

namespace ByteGenerate
{
    public class ByteGenerate: IPlugin
    {
        public object Generate()
        {
            Random random = new Random();
            return random.Next(0,255);
        }

        public object GenericGenerate(Type type, Dictionary<Type, IGenerate> dictionary)
        {
            return null;
        }

        public Type GetGeneratorType()
        {
            return typeof(byte);
        }
    }
}