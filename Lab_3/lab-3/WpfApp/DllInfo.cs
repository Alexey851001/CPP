using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using AssemblyLibrary;
using WpfApp.Annotations;

namespace WpfApp
{
    public class DllInfo : INotifyPropertyChanged
    {
        private string path;

        public string Path
        {
            get => path;
            set
            {
                path = value;
                OnPropertyChanged("Path");
            }
        }

        private string name;

        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        private AssemblyInfo _assemblyInfo;
        
        public TreeViewItem AssemblyInfo 
        { 
            get 
            {
                return AssemblyLib.BuildTree(_assemblyInfo);
            } 
        }

        public DllInfo(string dllPath, string dllName, AssemblyInfo assemblyInfo)
        {
            Path = dllPath;
            Name = dllName;
            _assemblyInfo = assemblyInfo;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}