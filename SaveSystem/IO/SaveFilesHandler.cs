using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace PJL.SaveSystem.IO
{
    public class SaveFilesHandler : IFilesHandler
    {
        public string PreambleSeparator { get; }

        private string _saveDirectory = "Saves";
        // Does not have to just be an extension, it really is just a suffix for the save index
        // i.e. "SAVE.game" would work as well, and produce a file named "xSAVE.game" where x is the index
        private string _fileFormat = ".save";

        public SaveFilesHandler(string preambleSeparator)
        {
            PreambleSeparator = preambleSeparator;
        }

        public void ChangeFileFormat(string format) => _fileFormat = format;
        public void ChangeFilesDirectoryName(string name) => _saveDirectory = name;

        private string SavesPath => $"{Application.persistentDataPath}{Path.DirectorySeparatorChar}{_saveDirectory}{Path.DirectorySeparatorChar}";

        private string GetSaveFullPath(int idx) =>
            $"{SavesPath}{idx}{_fileFormat}";

        private void EnsureSavesDirectoryExists()
        {
            if (Directory.Exists(SavesPath)) return;
            Directory.CreateDirectory(SavesPath);
        }

        private static string GetFileName(string path) => path.Split(Path.DirectorySeparatorChar)[^1];

        private string GetFilePreamble(string path)
        {
            var preambleBuilder = new StringBuilder();
            using var reader = new StreamReader(path);
            string line;

            var trimmedSeparator = PreambleSeparator.Trim();
            while ((line = reader.ReadLine()) != null)
            {
                if (line.Trim() == trimmedSeparator)
                    break;
                preambleBuilder.Append(line);
            }

            return preambleBuilder.ToString();
        }

        private void SaveToIndex(int saveIndex, string data)
        {
            if (saveIndex < 0) return;
            EnsureSavesDirectoryExists();
            using var writer = new StreamWriter(GetSaveFullPath(saveIndex));
            writer.Write(data);
        }

        public int GetNextSaveIndex()
        {
            EnsureSavesDirectoryExists();
            var files = Directory.GetFiles(SavesPath);
            var max = 0;

            foreach (var file in files)
            {
                if (!file.EndsWith(_fileFormat)) continue;
                var filename = GetFileName(file);
                if (int.TryParse(filename[..^_fileFormat.Length], out var fileIdx) && max <= fileIdx)
                    max = fileIdx + 1;
            }

            return max;
        }

        public bool SaveExists(int saveIndex)
        {
            EnsureSavesDirectoryExists();
            var file = GetSaveFullPath(saveIndex);
            return File.Exists(file);
        }

        public SaveFileData GetSaveFileData(int saveIndex)
        {
            EnsureSavesDirectoryExists();
            var file = GetSaveFullPath(saveIndex);
            if (!File.Exists(file)) return new SaveFileData(-1, null);

            var preamble = GetFilePreamble(file);
            return new SaveFileData(saveIndex, preamble);
        }

        public IList<SaveFileData> GetAllSaveFilesData()
        {
            EnsureSavesDirectoryExists();
            var files = Directory.GetFiles(SavesPath);
            var results = new List<SaveFileData>();

            foreach (var file in files)
            {
                if (!file.EndsWith(_fileFormat)) continue;
                var filename = GetFileName(file);
                if (!int.TryParse(filename[..^_fileFormat.Length], out var fileIdx)) continue;
                var preamble = GetFilePreamble(file);

                results.Add(new SaveFileData(fileIdx, preamble));
            }

            return results;
        }

        public void Save(string data) => SaveToIndex(GetNextSaveIndex(), data);

        public void Override(int saveIndex, string data)
        {
            if (!Delete(saveIndex)) return;
            SaveToIndex(saveIndex, data);
        }

        public bool TryLoad(int saveIndex, out string data)
        {
            if (saveIndex < 0)
            {
                data = null;
                return false;
            }
            EnsureSavesDirectoryExists();

            var file = GetSaveFullPath(saveIndex);
            if (!File.Exists(file))
            {
                data = null;
                return false;
            }

            data = File.ReadAllText(file);
            return true;
        }

        public bool Delete(int saveIndex)
        {
            if (saveIndex < 0) return false;
            EnsureSavesDirectoryExists();

            var file = GetSaveFullPath(saveIndex);
            if (!File.Exists(file)) return false;

            File.Delete(file);
            return true;
        }
    }
}