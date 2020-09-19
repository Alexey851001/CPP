using System;
using System.Collections.Generic;

namespace ClassLibrary1
{
    public interface ITracer
    {
        void StartTrace();

        void StopTrace();

        TraceResult GetTraceResult();
    }

    public class TraceResult
    {
        
        public TimeSpan Elapsed;
        public TraceResult(TimeSpan elapsed)
        {
            Elapsed = elapsed;
        }
    }

    public class ThreadInfo
    {
        public int ThreadId;
        public LinkedList<MethodInfo> Methods = new LinkedList<MethodInfo>();

        public ThreadInfo(int threadId)
        {
            ThreadId = threadId;
        }
    }

    public class MethodInfo
    {
        public string MethodName;
        public string ClassName;
        public int ElapsedMs;
        
        public MethodInfo(string methodName, string className, int elapsedMs)
        {
            MethodName = methodName;
            ClassName = className;
            ElapsedMs = elapsedMs;
        }
    }
}