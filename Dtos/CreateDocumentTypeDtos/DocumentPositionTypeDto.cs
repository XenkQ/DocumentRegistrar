using System.ComponentModel.DataAnnotations;

namespace Dtos.CreateDocumentTypeDtos;

public class DocumentPositionTypeDto
{
    public int Id { get; set; }

    [Required, MaxLength(25)]
    public string Name { get; set; }
}
