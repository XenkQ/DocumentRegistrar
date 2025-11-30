using System.ComponentModel.DataAnnotations;

namespace Dtos.CreateDocumentTypeDtos;

public class CreateDocumentPositionTypeDto
{
    [Required, MaxLength(25)]
    public string Name { get; set; }
}
