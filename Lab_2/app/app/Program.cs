using System;
using app.TestClasses;
using Newtonsoft.Json;

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
            Console.WriteLine("Foo:" + JsonConvert.SerializeObject(foo, Formatting.Indented));
            Console.WriteLine("Bar:" + JsonConvert.SerializeObject(bar, Formatting.Indented));
            Console.WriteLine("A:" +JsonConvert.SerializeObject(a, Formatting.Indented));
        }
    }
    
}