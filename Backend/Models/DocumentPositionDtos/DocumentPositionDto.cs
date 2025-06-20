using System.ComponentModel.DataAnnotations;

namespace Backend.Models.DocumentPositionDtos;

internal class DocumentPositionDto
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string NameOfProduct { get; set; }

    [Required]
    [StringLength(15)]
    public string MeasurementUnit { get; set; }

    [Required]
    public int Quantity { get; set; }

    [Required]
    public int AdmissionDocumentId { get; set; }
}
