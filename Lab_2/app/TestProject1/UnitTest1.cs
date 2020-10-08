using app.Faker;
using app.TestClasses;
using NUnit.Framework;

namespace TestProject1
{
    public class Tests
    {
        private Faker faker = new Faker();
        [Test]
        public void ObjNotNull()
        {
            Foo foo = faker.Create<Foo>();
            Assert.IsTrue(foo != null);
        }

        [Test]
        public void StringLengthBelow50()
        {
            Foo foo = faker.Create<Foo>();
            Assert.IsTrue(foo.SValue.Length <= 50);
        }

        [Test]
        public void CycleDependence()
        {
            A a = faker.Create<A>();
            Assert.AreEqual(a.b.c.a, null);
        }

        [Test]
        public void NestObjectIsExist()
        {
            Foo foo = faker.Create<Foo>();
            Assert.IsTrue(foo.BarObj != null);
        }

        [Test]
        public void ListGeneratorIsExist()
        {
            Foo foo = faker.Create<Foo>();
            Assert.IsTrue(foo.ListValue != null);
        }
    }
}