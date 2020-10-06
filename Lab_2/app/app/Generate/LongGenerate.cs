using System;
using System.Collections.Generic;

namespace app.Generate
{
    public class LongGenerate : IGenerate
    {
        public object Generate()
        {
            Random random = new Random();
            return random.Next();
        }

        public object GenericGenerate(Type type, Dictionary<Type, IGenerate> dictionary)
        {
            return null;
        }
    }
}