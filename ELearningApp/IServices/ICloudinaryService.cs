namespace ELearningApp.IServices
{
    public interface ICloudinaryService
    {
        public Task<string> UploadVideoAsync(Stream videoStream, string fileName, string notificationUrl = null);
        public Task<string> UploadImageAsync(Stream imageStream, string fileName);
    }
}
