namespace PJL.SaveSystem.Serialization {
public interface ISerializationProvider {
    string PreambleSeparator { get; }
    void SetSaveObject(ISerializable saveObject);
    bool Deserialize(string text);
    string Serialize(string preamble);
}
}
