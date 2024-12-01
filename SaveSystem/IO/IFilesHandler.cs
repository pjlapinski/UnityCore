using System.Collections.Generic;

namespace PJL.SaveSystem.IO {
public interface IFilesHandler {
    string PreambleSeparator { get; }
    void ChangeFileFormat(string format);
    void ChangeFilesDirectoryName(string name);
    int GetNextSaveIndex();
    bool SaveExists(int saveIndex);
    SaveFileData GetSaveFileData(int saveIndex);
    IList<SaveFileData> GetAllSaveFilesData();
    void Save(string data);
    void Override(int saveIndex, string data);
    bool TryLoad(int saveIndex, out string data);
    bool Delete(int saveIndex);
}
}
