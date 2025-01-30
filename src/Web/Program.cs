using Microsoft.AspNetCore.Identity;
using ThiIsFine.Api.Extensions.Config;
using ThiIsFine.Infrastructure.Data;
using ThiIsFine.Infrastructure.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediatrConfig();
builder.Services.AddServicesConfig();
builder.Services.ConfigureDatabaseServices(builder.Configuration);
builder.Services.AddWebConfig();
builder.Services.AddSwaggerConfiguration();

builder.Services.AddProblemDetails();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddIdentity<ThisIsFineUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    await app.InitialiseDatabaseAsync();
}
else
{
    await app.InitialiseDatabaseAsync();
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
