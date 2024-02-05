namespace CleanArchitecture.Domain.Interfaces.Server
{
    public interface IImageStorageService
    {
        Task<string> UploadImageAsync(byte[] image, string fileName);
        Task DeleteImageAsync(string fileName);
    }
}
