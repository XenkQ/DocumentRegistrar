using Backend.Models.Contractors;
using System.ComponentModel.DataAnnotations;

namespace Backend.Models.AdmissionDocument;

internal class CreateAdmissionDocumentDto
{
    [Required]
    public DateTime Date { get; set; }

    [Required]
    public string Symbol { get; set; }

    [Required]
    public int ContractorId { get; set; }
}
