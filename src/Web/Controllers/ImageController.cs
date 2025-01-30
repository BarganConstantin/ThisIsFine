using Application.Core.Services.ImageUpload;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ThiIsFine.Application.Images.Commands.Upload.DTOs;

namespace Web.Controllers;

public class ImageController(ISender mediatr, IImageUploadService imageUploadService) : Controller
{
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Upload(IFormFile image)
    {
        var result = await mediatr.Send(new UploadImageCommand(image));
        if (!result.Succeeded) return View("BadRequest", result.Message);
        
        var imageUrl = Url.Action("GetImage", "Image", new { id = result.Data!.ImageId }, HttpContext.Request.Scheme);
        return RedirectToAction("ImageLink", new { url = imageUrl });
    }

    [HttpGet]
    public async Task<IActionResult> GetImage(string id)
    {
        var result = await imageUploadService.GetImageById(id);
        if (!result.Succeeded) return new JsonResult(result);
        
        return File(result.Data!.OpenReadStream(), "image/jpeg");
    }
    
    [HttpGet]
    public IActionResult ImageLink(string url)
    {
        ViewBag.ImageUrl = url;
        return View();
    }
}
