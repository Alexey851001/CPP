using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

namespace ClassLibrary1
{
    public class Tracer : ITracer
    {
        public Stack<MethodInfo> MethodInfos = new Stack<MethodInfo>();
        private Stack<Stopwatch> stackWatch = new Stack<Stopwatch>();
        public Tracer()
        {
        }
        
        public void StartTrace()
        {
            Stopwatch stopwatch = new Stopwatch();
            stackWatch.Push(stopwatch);
            stopwatch.Start();
        }

        public void StopTrace()
        {
            stackWatch.Peek().Stop();
            Stopwatch stopwatch = stackWatch.Pop();
            string methodName = "", className = "";
            GetMethodAndClassName(out methodName,out className);
            MethodInfos.Push(new MethodInfo(methodName,className,stopwatch.Elapsed.Milliseconds));
        }

        public TraceResult GetTraceResult()
        {
            return null;
        }

        private void GetMethodAndClassName(out string methodName, out string className)
        {
            const int index = 2;
            
            var st = new StackTrace();
            var sf = st.GetFrame(index);
            
            MethodBase method = sf.GetMethod();
            
            className = method.ReflectedType.Name;
            methodName = method.Name;
        }
    }
}