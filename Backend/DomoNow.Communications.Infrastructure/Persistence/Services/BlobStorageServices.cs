using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using DomoNow.Communications.Application.Models;
using DomoNow.Communications.Application.Services;
using Microsoft.Extensions.Options;

namespace DomoNow.Communications.Infrastructure.Persistence.Services
{
    public class BlobStorageServices(IOptions<BlobStorageOptions> options) : IBlobStorageServices
    {
        private readonly BlobStorageOptions _settings = options.Value;
        private string ConnectionString =>
            $"DefaultEndpointsProtocol=https;AccountName={_settings.AccountName};AccountKey={_settings.AccountKey};EndpointSuffix=core.windows.net";
        public async Task<string> UploadFiles(string nameFile, Stream content, string contentType)
        {
            BlobContainerClient containerClient = new(ConnectionString, _settings.BlobName);
            await containerClient.CreateIfNotExistsAsync();
            BlobClient blobClient = containerClient.GetBlobClient(nameFile);
            await blobClient.UploadAsync(content, new BlobUploadOptions
            {
                HttpHeaders = new BlobHttpHeaders { ContentType = contentType }
            });
            return blobClient.Uri.ToString();
        }
        public async Task RemoveFromUrl(string urlOrPath, string? startFolder = null)
        {
            if (!Uri.TryCreate(urlOrPath, UriKind.Absolute, out Uri uri))
            {
                string fullUrl = $"https://{_settings.AccountName}.blob.core.windows.net/{_settings.BlobName}/{urlOrPath}";
                uri = new Uri(fullUrl);
            }
            string blobPath = uri.AbsolutePath;
            string blobName = blobPath.TrimStart('/').Substring(_settings.BlobName.Length + 1);
            BlobContainerClient containerClient = new(ConnectionString, _settings.BlobName);
            BlobClient blobClient = containerClient.GetBlobClient(blobName);
            await blobClient.DeleteIfExistsAsync();
        }
    }
}
