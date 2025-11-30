using System.ComponentModel.DataAnnotations;

namespace Dtos.CreateDocumentTypeDtos;

public class UpdateDocumentPositionTypeDto
{
    [Required, MaxLength(25)]
    public string Name { get; set; }
}
