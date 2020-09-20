using ClassLibrary1;
using ConsoleApplication;
using NUnit.Framework;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void InitialThreadCountShouldBeZero()
        {
            ITracer tracer = new Tracer();
            TraceResult result = tracer.GetTraceResult();
            Assert.IsEmpty(result.ThreadInfos);
        }

        [Test]
        public void MethodCountIsCorrect()
        {
            ITracer tracer = new Tracer();
            Bar bar = new Bar(tracer);
            bar.InnerMethod();
            bar.InnerMethod();
            
            TraceResult result = tracer.GetTraceResult();

            Assert.AreEqual(2, result.ThreadInfos.Last?.Value.Methods.Count);
        }

        [Test]
        public void BarMethodIsInsertedInFooMethod()
        {
            ITracer tracer = new Tracer();
            Foo foo = new Foo(tracer);
            foo.MyMethod();

            TraceResult result = tracer.GetTraceResult();
            
            Assert.AreEqual(1,result.ThreadInfos.Last?.Value.Methods.Last?.Value.InsertedMethod.Count);
        }
        
        [Test]
        public void MethodNameIsCorrect()
        {
            ITracer tracer = new Tracer();
            Foo foo = new Foo(tracer);
            foo.MyMethod();
            
            TraceResult result = tracer.GetTraceResult();
            
            Assert.AreEqual("MyMethod", result.ThreadInfos.Last?.Value.Methods.Last?.Value.MethodName);
        }
        
        [Test]
        public void ClassNameIsCorrect()
        {
            ITracer tracer = new Tracer();
            Foo foo = new Foo(tracer);
            foo.MyMethod();
            
            TraceResult result = tracer.GetTraceResult();
            
            Assert.AreEqual("Foo", result.ThreadInfos.Last?.Value.Methods.Last?.Value.ClassName);
        }
    }
}