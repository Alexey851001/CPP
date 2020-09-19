using System;
using System.IO;
using System.Threading;
using ClassLibrary1;
namespace ConsoleApplication
{
    class Program
    {
        public static void Main(string[] args)
        {
            Tracer tracer = new Tracer();
            Foo foo = new Foo(tracer);
            
            foo.MyMethod();
            tracer.StartTrace();
            Thread.Sleep(20);
            
            tracer.StopTrace();
            while (tracer.MethodInfos.Count != 0)
            {
                MethodInfo methodInfo = tracer.MethodInfos.Pop();
                
                Console.WriteLine(methodInfo.ClassName);
                Console.WriteLine(methodInfo.MethodName);
                Console.WriteLine(methodInfo.ElapsedMs);
            }
        }
    }
    
    public class Foo
    {
        private Bar _bar;
        private ITracer _tracer;

        internal Foo(ITracer tracer)
        {
            _tracer = tracer;
            _bar = new Bar(_tracer);
        }
    
        public void MyMethod()
        {
            _tracer.StartTrace();
            
            _bar.InnerMethod();
            
            _tracer.StopTrace();
        }
    }

    public class Bar
    {
        private ITracer _tracer;

        internal Bar(ITracer tracer)
        {
            _tracer = tracer;
        }
    
        public void InnerMethod()
        {
            _tracer.StartTrace();
            Thread.Sleep(10);
            _tracer.StopTrace();
        }
    }
}