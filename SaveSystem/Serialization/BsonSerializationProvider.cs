using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json.Bson;

namespace PJL.SaveSystem.Serialization
{
    public class BsonSerializationProvider : BaseSerializationProvider
    {
        public BsonSerializationProvider(string preambleSeparator) : base(preambleSeparator)
        { }

        [Serializable]
        private class JsonList
        {
            public List<ISerializable> List;
        }

        public override bool Deserialize(string text)
        {
            var splits = text.Split(PreambleSeparator);
            if (splits.Length < 2) return false;
            // in case Split encounters the separator again, which technically is not supposed to happen
            var serializedText = string.Join(string.Empty, splits.Skip(1));
            var rawData = Convert.FromBase64String(serializedText);

            var stream = new MemoryStream(rawData);
#pragma warning disable 0618
            using var reader = new BsonReader(stream);
#pragma warning restore 0618
            var data = Serializer.Deserialize<JsonList>(reader);
            if (data == null) return false;
            for (var i = 0; i < data.List.Count; ++i)
            {
                Subscribers[i].LoadDataFromCopy(data.List[i]);
                Subscribers[i].AfterDeserialization();
            }

            return true;
        }

        public override string Serialize(string preamble)
        {
            foreach (var sub in Subscribers)
                sub.BeforeSerialization();
            StringBuilder.Clear();
            StringBuilder.Append(preamble);
            StringBuilder.Append('\n');
            StringBuilder.Append(PreambleSeparator);

            var stream = new MemoryStream();
#pragma warning disable 0618
            using var writer = new BsonWriter(stream);
#pragma warning restore 0618
            var list = new JsonList {List = Subscribers};
            Serializer.Serialize(writer, list);
            StringBuilder.Append(Convert.ToBase64String(stream.ToArray()));
            return StringBuilder.ToString();
        }
    }
}