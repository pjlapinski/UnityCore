namespace PJL.SaveSystem.Serialization {
public interface ISerializationProvider {
    string PreambleSeparator { get; }
    bool TryDeserialize<T>(string text, out T serializable);
    string Serialize(object serializable, string preamble);
}
}
