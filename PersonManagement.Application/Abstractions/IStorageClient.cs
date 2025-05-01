namespace PersonManagment.Application.Abstractions;

public interface IStorageClient
{
    Task<string> UploadFileAsync(Stream fileStream, string fileName, CancellationToken cancellationToken);
    Task<bool> DeleteFileAsync(string fileName, CancellationToken cancellationToken);
    string GetFileUrl(string fileName);
}