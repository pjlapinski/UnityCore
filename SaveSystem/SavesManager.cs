using System.Collections.Generic;
using PJL.SaveSystem.IO;
using PJL.SaveSystem.Serialization;

namespace PJL.SaveSystem
{
    public class SavesManager
    {
        public static SavesManager Shared { get; } = new();

        public SavesManagerSettings Settings { get; set; }

        public SavesManager() : this(new SavesManagerSettings()) { }
        public SavesManager(SavesManagerSettings settings)
        {
            Settings = settings;
        }

        /// <summary>
        /// Add the serializable object to this manager's pool of subscribers.
        /// </summary>
        public void SubscribeForSerialization(ISerializable subscriber) =>
            Settings.SerializationProvider.SubscribeForSerialization(subscriber);

        /// <summary>
        /// Remove the serializable object from this manager's pool of subscribers.
        /// </summary>
        public void UnsubscribeFromSerialization(ISerializable subscriber) =>
            Settings.SerializationProvider.UnsubscribeFromSerialization(subscriber);

        /// <summary>
        /// Checks whether a save file corresponding to the given save index exists.
        /// </summary>
        public bool SaveExists(int saveIndex) => Settings.FilesHandler.SaveExists(saveIndex);

        /// <summary>
        /// Fetches the data from the file corresponding to the given save index.
        /// </summary>
        /// <returns>The SaveFileData object for the given save.</returns>
        public SaveFileData GetSaveData(int saveIndex) => Settings.FilesHandler.GetSaveFileData(saveIndex);

        /// <summary>
        /// Creates a new save file based on all subscribed objects.
        /// </summary>
        /// <param name="preamble">The file's preamble, that contains information that can be quickly
        /// read, for example save's in-game name.</param>
        public void CreateSave(string preamble = "") => Settings.FilesHandler.Save(Settings.SerializationProvider.Serialize(preamble));

        /// <summary>
        /// Overrides the content of a save file. Will not create a new file, if there is no matching one to override.
        /// </summary>
        /// <param name="saveIndex">The save's index (NOTE: this is not the index of the file
        /// in the system. Instead it indicates that this is the n-th file created. The index
        /// will be the prefix of the file's name)</param>
        /// <param name="preamble">The file's preamble, that contains information that can be quickly
        /// read, for example save's in-game name.</param>
        public void OverrideSave(int saveIndex, string preamble = "") =>
            Settings.FilesHandler.Override(saveIndex, Settings.SerializationProvider.Serialize(preamble));

        /// <summary>
        /// Loads the correct save file and injects loaded data into subscribers, through their
        /// 'GetDataFromCopy' methods.
        /// </summary>
        /// <param name="saveIndex">The save's index (NOTE: this is not the index of the file
        /// in the system. Instead it indicates that this is the n-th file created. The index
        /// will be the prefix of the file's name)</param>
        /// <returns>Whether loading was successful.</returns>
        public bool LoadSave(int saveIndex)
        {
            var success = Settings.FilesHandler.TryLoad(saveIndex, out var data);
            if (success) Settings.SerializationProvider.Deserialize(data);

            return success;
        }

        /// <returns>The SaveFileData objects of all valid files in the saves directory.</returns>
        public IList<SaveFileData> GetAllSaveFilesData() => Settings.FilesHandler.GetAllSaveFilesData();

        /// <returns>The next free index for a save file.</returns>
        public int GetNextSaveIndex() => Settings.FilesHandler.GetNextSaveIndex();

        /// <summary>
        /// Attempts to delete a save file.
        /// </summary>
        /// <param name="saveIndex">The save's index (NOTE: this is not the index of the file
        /// in the system. Instead it indicates that this is the n-th file created. The index
        /// will be the prefix of the file's name)</param>
        /// <returns>Whether the save file was successfully deleted</returns>
        public bool DeleteSave(int saveIndex) => Settings.FilesHandler.Delete(saveIndex);
    }

    public class SavesManagerSettings
    {
        private ISerializationProvider _serializationProvider;
        private IFilesHandler _filesHandler;

        // A generated GUID, to not interfere with file contents
        public string PreambleSeparator { get; set; } = "3847facf-b237-430b-b4b3-7d388f5d1f26\n";
        public ISerializationProvider SerializationProvider
        {
            get
            {
                return _serializationProvider ??= new JsonSerializationProvider(PreambleSeparator);
            }
            set => _serializationProvider = value;
        }

        public IFilesHandler FilesHandler
        {
            get
            {
                return _filesHandler ??= new SaveFilesHandler(PreambleSeparator);
            }
            set => _filesHandler = value;
        }
    }
}