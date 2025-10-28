namespace DomoNow.Communications.Application.Services
{
    public interface IBlobStorageServices
    {
        Task<string> UploadFiles(string nameFile, Stream content, string contentType);
        Task RemoveFromUrl(string url, string? startFolder = null);
    }
}
