using System.ComponentModel.DataAnnotations;

namespace Backend.Entities;

public class DocumentPositionType
{
    public int Id { get; set; }

    [MaxLength(25), Required]
    public string Name { get; set; }
}
