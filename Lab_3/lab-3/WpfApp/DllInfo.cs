using System.IO;
using AssemblyLibrary;

namespace WpfApp
{
    public class DllInfo
    {
        private string path;

        public string Path
        {
            get => path;
            set => path = value;
        }

        private string name;

        public string Name
        {
            get => name;
            set => name = value;
        }

        public AssemblyLib.AssemblyInfo AssemblyInfo;

        public DllInfo(string dllPath, string dllName, AssemblyLib.AssemblyInfo assemblyInfo)
        {
            Path = dllPath;
            Name = dllName;
            AssemblyInfo = assemblyInfo;
        }
    }
}