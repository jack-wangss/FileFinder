using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace LibFinder
{
    internal class ConfigManager
    {
        class Config
        {
            public string LibPath { get; set; } = string.Empty;
            // dumpbin.exe
            public string DumpbinPath { get; set; } = string.Empty;
        }
        public bool HasConfig { get; set; } = false;

        const string _configFileName = "libFinderConfig.json";
        private Config _configData = new Config();
        string _configPath;
        public ConfigManager()
        {
            var configFolder = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            _configPath = System.IO.Path.Combine(configFolder, _configFileName);
            if (System.IO.File.Exists(_configPath))
            {
                LoadConfig();
            }
            else
            {
                HasConfig = false;
            }
        }

        public string GetLibPath()
        {
            return _configData.LibPath;
        }
        public string GetDumpbinPath()
        {
            return _configData.DumpbinPath;
        }
        public void SetLibPath(string libPath)
        { 
            _configData.LibPath = libPath;
            SaveConfig();
        }
        public void SetDumpbinPath(string dumpbinPath) 
        {
            _configData.DumpbinPath = dumpbinPath;
            SaveConfig();
        }


        private void LoadConfig()
        {
            var json = System.IO.File.ReadAllText(_configPath);
            var data = JsonSerializer.Deserialize<Config>(json);
            if (data != null)
            {
                _configData = data;
                HasConfig = true;
            }
            else
            {
                HasConfig = false;
            }
        }

        public void SaveConfig()
        {
            var json = JsonSerializer.Serialize(_configData);
            System.IO.File.WriteAllText(_configPath, json);
            HasConfig = true;
        }
    }
}
