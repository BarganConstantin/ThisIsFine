namespace Application.Core.Services.Identity.DTOs;

public class AuthenticateDto
{
    public required string AccessToken { get; set; }
    public required DateTimeOffset AccessTokenExpiration { get; set; }
    public required DateTimeOffset CurrentTime { get; set; }
}
