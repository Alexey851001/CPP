using System;

namespace app.Generate
{
    public class LongGenerate : IGenerate
    {
        public object Generate()
        {
            Random random = new Random();
            return random.Next();
        }

        public object GenericGenerate(Type type)
        {
            return null;
        }
    }
}