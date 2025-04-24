using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace PJL.SaveSystem.Serialization {
public class JsonSerializationProvider : BaseSerializationProvider {
    public JsonSerializationProvider(string preambleSeparator) : base(preambleSeparator) { }

    public override bool TryDeserialize<T>(string text, out T serializable) {
        var splits = text.Split(PreambleSeparator);
        if (splits.Length < 2) {
            serializable = default;
            return false;
        }
        // in case Split encounters the separator again, which technically is not supposed to happen
        var serializedText = string.Join(string.Empty, splits.Skip(1));

        using var sr = new StringReader(serializedText);
        using var reader = new JsonTextReader(sr);
        var data = Serializer.Deserialize(reader, typeof(T));
        if (data == null) {
            serializable = default;
            return false;
        }
        serializable = (T)data;

        return true;
    }

    public override string Serialize(object serializable, string preamble) {
        StringBuilder.Clear();
        StringBuilder.Append(preamble);
        StringBuilder.Append('\n');
        StringBuilder.Append(PreambleSeparator);

        var sw = new StringWriter();
        using (var writer = new JsonTextWriter(sw)) { Serializer.Serialize(writer, serializable); }

        StringBuilder.Append(sw);
        return StringBuilder.ToString();
    }
}
}
