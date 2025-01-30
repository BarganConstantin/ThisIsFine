using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Application.Core.Services.Identity.DTOs;
using Application.Core.Services.TokenGenerator;
using Domain.Core.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ThiIsFine.Domain.Entities.User;
using ThiIsFine.Infrastructure.Identity;

namespace ThiIsFine.Infrastructure.Services.TokenGenerator;

public class TokenService(
        IConfiguration config, 
        UserManager<ThisIsFineUser> userManage)
    : ITokenService
{
    public async Task<AccessTokenDto> GenerateToken(IThisIsFineUser user, CancellationToken cancellationToken)
    {
        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        
        var securityKey = await GetSecurityKey();

        // Create signing credentials
        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.RsaSha256);

        // Set token expiration
        var expiresMinutes = int.Parse(config["JwtSettings:AccessTokenExpiresMinute"]!);
        var expires = DateTime.Now.AddMinutes(expiresMinutes);

        // Create token with specified claims and settings
        var token = tokenHandler.CreateToken(new SecurityTokenDescriptor()
        {
            Issuer = config["JwtSettings:Issuer"],
            Audience = config["JwtSettings:Audience"],
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(UserClaim.NameIdentifier, user.Id.ToString()),
                new Claim(UserClaim.Name, user.UserName!),
                new Claim(UserClaim.Role, (await userManage.GetRolesAsync((ThisIsFineUser)user)).FirstOrDefault() ?? String.Empty),
            }),
            Expires = expires,
            SigningCredentials = signingCredentials
        });
            
        return new AccessTokenDto()
        {
            AccessToken = tokenHandler.WriteToken(token),
            Expiration = expires,
        };
    }
    
    public async Task<RefreshTokenDto> CreateUserRefreshToken()
    {
        var expiresMinutes = int.Parse(config["JwtSettings:RefreshTokenExpiresMinute"]!);
        
        using var rng = RandomNumberGenerator.Create();
        
        var randomNumber = new byte[64];
        rng.GetBytes(randomNumber);
        
        return await Task.FromResult(
            new RefreshTokenDto()
            {
                RefreshToken = Convert.ToBase64String(randomNumber), 
                Expiration = DateTimeOffset.Now.AddMinutes(expiresMinutes)
            });
    }
    
    private async Task<RsaSecurityKey?> GetSecurityKey()
    {
        var rsaKey = RSA.Create();
        rsaKey.ImportRSAPrivateKey(await File.ReadAllBytesAsync(config["JwtSettings:FileWithKeyPath"]!)!, out _);
        return new RsaSecurityKey(rsaKey);
    }
    
    public async Task<string?> ValidateAndGetUsername(string accessToken)
    {
        var validation = new TokenValidationParameters()
        {
            ValidIssuer = config["JwtSettings:Issuer"],
            ValidAudience = config["JwtSettings:Audience"],
            IssuerSigningKey = await GetSecurityKey(),
            ValidateLifetime = false
        };
        
         var principal = new JwtSecurityTokenHandler()
            .ValidateToken(accessToken, validation, out _);
         
         return principal?.Identity?.Name;
    }
}
