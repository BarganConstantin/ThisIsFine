using Application.Core.Services.UserInfo;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ThiIsFine.Application.Purchases.Create.DTOs;
using ThiIsFine.Application.Purchases.Queries.GetAll.DTOs;
using Web.Models;

namespace Web.Controllers;

public sealed class PurchasesController(ISender mediator, ICurrentUserInfo currentUserInfo) : Controller
{
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var result = await mediator.Send(new GetAllPurchasesQuery(currentUserInfo.Id));
        if (!result.Succeeded) return View("BadRequest", result.Message);

        return View(result.Data!.Select(PurchaseViewModel.Create));
    }
    
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Buy(string subscriptionId)
    {
        var result = await mediator.Send(new CreatePurchaseCommand(new CreatePurchaseDto(subscriptionId)));
        if (!result.Succeeded) return View("BadRequest", result.Message);

        return RedirectToAction("Index", "Purchases");
    }
}
