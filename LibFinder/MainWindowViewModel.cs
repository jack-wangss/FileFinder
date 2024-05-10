using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Media.Media3D;
using System.Collections.ObjectModel;

namespace LibFinder
{
    internal class MainWindowViewModel : ObservableObject
    {
        // command to select dumpbin folder
        public ICommand SelectDumpbinPathCommand { get; private set; }
        // command to open a file dialog
        public ICommand OpenFileCommand { get; private set; }
        // command to fine
        public ICommand FindCommand { get; private set; }

        // save the selected folder path
        public string FolderPath { get; private set; } = "select a folder...";
        // save the selected dumpbin path
        public string DumpbinPath { get; private set; } = "select dumpbin.exe...";
        // recursive search
        public bool RecursiveSearch { get; set; } = false;
        // result list
        public ObservableCollection<ResultItemViewModel> ResultList { get; private set; } = new ObservableCollection<ResultItemViewModel>();
        // symbol to find
        public string Symbol { get; set; } = string.Empty;

        private ConfigManager _configManager;
        public MainWindowViewModel()
        {
            _configManager = new ConfigManager();
            if(_configManager.HasConfig)
            {
                FolderPath = _configManager.GetLibPath();
                DumpbinPath = _configManager.GetDumpbinPath();
            }

            OpenFileCommand = new RelayCommand(OpenFile);
            FindCommand = new RelayCommand(RunFind);
            SelectDumpbinPathCommand = new RelayCommand(SelectDumpbin);
        }

        private void OpenFile()
        {
            // open OpenFolderDialog dialog,
            OpenFolderDialog dialog = new OpenFolderDialog();
            // get the selected folder path
            dialog.ShowDialog();

            FolderPath = dialog.FolderName;
            OnPropertyChanged(nameof(FolderPath));

            _configManager.SetLibPath(FolderPath);
        }

        private void SelectDumpbin()
        {
            // open OpenFileDialog dialog
            OpenFileDialog dialog = new OpenFileDialog();
            // set the filter
            dialog.Filter = "dumpbin.exe|dumpbin.exe";
            // get the selected file path
            dialog.ShowDialog();

            DumpbinPath = dialog.FileName;
            OnPropertyChanged(nameof(DumpbinPath));

            _configManager.SetDumpbinPath(DumpbinPath);
        }

        private void RunFind()
        {
           var results = LibFinderHelper.StartFind(DumpbinPath,FolderPath, Symbol, RecursiveSearch);
            ResultList.Clear();
            foreach (var result in results)
            {
                ResultList.Add(new ResultItemViewModel() { Path = result });
            }
            OnPropertyChanged(nameof(ResultList));
        }
    }
}
