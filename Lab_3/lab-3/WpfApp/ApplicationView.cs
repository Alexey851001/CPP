using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WpfApp
{
    public class ApplicationView : INotifyPropertyChanged
    {
        public DllInfo SelectedDll;

        public ObservableCollection<DllInfo> DllInfos;
        
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