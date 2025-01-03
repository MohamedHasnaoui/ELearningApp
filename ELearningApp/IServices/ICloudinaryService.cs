﻿using CloudinaryDotNet.Actions;

namespace ELearningApp.IServices
{
    public interface ICloudinaryService
    {
        public Task<VideoUploadResult> UploadVideoAsync(Stream videoStream, string fileName, string notificationUrl = null);
        public Task<ImageUploadResult> UploadImageAsync(Stream imageStream, string fileName);
        public Task<bool> DeleteAsync(string publicId, ResourceType type);
    }
}
