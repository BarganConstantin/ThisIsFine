using Application.Core.Services.UserInfo;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using ThiIsFine.Application.Usages.Queries.GetAll.DTOs;
using Web.Models;

namespace Web.Controllers;

public class UsagesController(IMediator mediator, ICurrentUserInfo currentUserInfo) : Controller
{
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var result = await mediator.Send(new GetAllUsagesQuery(currentUserInfo.Id));
        if (!result.Succeeded) return View("BadRequest", result.Message);

        return View(result.Data!.Select(dto => UsageViewModel.Create(dto, Url, HttpContext)));
    }
}
