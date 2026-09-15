using System.Text;
using Newtonsoft.Json;

namespace PJL.SaveSystem.Serialization
{
    public abstract class BaseNewtonsoftSerializationProvider : ISerializationProvider
    {
        protected readonly JsonSerializer Serializer;
        protected readonly StringBuilder StringBuilder;

        protected BaseNewtonsoftSerializationProvider(string preambleSeparator)
        {
            PreambleSeparator = preambleSeparator;
            StringBuilder = new StringBuilder();
            Serializer = JsonSerializer.Create(
                new JsonSerializerSettings
                {
                    // required for serializing Unity types, like Vector3
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    // required for serializing interfaces
                    TypeNameHandling = TypeNameHandling.Auto
                }
            );
        }

        public string PreambleSeparator { get; }

        public abstract bool TryDeserialize<T>(string text, out T serializable);
        public abstract string Serialize(object serializable, string preamble);
    }
}
