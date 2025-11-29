using System.ComponentModel.DataAnnotations;

namespace Dtos.CreateDocumentTypeDtos;

public class DocumentPositionTypeDto
{
    [Required, MaxLength(25)]
    public string Name;
}
