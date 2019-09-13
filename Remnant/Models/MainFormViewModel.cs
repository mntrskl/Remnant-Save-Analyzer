using Remnant.FileIO;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.Caching;
using System.Text;

namespace Remnant.Models
{
    public class MainFormViewModel
    {
        private FileSystemWatcher saveWatcher;
        private string _oldPath;
        private string _currentPath;
        private MemoryCache _memCache;
        private CacheItemPolicy _cacheItemPolicy;
        private const int CacheTimeMilliseconds = 1000;
        public MainForm form;

        public MainFormViewModel()
        {
            form = new MainForm(this);
            _oldPath = string.Empty;
            _currentPath = string.Empty;
        }

        private void InitializeWatcher()
        {
            _memCache = MemoryCache.Default;
            if (saveWatcher != null)
            {
                saveWatcher.Dispose();
            }

            saveWatcher = new FileSystemWatcher()
            {
                Path = Path.GetDirectoryName(_currentPath),
                Filter = Path.GetFileName(_currentPath),
                NotifyFilter = NotifyFilters.LastWrite
            };
            _cacheItemPolicy = new CacheItemPolicy() { RemovedCallback = OnRemovedFromCache };
            saveWatcher.Changed += OnFileChanged;
            saveWatcher.EnableRaisingEvents = true;
        }
        private void KillWatcher()
        {
            if (saveWatcher != null)
            {
                saveWatcher.Changed -= OnFileChanged;
                saveWatcher.EnableRaisingEvents = false;
                saveWatcher.Dispose();
            }
        }

        public void FileUpdate(string filePath)
        {
            //if (!_oldPath.Equals(filePath))
            //{
            //    InitializeWatcher(filePath);
            //}
            _currentPath = filePath;
            SaveFileReader reader = new SaveFileReader(filePath);
            form.UpdateCampaignGrid(reader.ParseCampaign());
            form.UpdateAdventureGrid(reader.ParseAdventure());
        }

        public bool SetWatcher (bool enabled)
        {
            if (enabled && _currentPath != string.Empty)
            {
                InitializeWatcher();
                return true;
            }
            else
            {
                KillWatcher();
                return false;
            }
        }

        public void ExportCSV(string filePath, DataTable dt)
        {
            StringBuilder sb = new StringBuilder();

            IEnumerable<string> columnNames = dt.Columns.Cast<DataColumn>().
                                              Select(column => column.ColumnName);
            sb.AppendLine(string.Join(",", columnNames));

            foreach (DataRow row in dt.Rows)
            {
                IEnumerable<string> fields = row.ItemArray.Select(field => field.ToString());
                sb.AppendLine(string.Join(",", fields));
            }

            File.WriteAllText(filePath, sb.ToString());
        }

        private void OnFileChanged(object source, FileSystemEventArgs e)
        {
            _cacheItemPolicy.AbsoluteExpiration =
           DateTimeOffset.Now.AddMilliseconds(CacheTimeMilliseconds);

            // Only add if it is not there already (swallow others)
            _memCache.AddOrGetExisting(e.Name, e, _cacheItemPolicy);
        }
        private void OnRemovedFromCache(CacheEntryRemovedArguments args)
        {
            if (args.RemovedReason != CacheEntryRemovedReason.Expired) return;
            // Now actually handle file event
            var e = (FileSystemEventArgs)args.CacheItem.Value;
            FileUpdate(e.FullPath);
        }
    }
}
