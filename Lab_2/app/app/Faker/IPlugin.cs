using System;
using app.Generate;

namespace app.Faker
{
    public interface IPlugin : IGenerate
    {
        Type GetGeneratorType();
    }
}