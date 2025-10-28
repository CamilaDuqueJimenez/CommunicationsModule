using DomoNow.Communications.Application.Services;
using Microsoft.AspNetCore.Http;

namespace DomoNow.Communications.Infrastructure.Persistence.Services
{
    public class FileValidatorService : IFileValidator
    {
        private readonly FileValidationOptions _defaultOptions;
        private static readonly Dictionary<string, byte[][]> FileSignatures = new()
        {
            { ".pdf", new[] { "%PDF"u8.ToArray() } },
            { ".jpg", new[] { new byte[] { 0xFF, 0xD8 } } },
            { ".jpeg", new[] { new byte[] { 0xFF, 0xD8 } } },
            { ".png", new[] { new byte[] { 0x89, 0x50, 0x4E, 0x47 } } },
            { ".gif", new[] { "GIF"u8.ToArray() } },
            { ".webp", new[] { "RIFF"u8.ToArray() } }
        };
        public FileValidatorService()
        {
            _defaultOptions = new FileValidationOptions();
        }
        public async Task Validate(IFormFile file)
        {
            var result = await ValidateWithOptions(file, _defaultOptions);
            if (!result.IsValid)
            {
                throw new InvalidOperationException(string.Join("; ", result.Errors));
            }
        }
        public async Task<(bool IsValid, List<string> Errors)> ValidateFiles(IList<IFormFile>? files)
        {
            return await ValidateFilesWithOptions(files, _defaultOptions);
        }
        public async Task<(bool IsValid, List<string> Errors)> ValidateWithOptions(IFormFile file, FileValidationOptions? options = null)
        {
            var validationOptions = options ?? _defaultOptions;
            var errors = new List<string>();
            if (file == null)
            {
                errors.Add("El archivo es nulo");
                return (false, errors);
            }
            if (file.Length == 0)
            {
                if (!validationOptions.AllowEmptyFiles)
                {
                    errors.Add($"El archivo '{file.FileName}' está vacío");
                }
                return (errors.Count == 0, errors);
            }
            if (file.Length > validationOptions.MaxFileSizeBytes)
            {
                var maxSizeMB = validationOptions.MaxFileSizeBytes / (1024.0 * 1024.0);
                var fileSizeMB = file.Length / (1024.0 * 1024.0);
                errors.Add($"El archivo '{file.FileName}' excede el tamaño máximo de {maxSizeMB:F1}MB. Tamaño actual: {fileSizeMB:F1}MB");
            }
            var extension = Path.GetExtension(file.FileName)?.ToLowerInvariant();
            if (validationOptions.AllowedExtensions?.Any() == true)
            {
                if (string.IsNullOrWhiteSpace(extension) || !validationOptions.AllowedExtensions.Contains(extension))
                {
                    errors.Add($"Extensión '{extension}' no permitida para '{file.FileName}'. Extensiones válidas: {string.Join(", ", validationOptions.AllowedExtensions)}");
                }
            }
            if (validationOptions.AllowedMimeTypes?.Any() == true)
            {
                if (string.IsNullOrWhiteSpace(file.ContentType) || !validationOptions.AllowedMimeTypes.Contains(file.ContentType.ToLowerInvariant()))
                {
                    errors.Add($"Tipo de contenido '{file.ContentType}' no permitido para '{file.FileName}'. Tipos válidos: {string.Join(", ", validationOptions.AllowedMimeTypes)}");
                }
            }
            if (validationOptions.ValidateSignatures && !string.IsNullOrWhiteSpace(extension))
            {
                var signatureErrors = await ValidateFileSignature(file, extension);
                errors.AddRange(signatureErrors);
            }
            return (errors.Count == 0, errors);
        }
        public async Task<(bool IsValid, List<string> Errors)> ValidateFilesWithOptions(IList<IFormFile>? files, FileValidationOptions? options = null)
        {
            var validationOptions = options ?? _defaultOptions;
            var errors = new List<string>();
            if (files == null || !files.Any())
            {
                return (true, errors);
            }
            if (files.Count > validationOptions.MaxFilesPerPost)
            {
                errors.Add($"Máximo {validationOptions.MaxFilesPerPost} archivos permitidos. Se recibieron {files.Count}");
            }
            for (int i = 0; i < files.Count; i++)
            {
                var result = await ValidateWithOptions(files[i], validationOptions);
                if (!result.IsValid)
                {
                    var fileErrors = result.Errors.Select(error => $"Archivo {i + 1}: {error}");
                    errors.AddRange(fileErrors);
                }
            }
            return (errors.Count == 0, errors);
        }
        private async Task<List<string>> ValidateFileSignature(IFormFile file, string extension)
        {
            var errors = new List<string>();
            if (!FileSignatures.ContainsKey(extension))
            {
                return errors;
            }
            try
            {
                using var stream = file.OpenReadStream();
                using var reader = new BinaryReader(stream);
                var signatures = FileSignatures[extension];
                var maxHeaderSize = signatures.Max(s => s.Length);
                var headerBytes = reader.ReadBytes(Math.Min(maxHeaderSize, (int)stream.Length));
                bool isValid = signatures.Any(signature =>
                    headerBytes.Take(signature.Length).SequenceEqual(signature));
                if (!isValid)
                {
                    errors.Add($"La firma del archivo '{file.FileName}' no coincide con el tipo esperado ({extension})");
                }
                stream.Position = 0;
            }
            catch (Exception ex)
            {
                errors.Add($"Error al validar la firma del archivo '{file.FileName}': {ex.Message}");
            }
            return errors;
        }
    }
}
