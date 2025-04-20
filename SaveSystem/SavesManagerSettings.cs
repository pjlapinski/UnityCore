using System;
using PJL.SaveSystem.IO;
using PJL.SaveSystem.Serialization;

namespace PJL.SaveSystem {
public class SavesManagerSettings {
    public const string DefaultPreambleSeparator = "3847facf-b237-430b-b4b3-7d388f5d1f26\n";

    private SavesManagerSettings() { }

    public string PreambleSeparator { get; private set; }
    public ISerializationProvider SerializationProvider { get; private set; }
    public IFilesHandler FilesHandler { get; private set; }

    public static SavesManagerSettingsBuilder Create() => new(new SavesManagerSettings());

    public class SavesManagerSettingsBuilder {
        private readonly SavesManagerSettings _target;
        private Type _serializationProviderType;

        public SavesManagerSettingsBuilder(SavesManagerSettings target) => _target = target;

        public SavesManagerSettingsBuilder WithPreambleSeparator(string preambleSeparator) {
            _target.PreambleSeparator = preambleSeparator;
            return this;
        }

        public SavesManagerSettingsBuilder WithSerializationProvider<T>() {
            // _target.SerializationProvider = provider;
            _serializationProviderType = typeof(T);
            return this;
        }

        public SavesManagerSettingsBuilder WithFilesHandler(IFilesHandler handler) {
            _target.FilesHandler = handler;
            return this;
        }

        public static implicit operator SavesManagerSettings(SavesManagerSettingsBuilder builder) =>
            builder.Build();

        private SavesManagerSettings Build() {
            // A generated GUID, to not interfere with file contents
            if (string.IsNullOrEmpty(_target.PreambleSeparator))
                _target.PreambleSeparator = DefaultPreambleSeparator;
            if (_serializationProviderType == null) {
                _target.SerializationProvider = new JsonSerializationProvider(_target.PreambleSeparator);
            } else {
                _target.SerializationProvider = (ISerializationProvider)Activator.CreateInstance(_serializationProviderType, _target.PreambleSeparator);
            }
            _target.FilesHandler ??= new SaveFilesHandler(_target.PreambleSeparator);
            return _target;
        }
    }
}
}
