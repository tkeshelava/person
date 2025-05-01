using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using PersonManagment.Application.Abstractions;

namespace PersonManagement.Infrastructure.Storage;

public class FileSystemStorageClient(IOptionsSnapshot<StorageOptions> storageOptionsSnapshot) : IStorageClient
{
    private StorageOptions storageOptions = storageOptionsSnapshot.Value ?? throw new ArgumentNullException(nameof(storageOptionsSnapshot));

    public async Task<string> UploadFileAsync(Stream fileStream, string fileName, CancellationToken cancellationToken)
    {
        string dir = storageOptions.UploadPath;

        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }

        var filePath = Path.Combine(dir, fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await fileStream.CopyToAsync(stream, cancellationToken);
        }

        return GetFileUrl(fileName);
    }

    public Task<bool> DeleteFileAsync(string fileName, CancellationToken cancellationToken)
    {
        var filePath = Path.Combine(storageOptions.UploadPath, fileName);

        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            return Task.FromResult(true);
        }

        return Task.FromResult(false);
    }

    public string GetFileUrl(string fileName)
    {
        return Path.Combine(storageOptions.BaseUrl, fileName).Replace('\\', '/');
    }
}