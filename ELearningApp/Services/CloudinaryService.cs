using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using ELearningApp.IServices;

namespace ELearningApp.Services
{
    public class CloudinaryService:ICloudinaryService
    {
        private readonly Cloudinary _cloudinary;
        public CloudinaryService(Cloudinary cloudinary)
        {
            _cloudinary = cloudinary;
        }
        public async Task<string> UploadVideoAsync(Stream videoStream, string fileName, string notificationUrl = null)
        {
            var uniqueId = Guid.NewGuid().ToString();
            var publicId = $"videos/{uniqueId}_{fileName}";

            var uploadParams = new VideoUploadParams
            {
                File = new FileDescription(fileName, videoStream),
                PublicId = publicId,
                Overwrite = true,
                NotificationUrl = notificationUrl
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams); 

            if (uploadResult.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception($"Video upload failed: {uploadResult.Error.Message}");

            return uploadResult.SecureUrl.ToString(); // Return the URL of the uploaded video
        }
        public async Task<string> UploadImageAsync(Stream imageStream, string fileName)
        {
            // Generate a unique ID to ensure no filename conflicts
            var uniqueId = Guid.NewGuid().ToString();
            var publicId = $"images/{uniqueId}_{fileName}";

            // Create upload parameters
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(fileName, imageStream),
                PublicId = publicId,
                Overwrite = true,
            };

            // Upload the image to Cloudinary
            var uploadResult = await _cloudinary.UploadAsync(uploadParams);

            // Check for errors
            if (uploadResult.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception($"Image upload failed: {uploadResult.Error.Message}");

            // Return the URL of the uploaded image
            return uploadResult.SecureUrl.ToString();
        }

    }
}
