using System.ComponentModel.DataAnnotations;

namespace Backend.Models.AdmissionDocumentDtos;

internal class UpdateAdmissionDocumentDto
{
    [Required]
    public DateTime Date { get; set; }

    [Required]
    public string Symbol { get; set; }

    [Required]
    public int ContractorId { get; set; }
}
