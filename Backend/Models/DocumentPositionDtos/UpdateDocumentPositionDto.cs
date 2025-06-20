using System.ComponentModel.DataAnnotations;

namespace Backend.Models.Contractors;

internal class UpdateDocumentPositionDto
{
    [StringLength(100)]
    public string NameOfProduct { get; set; }

    [StringLength(15)]
    public string MeasurementUnit { get; set; }

    public int Quantity { get; set; }

    public int AdmissionDocumentId { get; set; }
}
