using System.IO;

namespace WpfApp
{
    public class DllInfo
    {
        public string Path;
        public string Name;

        public DllInfo(string dllPath, string dllName)
        {
            Path = dllPath;
            Name = dllName;
        }
    }
}