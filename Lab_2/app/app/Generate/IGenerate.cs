using System;

namespace app.Generate
{
    public interface IGenerate
    {
        Object Generate();
        Object GenericGenerate(Type type);
    }
}