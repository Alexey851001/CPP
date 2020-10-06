using System;

namespace app.Generate
{
    public class IntGenerate : IGenerate
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