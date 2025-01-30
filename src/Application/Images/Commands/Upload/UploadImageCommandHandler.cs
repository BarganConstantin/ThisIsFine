using Application.Core.Responses;
using Application.Core.Responses.Enum;
using Application.Core.Services.Identity;
using Application.Core.Services.ImageUpload;
using Application.Core.Services.UserInfo;
using Microsoft.AspNetCore.Http;
using ThiIsFine.Application.Images.Commands.Upload.DTOs;
using ThiIsFine.Application.Repositories;
using ThiIsFine.Domain.Entities.Purchases;
using ThiIsFine.Domain.Entities.Usages;

namespace ThiIsFine.Application.Images.Commands.Upload;

public sealed class UploadImageCommandHandler(
    IApplicationUnitOfWork applicationUnitOfWork,
    ICurrentUserInfo currentUserInfo,
    IIdentityService identityService,
    IImageUploadService imageUploadService)
    : IRequestHandler<UploadImageCommand, Result<UploadImageDto>>
{
    public async Task<Result<UploadImageDto>> Handle(UploadImageCommand request, CancellationToken cancellationToken)
    {
        var validationResult = ValidateRequestModel(request);
        if (!validationResult.Succeeded) return validationResult.ConvertTo<UploadImageDto>();

        var hasFreeAttempts = await CheckUserFreeAttempts(cancellationToken);
        if (!hasFreeAttempts.Succeeded) return hasFreeAttempts.ConvertTo<UploadImageDto>();

        return hasFreeAttempts.Data
            ? await ExecuteUploadBasedOnFreeTrial(request, cancellationToken)
            : await ExecuteUploadBasedOnPurchase(request, cancellationToken);
    }

    private async Task<Result<UploadImageDto>> ExecuteUploadBasedOnFreeTrial(UploadImageCommand request,
        CancellationToken cancellationToken)
    {
        await applicationUnitOfWork.CreateTransaction(cancellationToken);

        var uploadResult = await imageUploadService.Upload(new List<IFormFile> { request.Image }, cancellationToken);
        if (!uploadResult.Succeeded) return uploadResult.ConvertTo<UploadImageDto>();

        var usage = Usage.Create(currentUserInfo.Id, purchaseId: null, uploadResult.Data!.First());
        var usageResult =
            await applicationUnitOfWork.UsagesRepository.Insert(usage, currentUserInfo.Id, cancellationToken);
        if (!usageResult.Succeeded) return usageResult.ConvertTo<UploadImageDto>();

        var decrementResult = await DecrementFreeTrialAttempts(currentUserInfo.Id, cancellationToken);
        if (!decrementResult.Succeeded) return decrementResult.ConvertTo<UploadImageDto>();

        await applicationUnitOfWork.CommitAsync(cancellationToken);

        return Result.Success(new UploadImageDto(uploadResult.Data!.First()));
    }

    private async Task<Result<UploadImageDto>> ExecuteUploadBasedOnPurchase(UploadImageCommand request,
        CancellationToken cancellationToken)
    {
        await applicationUnitOfWork.CreateTransaction(cancellationToken);

        var purchaseResult = await CheckPurchaseWithAvailableAttempts(cancellationToken);
        if (!purchaseResult.Succeeded) return purchaseResult.ConvertTo<UploadImageDto>();

        var uploadResult = await imageUploadService.Upload(new List<IFormFile> { request.Image }, cancellationToken);
        if (!uploadResult.Succeeded) return uploadResult.ConvertTo<UploadImageDto>();

        var usage = Usage.Create(currentUserInfo.Id, purchaseId: purchaseResult.Data!.Id, uploadResult.Data!.First());
        var usageResult =
            await applicationUnitOfWork.UsagesRepository.Insert(usage, currentUserInfo.Id, cancellationToken);
        if (!usageResult.Succeeded) return usageResult.ConvertTo<UploadImageDto>();

        var decrementResult = await DecrementPurchaseAttempts(purchaseResult.Data!, cancellationToken);
        if (!decrementResult.Succeeded) return decrementResult.ConvertTo<UploadImageDto>();

        await applicationUnitOfWork.CommitAsync(cancellationToken);

        return Result.Success(new UploadImageDto(uploadResult.Data!.First()));
    }

    private async Task<Result<Purchase>> CheckPurchaseWithAvailableAttempts(CancellationToken cancellationToken)
    {
        var purchase = await (await applicationUnitOfWork.PurchasesRepository.GetAll(cancellationToken))
            .Where(x => x.UserId == currentUserInfo.Id && x.RemainingAttempts > 0)
            .FirstOrDefaultAsync(cancellationToken);

        return purchase == null
            ? Result.BadRequest<Purchase>("No purchase with available attempts")
            : Result.Success(purchase);
    }

    private async Task<Result> DecrementPurchaseAttempts(Purchase purchase, CancellationToken cancellationToken)
    {
        purchase.DecrementRemainingAttempts();
        var result =
            await applicationUnitOfWork.PurchasesRepository.Update(purchase, currentUserInfo.Id, cancellationToken);

        return !result.Succeeded
            ? Result.BadRequest("Error updating purchase")
            : new Result() { ResultStatus = ResultStatus.Success };
    }

    private static Result<UploadImageCommand> ValidateRequestModel(UploadImageCommand? request)
    {
        if (request?.Image == null)
            return Result.BadRequest<UploadImageCommand>("Image is required");

        if (request.Image.ContentType != "image/jpeg" && request.Image.ContentType != "image/png")
            return Result.BadRequest<UploadImageCommand>("Image must be jpeg or png");

        return Result.Success(request);
    }

    private async Task<Result<bool>> CheckUserFreeAttempts(CancellationToken cancellationToken)
    {
        var user = await identityService.GetUserById(currentUserInfo.Id, cancellationToken);
        if (!user.Succeeded) return Result.BadRequest<bool>("Error getting user");

        return user.Data!.FreeTrialAttempts > 0
            ? Result.Success(true)
            : Result.Success(false);
    }

    private async Task<Result> DecrementFreeTrialAttempts(string userId, CancellationToken cancellationToken)
    {
        var user = await identityService.GetUserById(userId, cancellationToken);
        if (!user.Succeeded) return Result.BadRequest("Error getting user");

        user.Data!.FreeTrialAttempts--;
        var result = await identityService.UpdateUserAsync(user.Data!, cancellationToken);

        return !result.Succeeded
            ? Result.BadRequest("Error updating user")
            : new Result() { ResultStatus = ResultStatus.Success };
    }
}
