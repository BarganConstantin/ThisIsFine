using Application.Core.Responses;
using Microsoft.AspNetCore.Http;

namespace Application.Core.Services.ImageUpload;

public interface IImageUploadService
{
    public Task<Result<IEnumerable<string>>> Upload(IEnumerable<IFormFile> files,
        CancellationToken cancellationToken = default);
    public Task<Result<IFormFile>> GetImageById(string id, CancellationToken cancellationToken = default);
}
