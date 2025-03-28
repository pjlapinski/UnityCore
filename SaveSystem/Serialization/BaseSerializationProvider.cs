using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace PJL.SaveSystem.Serialization {
public abstract class BaseSerializationProvider : ISerializationProvider {
    protected readonly JsonSerializer Serializer;
    protected readonly StringBuilder StringBuilder;

    protected readonly List<ISerializable> Subscribers;

    protected BaseSerializationProvider(string preambleSeparator) {
        PreambleSeparator = preambleSeparator;
        StringBuilder = new StringBuilder();
        Subscribers = new List<ISerializable>();
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

    public void SubscribeForSerialization(ISerializable subscriber) { Subscribers.Add(subscriber); }

    public void UnsubscribeFromSerialization(ISerializable subscriber) { Subscribers.Remove(subscriber); }

    public abstract bool Deserialize(string text);
    public abstract string Serialize(string preamble);
}
}
