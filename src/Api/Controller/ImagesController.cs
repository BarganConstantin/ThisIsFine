using Application.Core.Services.ImageUpload;
using Domain.Core.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ThiIsFine.Api.Controller;

[Route("api/[controller]")]
[ApiController]
public class ImagesController(
    IImageUploadService imageUploadService) 
    : ControllerBase
{
    /// <summary>
    /// Upload images and return urls
    /// </summary>
    /// <param name="files"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize(Policy = AccessPolicy.UserAccessPolicy)]
    public async Task<IActionResult> UploadImage(List<IFormFile> files)
    {
        var result = await imageUploadService.Upload(files);
        if (!result.Succeeded) return new JsonResult(result);
        
        var response = result.Data!.Select(
            id =>  Url.Action("GetImage", "Images", new { id }, HttpContext.Request.Scheme));
        return new JsonResult(new { data = response });
    }
    
    /// <summary>
    /// Get image by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetImage(string id)
    {
        var result = await imageUploadService.GetImageById(id);
        if (!result.Succeeded) return new JsonResult(result);
        
        return File(result.Data!.OpenReadStream(), "image/jpeg");
    }
}
