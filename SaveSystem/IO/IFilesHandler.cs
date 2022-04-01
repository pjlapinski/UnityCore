using System.Collections.Generic;

namespace PJL.SaveSystem.IO
{
    public interface IFilesHandler
    {
        string PreambleSeparator { get; }
        void ChangeFileFormat(string format);
        void ChangeFilesDirectoryName(string name);
        IList<SaveFileData> GetAllSaveFilesData();
        void Save(string data);
        bool TryLoad(int saveIndex, out string data);
    }
}