using System;
using System.Collections.Generic;

namespace app.Generate
{
    public interface IGenerate
    {
        Object Generate();
        Object GenericGenerate(Type type, Dictionary<Type, IGenerate> dictionary);
    }
}