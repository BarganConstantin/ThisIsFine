namespace Application.Core.Services.Identity.DTOs;

public class AccessTokenDto
{
    public required string AccessToken { get; set; }
    public required DateTimeOffset Expiration { get; set; }
}
