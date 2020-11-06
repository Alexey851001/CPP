using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using AssemblyLibrary;
using Microsoft.Win32;

namespace WpfApp
{
    public class ApplicationViewModel : INotifyPropertyChanged
    {
        private DllInfo selectedDll;
        public DllInfo SelectedDll
        {
            get => selectedDll;
            set
            {
                selectedDll = value;
                OnPropertyChanged("SelectedDll");
            }
        }

        public ObservableCollection<DllInfo> DllInfos { get; set; }

        private Command addCommand;
        public Command AddCommand
        {
            get => addCommand;
        }

        public ApplicationViewModel()
        {
            DllInfos = new ObservableCollection<DllInfo>();
            addCommand = new Command(obj =>
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Dll files(*.dll)|*.dll";
                openFileDialog.ShowDialog();
                string filename = openFileDialog.FileName;
                if (filename == "") return;
                AssemblyInfo assemblyInfo = AssemblyLib.GetAssemblyInfo(filename);

                DllInfo dll = new DllInfo(openFileDialog.FileName, openFileDialog.SafeFileName, assemblyInfo);
                DllInfos.Insert(0, dll);
                SelectedDll = dll;
            });
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}