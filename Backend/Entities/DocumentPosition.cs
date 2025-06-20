using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Entities;

public class DocumentPosition
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

    [Required]
    [ForeignKey("AdmissionDocumentId")]
    public AdmissionDocument AdmissionDocument { get; set; }
}
