using Application.Core.Services.Identity.DTOs;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ThiIsFine.Application.Users.Commands.CreateUser.DTOs;
using ThiIsFine.Infrastructure.Identity;
using Web.Models;

namespace Web.Controllers;

public class AccountController(
    IMediator mediator,
    UserManager<ThisIsFineUser> userManager, 
    SignInManager<ThisIsFineUser> signInManager)
    : Controller
{
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            var request = new CreateUserCommand(new RegisterUserDto
                { Email = model.Email, UserName = model.Email, Password = model.Password!});
            
            var result = await mediator.Send(request);
            
            if (!result.Succeeded)
                return View("BadRequest", result.Message);
            
            var user = await userManager.FindByIdAsync(result.Data!.Id!);
            if (user is null) return View("BadRequest", "User not found.");

            await signInManager.SignInAsync(user, isPersistent: false);
            return RedirectToAction("Index", "Home");
        }
        return View(model);
    }
    
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, 
                lockoutOnFailure: false);
            
            if (!result.Succeeded) return View("BadRequest", "Incorrect email or password.");
        }
        return RedirectToAction("Index", "Home");
    }
    
    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
}
