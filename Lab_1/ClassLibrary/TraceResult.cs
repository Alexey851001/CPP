using System.Collections.Generic;
using System.Diagnostics;

namespace ClassLibrary1
{
    public class TraceResult
    {
        public LinkedList<ThreadInfo> ThreadInfos = new LinkedList<ThreadInfo>();
        public TraceResult() {}
    }

    public class ThreadInfo
    {
        public int ThreadId;
        public long ElapsedMs = 0;
        public LinkedList<MethodInfo> Methods = new LinkedList<MethodInfo>();

        public ThreadInfo(int threadId)
        {
            ThreadId = threadId;
        }
        
        public override bool Equals(object obj)
        {
            if (obj is ThreadInfo)
            {
                return ((ThreadInfo)obj).ThreadId == ThreadId;
            }

            return false;
        }
    }

    public class MethodInfo
    {
        public string MethodName;
        public string ClassName;
        public long ElapsedMs;
        public LinkedList<MethodInfo> InsertedMethod = new LinkedList<MethodInfo>();
        
        public MethodInfo(string methodName, string className, long elapsedMs)
        {
            MethodName = methodName;
            ClassName = className;
            ElapsedMs = elapsedMs;
        }
        public MethodInfo(string methodName, string className)
        {
            MethodName = methodName;
            ClassName = className;
        }
    }
}