using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ThiIsFine.Application.Subscriptions.Queries.GetAll.DTOs;
using Web.Models;

namespace Web.Controllers;

public class SubscriptionsController(IMediator mediator) : Controller
{
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var subscriptionsResult = await mediator.Send(new GetAllSubscriptionsQuery());
        if (!subscriptionsResult.Succeeded) return View("BadRequest", subscriptionsResult.Message);
        
        return View(subscriptionsResult.Data!.OrderBy(x => x.CreationTime)
            .Select(SubscriptionViewModel.Create));
    }
}
