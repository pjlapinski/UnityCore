using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json.Bson;

namespace PJL.SaveSystem.Serialization
{
    public class BsonSerializationProvider : BaseSerializationProvider
    {
        public BsonSerializationProvider(string preambleSeparator) : base(preambleSeparator)
        {
        }

        public override bool TryDeserialize<T>(string text, out T serializable)
        {
            var splits = text.Split(PreambleSeparator);
            if (splits.Length < 2)
            {
                serializable = default;
                return false;
            }

            // in case Split encounters the separator again, which technically is not supposed to happen
            var serializedText = string.Join(string.Empty, splits.Skip(1));
            var rawData = Convert.FromBase64String(serializedText);

            var stream = new MemoryStream(rawData);
#pragma warning disable 0618
            using var reader = new BsonReader(stream);
#pragma warning restore 0618
            var data = Serializer.Deserialize(reader, typeof(T));
            if (data == null)
            {
                serializable = default;
                return false;
            }

            serializable = (T)data;

            return true;
        }

        public override string Serialize(object serializable, string preamble)
        {
            StringBuilder.Clear();
            StringBuilder.Append(preamble);
            StringBuilder.Append('\n');
            StringBuilder.Append(PreambleSeparator);

            var stream = new MemoryStream();
#pragma warning disable 0618
            using var writer = new BsonWriter(stream);
#pragma warning restore 0618
            Serializer.Serialize(writer, serializable);
            StringBuilder.Append(Convert.ToBase64String(stream.ToArray()));
            return StringBuilder.ToString();
        }
    }
}