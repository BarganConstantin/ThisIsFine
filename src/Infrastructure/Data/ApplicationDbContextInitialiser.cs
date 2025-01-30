using Domain.Core.Constants;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ThiIsFine.Infrastructure.Identity;

namespace ThiIsFine.Infrastructure.Data;

public static class InitialiserExtensions
{
    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();

        await initialiser.InitialiseAsync();

        await initialiser.SeedAsync();
    }
}

public class ApplicationDbContextInitialiser
{
    private readonly ILogger<ApplicationDbContextInitialiser> _logger;
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ThisIsFineUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public ApplicationDbContextInitialiser(
        ILogger<ApplicationDbContextInitialiser> logger, 
        ApplicationDbContext context, 
        UserManager<ThisIsFineUser> userManager, 
        RoleManager<IdentityRole> roleManager)
    {
        _logger = logger;
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            await _context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        var superAdminRole = new IdentityRole(Roles.SuperAdmin);
        var adminRole = new IdentityRole(Roles.Admin);
        var moderatorRole = new IdentityRole(Roles.Moderator);
        var userRole = new IdentityRole(Roles.User);

        if (_roleManager.Roles.All(r => r.Name != superAdminRole.Name))
            await _roleManager.CreateAsync(superAdminRole);
        if (_roleManager.Roles.All(r => r.Name != adminRole.Name))
            await _roleManager.CreateAsync(adminRole);
        if (_roleManager.Roles.All(r => r.Name != moderatorRole.Name))
            await _roleManager.CreateAsync(moderatorRole);
        if (_roleManager.Roles.All(r => r.Name != userRole.Name))
            await _roleManager.CreateAsync(userRole);
        
        var passwordHasher = new PasswordHasher<ThisIsFineUser>();
        
        // Default users
        var superAdmin = new ThisIsFineUser
        {
            Id = Guid.NewGuid().ToString(),
            UserName = "superAdmin", 
            Email = "superAdmin@localhost",
            CreationTime = DateTimeOffset.Now,
            FreeTrialAttempts = 3,
        };
        superAdmin.SecurityStamp = superAdmin.Id;
        
        if (_userManager.Users.All(u => u.UserName != superAdmin.UserName))
        {
            await _userManager.CreateAsync(superAdmin, "Qwe123!");
            if (!string.IsNullOrWhiteSpace(superAdminRole.Name))
            {
                await _userManager.AddToRolesAsync(superAdmin, new [] { superAdminRole.Name });
            }
        }
        
        var admin = new ThisIsFineUser
        {
            Id = Guid.NewGuid().ToString(),
            UserName = "admin", 
            Email = "admin@localhost",
            CreationTime = DateTimeOffset.Now,
            FreeTrialAttempts = 3,
        };
        admin.SecurityStamp = admin.Id;
        
        if (_userManager.Users.All(u => u.UserName != admin.UserName))
        {
            await _userManager.CreateAsync(admin, "Qwe123!");
            if (!string.IsNullOrWhiteSpace(adminRole.Name))
            {
                await _userManager.AddToRolesAsync(admin, new [] { adminRole.Name });
            }
        }
        
        var moderator = new ThisIsFineUser
        {
            Id = Guid.NewGuid().ToString(),
            UserName = "moderator", 
            Email = "moderator@localhost",
            CreationTime = DateTimeOffset.Now,
            FreeTrialAttempts = 3,
        };
        moderator.SecurityStamp = moderator.Id;
        
        if (_userManager.Users.All(u => u.UserName != moderator.UserName))
        {
            await _userManager.CreateAsync(moderator, "Qwe123!");
            if (!string.IsNullOrWhiteSpace(moderatorRole.Name))
            {
                await _userManager.AddToRolesAsync(moderator, new [] { moderatorRole.Name });
            }
        }
        
        var user = new ThisIsFineUser
        {
            Id = Guid.NewGuid().ToString(),
            UserName = "user", 
            Email = "user@localhost",
            CreationTime = DateTimeOffset.Now,
            FreeTrialAttempts = 3,
        };
        user.SecurityStamp = user.Id;
        
        if (_userManager.Users.All(u => u.UserName != user.UserName))
        {
            await _userManager.CreateAsync(user, "Qwe123!");
            if (!string.IsNullOrWhiteSpace(userRole.Name))
            {
                await _userManager.AddToRolesAsync(user, new [] { userRole.Name });
            }
        }
    }
}
