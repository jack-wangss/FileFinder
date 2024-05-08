using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Media.Media3D;

namespace LibFinder
{
    internal class MainWindowViewModel : ObservableObject
    {

        // wirte a command to open a file dialog
        public ICommand OpenFileCommand { get; private set; }
        // save the selected folder path
        public string FolderPath { get; private set; }

        public MainWindowViewModel()
        {
            OpenFileCommand = new RelayCommand(OpenFile);
            FolderPath = "not a path.";
        }

        private void OpenFile()
        {
            // open OpenFolderDialog dialog,
            OpenFolderDialog dialog = new OpenFolderDialog();
            // get the selected folder path
            dialog.ShowDialog();

            FolderPath = dialog.FolderName;
            OnPropertyChanged(nameof(FolderPath));
        }

    }
}
