using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using ELearningApp.IServices;

namespace ELearningApp.Services
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly Cloudinary _cloudinary;
        public CloudinaryService(Cloudinary cloudinary)
        {
            _cloudinary = cloudinary;
        }
        public async Task<VideoUploadResult> UploadVideoAsync(Stream videoStream, string fileName, string notificationUrl = null)
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

            return uploadResult;
        }
        public async Task<ImageUploadResult> UploadImageAsync(Stream imageStream, string fileName)
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
            return uploadResult;
        }


        public async Task<string> UploadPostImageAsync(string filePath)
        {
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(filePath)
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);
            return uploadResult.SecureUrl.ToString(); // URL sécurisée de l'image
        }



        public async Task<bool> DeleteAsync(string publicId, ResourceType type)
        {
            var deletionParams = new DeletionParams(publicId)
            {
                ResourceType = type
            };

            var result = await _cloudinary.DestroyAsync(deletionParams);

            if (result.Result == "ok")
            {
                return true;
            }
            return false;
        }
        public async Task<string> TransformImg(string publicId, int width, int height)
        {
            return _cloudinary.Api.UrlImgUp.Transform(
            new Transformation().Width(width).Height(height).Crop("fill")
            ).BuildUrl(publicId) + ".png";
        }
        public async Task<string> TransformVid(string publicId, int width, int hieght)
        {
            return _cloudinary.Api.UrlVideoUp.Transform(new Transformation()
        .Height(hieght).Width(width).Crop("pad")).Secure()
        .BuildUrl(publicId) + ".mp4";
        }
    }
}
