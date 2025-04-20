namespace PJL.SaveSystem.Serialization {
public interface ISerializable {
    public void BeforeSerialization();
    public void AfterDeserialization();

    public void LoadDataFromCopy(object copy) {
        var srcType = GetType();
        var dstType = copy.GetType();

        if (srcType != dstType) {
            return;
        }

        var propInfo = srcType.GetProperties();
        var fieldInfo = srcType.GetFields();

        foreach (var prop in propInfo) {
            if (!prop.CanRead) continue;
            var targetProp = dstType.GetProperty(prop.Name);
            if (targetProp == null) continue;
            if (!targetProp.CanWrite) continue;
            targetProp.SetValue(this, targetProp.GetValue(copy, null), null);
        }

        foreach (var field in fieldInfo) {
            var targetField = dstType.GetField(field.Name);
            if (targetField == null) continue;
            targetField.SetValue(this, field.GetValue(copy));
        }
    }
}
}
