using CloudinaryDotNet.Actions;

namespace WebApplication1.Interface
{
    public interface IPhotoService
    {
        Task<ImageUploadResult> AddPhotoAsync(IFormFile file);
        Task<ImageUploadResult> AddPhotoCommentAsync(IFormFile file);
        Task<ImageUploadResult> AddPhotoCommentDepositAsync(IFormFile file);
        Task<DeletionResult> DeletePhotoAsync(string publicId);


    }
}
