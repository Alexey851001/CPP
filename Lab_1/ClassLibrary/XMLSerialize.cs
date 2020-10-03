using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace ClassLibrary1
{
    public class XmlSerialize : ISerializer<TraceResult>
    {
        private const string FILE_PATH = "XML output.txt";
        private readonly XDocument xdoc = new XDocument();

        public string Stringify(TraceResult traceResult)
        {
            XElement root = new XElement("root");

            foreach (ThreadInfo threadInfo in traceResult.ThreadInfos)
            {
                XElement thread = new XElement("thread");
                
                XAttribute threadId = new XAttribute("id", threadInfo.ThreadId);
                XAttribute threadTimeMs = new XAttribute("timeMs", threadInfo.ElapsedMs);
                thread.Add(threadId, threadTimeMs);
                
                AddMethods(thread, threadInfo.Methods);
                
                root.Add(thread);
            }
            
            xdoc.Add(root);

            return xdoc.ToString();
        }
        
        private static void AddMethods(XElement element, LinkedList<MethodInfo> list)
        {
            foreach (MethodInfo methodInfo in list)
            {
                XElement method = new XElement("method");
                
                XAttribute methodName = new XAttribute("name", methodInfo.MethodName);
                XAttribute methodClass = new XAttribute("class", methodInfo.ClassName);
                XAttribute methodTimeMs = new XAttribute("timeMs", methodInfo.ElapsedMs);
                
                method.Add(methodName, methodClass, methodTimeMs);
                
                AddMethods(method, methodInfo.InsertedMethod);
                
                element.Add(method);
            }
        }

        public void SaveToFile()
        {
            using FileStream fileStream = File.Create(FILE_PATH);
            xdoc.Save(fileStream);
        }

        public void WriteToConsole()
        {
            Console.WriteLine(xdoc.ToString());
        }
    }
}