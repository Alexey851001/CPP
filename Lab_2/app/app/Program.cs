using System;
using app.TestClasses;

namespace app
{
    class Program
    {
        static void Main(string[] args)
        {
            Faker.Faker faker = new Faker.Faker();
             
            Foo foo = faker.Create<Foo>();
            Bar bar = faker.Create<Bar>();
            A a = faker.Create<A>();
        }
    }
    
}