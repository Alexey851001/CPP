using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks.Dataflow;
using TestsClassGenerator;

namespace ConsoleApp
{
   class Program
   {
      private const string DestPath = "D:\\5sem\\SPP\\CPP\\Lab_4\\Output\\";
      private const int MaxDegreeOfParallelismLoad = 4;
      private const int MaxDegreeOfParallelismGenerate = 4;
      private const int MaxDegreeOfParallelismSave = 4;
      private static string[] _input = new[] { 
         "D:\\5sem\\SPP\\CPP\\Lab_1\\ClassLibrary\\Tracer.cs",
         "D:\\5sem\\SPP\\CPP\\Lab_2\\app\\app\\Faker\\Faker.cs",
         "D:\\5sem\\SPP\\CPP\\Lab_3\\lab-3\\WpfApp\\DllInfo.cs"
      };
      
      public static void Main(string[] args)
      {
         var loadSourceFileToMemory = new TransformBlock<string, string>(async path =>
         {
            Console.WriteLine("Loading to memory '{0}'...", path);

            using (StreamReader SourceReader = File.OpenText(path))
            {
               return await SourceReader.ReadToEndAsync();
            }
         }, new ExecutionDataflowBlockOptions
         {
            MaxDegreeOfParallelism = MaxDegreeOfParallelismLoad
         });

         
         foreach (var path in _input)
         {
            loadSourceFileToMemory.Post(path);
         }

         loadSourceFileToMemory.Complete();
         
      }
   }
}
