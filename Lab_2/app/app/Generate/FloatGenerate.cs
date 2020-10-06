using System;

namespace app.Generate
{
    public class FloatGenerate : IGenerate
    {
        public object Generate()
        {
            Random random = new Random();
            return (float)random.NextDouble();
        }

        public object GenericGenerate(Type type)
        {
            return null;
        }
    }
}