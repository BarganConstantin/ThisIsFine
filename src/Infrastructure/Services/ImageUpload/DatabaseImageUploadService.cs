using Application.Core.Responses;
using Application.Core.Responses.Enum;
using Application.Core.Services.ImageUpload;
using Application.Core.Services.UserInfo;
using Microsoft.AspNetCore.Http;
using ThiIsFine.Application.Repositories;
using ThiIsFine.Domain.Entities.Images;

namespace ThiIsFine.Infrastructure.Services.ImageUpload;

public class DatabaseImageUploadService(
    IApplicationUnitOfWork applicationUnitOfWork,
    ICurrentUserInfo currentUserInfo)
    : IImageUploadService
{
    public async Task<Result<IEnumerable<string>>> Upload(
        IEnumerable<IFormFile> files, CancellationToken cancellationToken)
    {
        return applicationUnitOfWork.HasActiveTransaction()
            ? await UploadWithoutUnitOfWork(files, cancellationToken)
            : await UploadInUnitOfWork(files, cancellationToken);
    }
    
    private async Task<Result<IEnumerable<string>>> UploadInUnitOfWork(
        IEnumerable<IFormFile> files, CancellationToken cancellationToken)
    {
        try
        {
            await applicationUnitOfWork.CreateTransaction(cancellationToken);
        
            var uploadResult = await UploadWithoutUnitOfWork(files, cancellationToken);
            if (!uploadResult.Succeeded) return uploadResult;

            await applicationUnitOfWork.CommitAsync(cancellationToken);
            return Result.Success(uploadResult.Data!);
        }
        catch (Exception)
        {
            await applicationUnitOfWork.Rollback(cancellationToken);
            return new Result<IEnumerable<string>>()
            {
                Message = "Error uploading image",
                ResultStatus = ResultStatus.InternalServerError
            };
        }
    }
    
    private async Task<Result<IEnumerable<string>>> UploadWithoutUnitOfWork(
        IEnumerable<IFormFile> files, CancellationToken cancellationToken)
    {
        var imageIds = new List<string>();           

        foreach (var file in files)
        {
            byte[] fileBytes;
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream, cancellationToken);
                fileBytes = memoryStream.ToArray();
            }
        
            var image = AspNetImage.Create(
                bytes: fileBytes,
                fileExtension: Path.GetExtension(file.FileName),
                size: file.Length);
        
            await applicationUnitOfWork.ImageRepository
                .Insert(image, currentUserInfo.Id, cancellationToken);
        
            imageIds.Add(image.Id);
        }

        await applicationUnitOfWork.SaveAsync(cancellationToken);
        return Result.Success<IEnumerable<string>>(imageIds);
    }
    
    public async Task<Result<IFormFile>> GetImageById(string id, CancellationToken cancellationToken)
    {
        var imageResult = await applicationUnitOfWork.ImageRepository.GetById(id, cancellationToken);
        if (!imageResult.Succeeded) return Result.NotFound<IFormFile>("Image not found");

        var imageStream = new MemoryStream(imageResult.Data!.Bytes);

        var formFile = new FormFile(imageStream, 0, imageResult.Data!.Bytes.Length, "image", "image.jpg")
        {
            Headers = new HeaderDictionary(),
            ContentType = imageResult.Data!.FileExtension
        };

        return Result.Success<IFormFile>(formFile);
    }
}
