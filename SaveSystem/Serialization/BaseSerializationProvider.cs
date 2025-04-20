using System.Text;
using Newtonsoft.Json;

namespace PJL.SaveSystem.Serialization {
public abstract class BaseSerializationProvider : ISerializationProvider {
    protected readonly JsonSerializer Serializer;
    protected readonly StringBuilder StringBuilder;

    protected ISerializable SaveObject;

    protected BaseSerializationProvider(string preambleSeparator) {
        PreambleSeparator = preambleSeparator;
        StringBuilder = new StringBuilder();
        SaveObject = null;
        Serializer = JsonSerializer.Create(
            new JsonSerializerSettings {
                // required for serializing Unity types, like Vector3
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                // required for serializing interfaces
                TypeNameHandling = TypeNameHandling.Auto,
            }
        );
    }

    public string PreambleSeparator { get; }

    public void SetSaveObject(ISerializable saveObject) => SaveObject = saveObject;

    public abstract bool Deserialize(string text);
    public abstract string Serialize(string preamble);
}
}
