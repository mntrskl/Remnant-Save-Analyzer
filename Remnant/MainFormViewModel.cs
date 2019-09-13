using somethingDifferent.FileIO;
using System;
using System.Collections.Generic;
using System.IO;

namespace somethingDifferent.Models
{
    public class MainFormViewModel
    {
        private FileSystemWatcher saveWatcher;
        private string _oldPath;
        public MainForm form;

        public MainFormViewModel()
        {
            form = new MainForm(this);
            _oldPath = string.Empty;
        }

        private void InitializeWatcher(string filePath)
        {
            if (saveWatcher != null)
            {
                saveWatcher.Dispose();
            }

            saveWatcher = new FileSystemWatcher()
            {
                Path = Path.GetDirectoryName(filePath),
                Filter = Path.GetFileName(filePath)
            };

            saveWatcher.Changed += OnChangedAsync;
            saveWatcher.EnableRaisingEvents = true;
        }

        public void FileUpdate(string filePath)
        {
            if (!_oldPath.Equals(filePath))
            {
                InitializeWatcher(filePath);
            }

            SaveFileReader reader = new SaveFileReader(filePath);
            form.UpdateCampaignGrid(reader.ParseCampaign());
            form.UpdateAdventureGrid(reader.ParseAdventure());
        }

        private void OnChangedAsync(object source, FileSystemEventArgs e) => FileUpdate(e.FullPath);
    }
}
