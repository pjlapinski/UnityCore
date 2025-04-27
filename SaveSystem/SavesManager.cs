using System.Collections.Generic;
using PJL.SaveSystem.IO;

namespace PJL.SaveSystem
{
    public class SavesManager
    {
        public SavesManager() : this(SavesManagerSettings.Create()) { }

        public SavesManager(SavesManagerSettings settings)
        {
            Settings = settings;
        }

        public static SavesManager Shared { get; } = new();

        public SavesManagerSettings Settings { get; set; }

        /// <summary>
        ///     Checks whether a save file corresponding to the given save index exists.
        /// </summary>
        public bool SaveExists(int saveIndex) => Settings.FilesHandler.SaveExists(saveIndex);

        /// <summary>
        ///     Fetches the data from the file corresponding to the given save index.
        /// </summary>
        /// <returns>The SaveFileData object for the given save.</returns>
        public SaveFileData GetSaveData(int saveIndex) => Settings.FilesHandler.GetSaveFileData(saveIndex);

        /// <summary>
        ///     Creates a new save file based on all subscribed objects.
        /// </summary>
        /// <param name="serializable">
        ///     The object to be serialized.
        /// </param>
        /// <param name="preamble">
        ///     The file's preamble, that contains information that can be quickly
        ///     read, for example save's in-game name.
        /// </param>
        public void Save(object serializable, string preamble = "") =>
            Settings.FilesHandler.Save(Settings.SerializationProvider.Serialize(serializable, preamble));

        /// <summary>
        ///     Creates a new save file based on all subscribed objects. Will override exisitng save
        ///     at index <see cref="saveIndex" />.
        /// </summary>
        /// <param name="saveIndex">
        ///     The save's index (NOTE: this is not the index of the file
        ///     in the system. Instead it indicates that this is the n-th file created. The index
        ///     will be the prefix of the file's name)
        /// </param>
        /// <param name="serializable">
        ///     The object to be serialized.
        /// </param>
        /// <param name="preamble">
        ///     The file's preamble, that contains information that can be quickly
        ///     read, for example save's in-game name.
        /// </param>
        public void Save(int saveIndex, object serializable, string preamble = "") =>
            Settings.FilesHandler.Save(saveIndex, Settings.SerializationProvider.Serialize(serializable, preamble));

        /// <summary>
        ///     Loads the correct save file and injects loaded data into subscribers, through their
        ///     'GetDataFromCopy' methods.
        /// </summary>
        /// <param name="saveIndex">
        ///     The save's index (NOTE: this is not the index of the file
        ///     in the system. Instead it indicates that this is the n-th file created. The index
        ///     will be the prefix of the file's name)
        /// </param>
        /// <param name="serializable">
        ///     The object containing the deserialized data. Will be set to default(T) if loading failed.
        /// </param>
        /// <returns>Whether loading was successful.</returns>
        public bool LoadSave<T>(int saveIndex, out T serializable)
        {
            var success = Settings.FilesHandler.TryLoad(saveIndex, out var data);
            if (success && Settings.SerializationProvider.TryDeserialize<T>(data, out var obj))
            {
                serializable = obj;
                return true;
            }

            serializable = default;
            return false;
        }

        /// <returns>The SaveFileData objects of all valid files in the saves directory.</returns>
        public IList<SaveFileData> GetAllSaveFilesData() => Settings.FilesHandler.GetAllSaveFilesData();

        /// <returns>The next free index for a save file.</returns>
        public int GetNextSaveIndex() => Settings.FilesHandler.GetNextSaveIndex();

        /// <summary>
        ///     Attempts to delete a save file.
        /// </summary>
        /// <param name="saveIndex">
        ///     The save's index (NOTE: this is not the index of the file
        ///     in the system. Instead it indicates that this is the n-th file created. The index
        ///     will be the prefix of the file's name)
        /// </param>
        /// <returns>Whether the save file was successfully deleted</returns>
        public bool DeleteSave(int saveIndex) => Settings.FilesHandler.Delete(saveIndex);
    }
}
