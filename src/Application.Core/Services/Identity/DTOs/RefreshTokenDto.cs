namespace Application.Core.Services.Identity.DTOs;

public class RefreshTokenDto
{
    public required string RefreshToken { get; set; }
    public required DateTimeOffset Expiration { get; set; }
}
