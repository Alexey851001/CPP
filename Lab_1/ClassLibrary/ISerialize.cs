using System.IO;

namespace ClassLibrary1
{
    public interface ISerializer<T>
    {
        string Stringify(T obj);
        void SaveToFile();
        void WriteToConsole();
    }
}