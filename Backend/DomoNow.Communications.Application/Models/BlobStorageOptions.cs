namespace DomoNow.Communications.Application.Models
{
    public class BlobStorageOptions
    {
        public string NameContainer { get; set; } = string.Empty;
        public string AccountName { get; set; } = string.Empty;
        public string AccountKey { get; set; } = string.Empty;
        public string BlobName { get; set; } = string.Empty;
    }
}
