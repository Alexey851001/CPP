using System;
using System.IO;
using Newtonsoft.Json;

namespace ClassLibrary1
{
    public class JsonSerialize : ISerializer<TraceResult>
    {
        private const string FILE_PATH = "Json output.txt";
        public string Json ;

        public string Stringify(TraceResult obj)
        {
            Json = JsonConvert.SerializeObject(obj, Formatting.Indented);
            return Json;
        }

        public void SaveToFile()
        {
            using ( FileStream fileStream = File.Create(FILE_PATH))
            {
                byte[] array = System.Text.Encoding.Default.GetBytes(Json);
                fileStream.Write(array,0,array.Length);
            }
        }

        public void WriteToConsole()
        {
            Console.WriteLine(Json);
        }
    }
}