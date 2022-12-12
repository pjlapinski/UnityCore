using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace PJL.SaveSystem.Serialization {
public class JsonSerializationProvider : BaseSerializationProvider {
  public JsonSerializationProvider(string preambleSeparator) : base(preambleSeparator) { }

  public override bool Deserialize(string text) {
    var splits = text.Split(PreambleSeparator);
    if (splits.Length < 2) return false;
    // in case Split encounters the separator again, which technically is not supposed to happen
    var serializedText = string.Join(string.Empty, splits.Skip(1));

    using var sr = new StringReader(serializedText);
    using var reader = new JsonTextReader(sr);
    var data = Serializer.Deserialize<List<ISerializable>>(reader);
    if (data == null) return false;
    for (var i = 0; i < data.Count; ++i) {
      Subscribers[i].LoadDataFromCopy(data[i]);
      Subscribers[i].AfterDeserialization();
    }

    return true;
  }

  public override string Serialize(string preamble) {
    foreach (var sub in Subscribers)
      sub.BeforeSerialization();
    StringBuilder.Clear();
    StringBuilder.Append(preamble);
    StringBuilder.Append('\n');
    StringBuilder.Append(PreambleSeparator);

    var sw = new StringWriter();
    using (var writer = new JsonTextWriter(sw)) {
      Serializer.Serialize(writer, Subscribers);
    }

    StringBuilder.Append(sw);
    return StringBuilder.ToString();
  }
}
}