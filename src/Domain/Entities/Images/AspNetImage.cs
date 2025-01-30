using Domain.Core.Entities;

namespace ThiIsFine.Domain.Entities.Images;

public class AspNetImage : EntityFullAudited
{
    public required byte[] Bytes { get; set; }
    public required string FileExtension { get; set; }  
    public decimal Size { get; set; }
    
    public static AspNetImage Create(
        byte[] bytes, string fileExtension, decimal size)
    {
        return new AspNetImage
        {
            Bytes = bytes,
            FileExtension = fileExtension,
            Size = size
        };
    }
}
