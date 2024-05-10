using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;


namespace LibFinder
{
    internal class ResultItemViewModel
    {
        // copy path command
        public ICommand CopyPathCommand { get; private set; }
        // copy file name command
        public ICommand CopyFileCommand { get; private set; }
        public string Path { get; set; } = string.Empty;

        public ResultItemViewModel()
        {
            CopyPathCommand = new RelayCommand(CopyPath);
            CopyFileCommand = new RelayCommand(CopyFileName);
        }

        private void CopyPath()
        {
            // do not know why exception is thrown
            try
            {
                // copy the path to the clipboard
                System.Windows.Clipboard.SetText(Path);
            }
            catch (Exception)
            {

            }
        }
        private void CopyFileName()
        {
            // do not know why exception is thrown
            try
            {
                // copy the file name to the clipboard
                System.Windows.Clipboard.SetText(System.IO.Path.GetFileName(Path));
            }
            catch (Exception)
            {

            }
        }
    }
}
