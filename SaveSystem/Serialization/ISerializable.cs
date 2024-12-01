namespace PJL.SaveSystem.Serialization {
public interface ISerializable {
    public void BeforeSerialization();
    public void AfterDeserialization();
    public void LoadDataFromCopy(ISerializable copy);
}
}
