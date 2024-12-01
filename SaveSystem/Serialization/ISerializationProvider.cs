namespace PJL.SaveSystem.Serialization {
public interface ISerializationProvider {
    string PreambleSeparator { get; }
    void SubscribeForSerialization(ISerializable subscriber);
    void UnsubscribeFromSerialization(ISerializable subscriber);
    bool Deserialize(string text);
    string Serialize(string preamble);
}
}
