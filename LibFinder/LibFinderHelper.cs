using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibFinder
{
    internal class LibFinderHelper
    {
        public static List<string> StartFind(string dumpbinPath, string folderPath, string symbol, bool recursiveSearch)
        {
            // get all lib file paths
            List<string> libFiles = new List<string>();
            if (recursiveSearch)
            {
                // get all files in the folder and its subfolders
                libFiles.AddRange(System.IO.Directory.GetFiles(folderPath, "*.lib", System.IO.SearchOption.AllDirectories));
            }
            else
            {
                // get all files in the folder
                libFiles.AddRange(System.IO.Directory.GetFiles(folderPath, "*.lib", System.IO.SearchOption.TopDirectoryOnly));
            }

            return libFiles.Where(x => IsContentInFile(dumpbinPath,x, symbol)).ToList();
        }
        private static bool IsContentInFile(string dumpbinPath,string filePath, string symbol)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = dumpbinPath;
            startInfo.Arguments = "/EXPORTS " + filePath;
            startInfo.RedirectStandardOutput = true;
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true;

            Process process = new Process();
            process.StartInfo = startInfo;
            process.Start();

            string output = process.StandardOutput.ReadToEnd();
            Console.WriteLine(output);
            process.WaitForExit();

            return output.Contains(symbol);
        }
    }
}
