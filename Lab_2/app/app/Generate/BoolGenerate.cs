using System;
using System.Collections.Generic;

namespace app.Generate
{
    public class BoolGenerate : IGenerate
    {
        public object Generate()
        {
            Random random = new Random();
            return (random.Next(0, 2) == 0);
        }

        public object GenericGenerate(Type type, Dictionary<Type, IGenerate> dictionary)
        {
            return null;
        }
    }
}