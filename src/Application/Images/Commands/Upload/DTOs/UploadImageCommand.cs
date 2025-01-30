using Application.Core.Responses;
using Microsoft.AspNetCore.Http;

namespace ThiIsFine.Application.Images.Commands.Upload.DTOs;

public sealed record UploadImageCommand(IFormFile Image) : IRequest<Result<UploadImageDto>>;
