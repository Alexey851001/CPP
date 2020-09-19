using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace ClassLibrary1
{
    public class Tracer : ITracer
    {
        public readonly TraceResult Result = new TraceResult();
        public ConcurrentDictionary<int,Stack<MethodInfo> > MethodInfosDictionary = new ConcurrentDictionary<int, Stack<MethodInfo>>();
        private ConcurrentDictionary<int, Stack<Stopwatch> > stackWatchDictionary = new ConcurrentDictionary<int, Stack<Stopwatch>>();
        public Tracer() {}
        
        public void StartTrace()
        {
            int threadId = Thread.CurrentThread.ManagedThreadId;
            string methodName = "", className = "";
            GetMethodAndClassName(out methodName,out className, out threadId);
            MethodInfo methodInfo = new MethodInfo(methodName,className);
            
            if (!MethodInfosDictionary.Keys.Contains(threadId))
            {
                MethodInfosDictionary[threadId] = new Stack<MethodInfo>();
                stackWatchDictionary[threadId] = new Stack<Stopwatch>();
            }

            if (MethodInfosDictionary[threadId].Count != 0)
            {
                MethodInfosDictionary[threadId].Peek().InsertedMethod.AddLast(methodInfo);
            }
            MethodInfosDictionary[threadId].Push(methodInfo);
            
            Stopwatch stopwatch = new Stopwatch();
            stackWatchDictionary[threadId].Push(stopwatch);
            stopwatch.Start();
            
            
        }

        public void StopTrace()
        {
            int threadId = Thread.CurrentThread.ManagedThreadId;
            stackWatchDictionary[threadId].Peek().Stop();
            
            Stopwatch stopwatch = stackWatchDictionary[threadId].Pop();
            MethodInfo methodInfo = MethodInfosDictionary[threadId].Pop();
            methodInfo.ElapsedMs = stopwatch.ElapsedMilliseconds;
            
            if (MethodInfosDictionary[threadId].Count == 0)
            {
                ThreadInfo threadInfo = new ThreadInfo(threadId);
                if (Result.ThreadInfos.Contains(threadInfo))
                {
                    Result.ThreadInfos.Find(threadInfo)?.Value.Methods.AddLast(methodInfo);
                    
                    Result.ThreadInfos.Find(threadInfo).Value.ElapsedMs += methodInfo.ElapsedMs;
                }
                else
                {
                    threadInfo.Methods.AddLast(methodInfo);
                    threadInfo.ElapsedMs += methodInfo.ElapsedMs;
                    Result.ThreadInfos.AddLast(threadInfo);
                }

                
            }

        }

        public TraceResult GetTraceResult()
        {
            return Result;
        }

        private void GetMethodAndClassName(out string methodName, out string className, out int threadId)
        {
            const int index = 2;
            
            var st = new StackTrace();
            var sf = st.GetFrame(index);
            
            MethodBase method = sf.GetMethod();
            
            className = method.ReflectedType.Name;
            methodName = method.Name;
            threadId = Thread.CurrentThread.ManagedThreadId;
        }
    }
}