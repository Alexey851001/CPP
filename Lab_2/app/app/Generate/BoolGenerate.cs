using System;

namespace app.Generate
{
    public class BoolGenerate : IGenerate
    {
        public object Generate()
        {
            Random random = new Random();
            return (random.Next(0, 2) == 0);
        }

        public object GenericGenerate(Type type)
        {
            return null;
        }
    }
}