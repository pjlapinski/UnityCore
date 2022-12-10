using PJL.SaveSystem.IO;
using PJL.SaveSystem.Serialization;

namespace PJL.SaveSystem
{
    public class SavesManagerSettings
    {
        public const string DefaultPreambleSeparator = "3847facf-b237-430b-b4b3-7d388f5d1f26\n";

        public string PreambleSeparator { get; private set; }
        public ISerializationProvider SerializationProvider { get; private set; }
        public IFilesHandler FilesHandler { get; private set; }

        public class SavesManagerSettingsBuilder
        {
            private readonly SavesManagerSettings _target;

            public SavesManagerSettingsBuilder(SavesManagerSettings target)
            {
                _target = target;
            }

            public SavesManagerSettingsBuilder WithPreambleSeparator(string preambleSeparator)
            {
                _target.PreambleSeparator = preambleSeparator;
                return this;
            }

            public SavesManagerSettingsBuilder WithSerializationProvider(ISerializationProvider provider)
            {
                _target.SerializationProvider = provider;
                return this;
            }

            public SavesManagerSettingsBuilder WithFilesHandler(IFilesHandler handler)
            {
                _target.FilesHandler = handler;
                return this;
            }

            public static implicit operator SavesManagerSettings(SavesManagerSettingsBuilder builder) =>
                builder.Build();

            private SavesManagerSettings Build()
            {
                // A generated GUID, to not interfere with file contents
                if (string.IsNullOrEmpty(_target.PreambleSeparator))
                    _target.PreambleSeparator = DefaultPreambleSeparator;
                _target.SerializationProvider ??= new JsonSerializationProvider(_target.PreambleSeparator);
                _target.FilesHandler ??= new SaveFilesHandler(_target.PreambleSeparator);
                return _target;
            }
        }

        private SavesManagerSettings() { }

        public static SavesManagerSettingsBuilder Create() => new(new SavesManagerSettings());
    }
}
