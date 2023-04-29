using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Microsoft.Extensions.Options;
using WebApplication1.Helpers;
using WebApplication1.Interface;

namespace WebApplication1.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly Cloudinary _clondinary;
        public PhotoService(IOptions<CloudinarySettings> config)
        {
            var acc = new Account(
                config.Value.CloudName,
                config.Value.ApiKey,
                config.Value.ApiSecret
                );
            _clondinary = new Cloudinary(acc);
        }
        public async Task<ImageUploadResult> AddPhotoAsync(IFormFile file)
        {
            /*var uploadResult = new ImageUploadResult();
            if (file.Length > 0)
            {
                using var stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    Transformation = new Transformation().Height(500).Width(500).Crop("fill").Gravity("face")
                };
                uploadResult = await _clondinary.UploadAsync(uploadParams);
            }
            
            return uploadResult;*/
            var uploadResult = new ImageUploadResult();
            if (file?.Length > 0)
            {
                using var stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    Transformation = new Transformation().Height(500).Width(500).Crop("fill").Gravity("face")
                };
                uploadResult = await _clondinary.UploadAsync(uploadParams);
            }

            return uploadResult;
        }
        public async Task<ImageUploadResult> AddPhotoCommentAsync(IFormFile file)
        {
            /*var uploadResult = new ImageUploadResult();
            if (file.Length > 0)
            {
                using var stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    Transformation = new Transformation().Height(500).Width(500).Crop("fill").Gravity("face")
                };
                uploadResult = await _clondinary.UploadAsync(uploadParams);
            }
            
            return uploadResult;*/
            var uploadResult = new ImageUploadResult();
            if (file?.Length > 0)
            {
                using var stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    Transformation = new Transformation().Height(980).Width(550).Crop("fill").Gravity("face")
                };
                uploadResult = await _clondinary.UploadAsync(uploadParams);
            }

            return uploadResult;
        }


        public async Task<ImageUploadResult> AddPhotoCommentDepositAsync(IFormFile file)
        {
            /*var uploadResult = new ImageUploadResult();
            if (file.Length > 0)
            {
                using var stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    Transformation = new Transformation().Height(500).Width(500).Crop("fill").Gravity("face")
                };
                uploadResult = await _clondinary.UploadAsync(uploadParams);
            }
            
            return uploadResult;*/
            var uploadResult = new ImageUploadResult();
            if (file?.Length > 0)
            {
                using var stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    Transformation = new Transformation().Height(300).Width(300).Crop("fill").Gravity("face")
                };
                uploadResult = await _clondinary.UploadAsync(uploadParams);
            }

            return uploadResult;
        }
        public async Task<DeletionResult> DeletePhotoAsync(string publicId)
        {
            var deleteParams = new DeletionParams(publicId);
            var result = await _clondinary.DestroyAsync(deleteParams);
            return result;
        }
    }
}
