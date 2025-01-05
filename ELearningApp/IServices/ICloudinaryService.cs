using CloudinaryDotNet.Actions;

namespace ELearningApp.IServices
{
    public interface ICloudinaryService
    {
        public Task<VideoUploadResult> UploadVideoAsync(Stream videoStream, string fileName, string notificationUrl = null);
        public Task<ImageUploadResult> UploadImageAsync(Stream imageStream, string fileName);
        public Task<bool> DeleteAsync(string publicId, ResourceType type);
        public Task<string> TransformImg(string publicId, int width, int height);
        Task<string> TransformVid(string publicId, int width, int hieght);
    }
}
