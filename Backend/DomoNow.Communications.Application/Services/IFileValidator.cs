using Microsoft.AspNetCore.Http;

namespace DomoNow.Communications.Application.Services
{
    public interface IFileValidator
    {
        Task Validate(IFormFile file);
        Task<(bool IsValid, List<string> Errors)> ValidateFiles(IList<IFormFile>? files);
        Task<(bool IsValid, List<string> Errors)> ValidateWithOptions(IFormFile file, FileValidationOptions? options = null);
        Task<(bool IsValid, List<string> Errors)> ValidateFilesWithOptions(IList<IFormFile>? files, FileValidationOptions? options = null);
    }
    public class FileValidationOptions
    {
        public long MaxFileSizeBytes { get; set; } = 50 * 1024 * 1024;
        public int MaxFilesPerPost { get; set; } = 10;
        public List<string> AllowedExtensions { get; set; } = new()
        {
            ".jpg", ".jpeg", ".png", ".gif", ".webp", ".bmp", ".svg", ".tiff", ".pdf"
        };
        public List<string> AllowedMimeTypes { get; set; } = new()
        {
            "image/jpeg", "image/jpg", "image/png", "image/gif", "image/webp",
            "image/bmp", "image/svg+xml", "image/tiff", "application/pdf",
            "application/octet-stream"
        };
        public bool ValidateSignatures { get; set; } = false;
        public bool AllowEmptyFiles { get; set; } = false;
    }
}
