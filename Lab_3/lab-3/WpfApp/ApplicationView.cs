using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using AssemblyLibrary;
using Microsoft.Win32;

namespace WpfApp
{
    public class ApplicationView : INotifyPropertyChanged
    {
        private DllInfo selectedDll;
        public DllInfo SelectedDll
        {
            get => selectedDll;
            set => selectedDll = value;
        }

        public ObservableCollection<DllInfo> DllInfos { get; set; }

        private Command addCommand;
        public Command AddCommand
        {
            get
            {
                return addCommand ??
                       (addCommand = new Command(obj =>
                       {
                           OpenFileDialog openFileDialog = new OpenFileDialog();
                           openFileDialog.Filter = "Dll files(*.dll)|*.dll";
                           openFileDialog.ShowDialog();
                           string filename = openFileDialog.FileName;
                           if (filename == "") return;
                           AssemblyLib.AssemblyInfo assemblyInfo = AssemblyLib.GetAssemblyInfo(filename);

                           DllInfo dll = new DllInfo(openFileDialog.FileName, openFileDialog.SafeFileName, assemblyInfo);
                           DllInfos.Insert(0, dll);
                           SelectedDll = dll;
                       }));
            }
        }

        public ApplicationView()
        {
            DllInfos = new ObservableCollection<DllInfo>();
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}