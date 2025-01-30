namespace ThiIsFine.Application.Users.Queries.GetAll.DTOs;

public record BriefUserDto
{
    public string? Id { get; set; }
    public string? Email { get; set; }
    
    public static BriefUserDto Create(string? id, string? email)
    {
        return new BriefUserDto
        {
            Id = id,
            Email = email,
        };
    }
}
