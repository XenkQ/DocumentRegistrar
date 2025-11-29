using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Entities;

public class DocumentPosition
{
    public int Id { get; set; }

    [StringLength(100), Required]
    public string NameOfProduct { get; set; }

    [StringLength(15), Required]
    public string MeasurementUnit { get; set; }

    [Required]
    public int Quantity { get; set; }

    [Required]
    public int AdmissionDocumentId { get; set; }

    [Required, ForeignKey("AdmissionDocumentId")]
    public AdmissionDocument AdmissionDocument { get; set; }
}
