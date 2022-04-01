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

        private int GetNextSaveIndex()
        {
            EnsureSavesDirectoryExists();
            var files = Directory.GetFiles(SavesPath);
            var idx = 0;

            foreach (var file in files)
            {
                if (!file.EndsWith(_fileFormat)) continue;
                var filename = GetFileName(file);
                if (int.TryParse(filename[..^_fileFormat.Length], out var fileIdx))
                    idx = fileIdx + 1;
            }

            return idx;
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
                var preambleBuilder = new StringBuilder();

                using (var reader = new StreamReader(file))
                {
                    string line;

                    while ((line = reader.ReadLine()) != null && line != PreambleSeparator)
                    {
                        preambleBuilder.Append(line);
                    }
                }

                results.Add(new SaveFileData(fileIdx, preambleBuilder.ToString()));
            }

            return results;
        }

        public void Save(string data)
        {
            EnsureSavesDirectoryExists();
            var idx = GetNextSaveIndex();
            using var writer = new StreamWriter(GetSaveFullPath(idx));
            writer.Write(data);
        }

        public bool TryLoad(int saveIndex, out string data)
        {
            EnsureSavesDirectoryExists();
            var files = Directory.GetFiles(SavesPath);

            foreach (var file in files)
            {
                if (!file.EndsWith(_fileFormat)) continue;
                var filename = GetFileName(file);
                if (int.TryParse(filename[..^_fileFormat.Length], out var fileIdx) && fileIdx == saveIndex)
                {
                    data = File.ReadAllText(file);
                    return true;
                }
            }

            data = null;
            return false;
        }
    }
}