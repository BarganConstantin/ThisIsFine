using Application.Core.Responses;
using Application.Core.Services.ImageUpload;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace ThiIsFine.Infrastructure.Services.ImageUpload;

public class CloudinaryImageUploadService : IImageUploadService
{
    const string Cloud = "dvucsju3f";
    const string ApiKey = "173834496749452";
    const string ApiSecret = "3NMFHqW9LB512CsJ1KeT3BY3KIE";

    private readonly Cloudinary _cloudinary;

    public CloudinaryImageUploadService()
    {
        var account = new Account(Cloud, ApiKey, ApiSecret);
        _cloudinary = new Cloudinary(account);
        _cloudinary.Api.Secure = true;
    }

    public async Task<Result<IEnumerable<string>>> Upload(IEnumerable<IFormFile> files, 
        CancellationToken cancellationToken = default)
    {
        var imageUrls = new List<string>();           

        foreach (var file in files)
        {
            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream, cancellationToken);
            memoryStream.Position = 0;

            var uploadparams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, memoryStream),
            };

            var result = _cloudinary.Upload(uploadparams);

            if (result.Error != null)
            {
                throw new Exception($"Cloudinary error occured: {result.Error.Message}");
            }

            imageUrls.Add(result.SecureUrl.ToString());
        }

        return Result.Success<IEnumerable<string>>(imageUrls);
    }

    public Task<Result<IFormFile>> GetImageById(string id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
