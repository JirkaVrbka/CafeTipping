namespace CaffeTipping.WebCommon.Options;

public sealed class StorageOptions
{
    public StorageOptionsValue Storage { get; set; }
}

public enum StorageOptionsValue
{
    Database,
    File
}