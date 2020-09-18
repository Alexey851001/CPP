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
        public LinkedList<ThreadInfo> Threads = new LinkedList<ThreadInfo>();

        public TraceResult()
        {
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
        public long ElapsedMs;
        
        public MethodInfo(string methodName, string className)
        {
            MethodName = methodName;
            ClassName = className;
        }
    }
}