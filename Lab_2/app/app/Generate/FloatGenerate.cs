using System;
using System.Collections.Generic;

namespace app.Generate
{
    public class FloatGenerate : IGenerate
    {
        public object Generate()
        {
            Random random = new Random();
            return (float)random.NextDouble();
        }

        public object GenericGenerate(Type type, Dictionary<Type, IGenerate> dictionary)
        {
            return null;
        }
    }
}