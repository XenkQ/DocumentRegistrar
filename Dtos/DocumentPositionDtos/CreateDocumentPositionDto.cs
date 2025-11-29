using System.ComponentModel.DataAnnotations;

namespace Dtos.DocumentPositionDtos;

public class CreateDocumentPositionDto
{
    [Required]
    [StringLength(100)]
    public string NameOfProduct { get; set; }

    [Required]
    [StringLength(15)]
    public string MeasurementUnit { get; set; }

    [Required]
    public int Quantity { get; set; }

    [Required]
    public decimal UnitPrice { get; set; }

    [Required]
    public int AdmissionDocumentId { get; set; }

    [Required]
    public int DocumentPositionTypeId { get; set; }
}
