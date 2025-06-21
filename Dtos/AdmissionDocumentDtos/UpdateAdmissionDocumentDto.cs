using System.ComponentModel.DataAnnotations;

namespace Dtos.AdmissionDocumentDtos;

public class UpdateAdmissionDocumentDto
{
    [Required]
    public DateOnly Date { get; set; }

    [Required]
    public string Symbol { get; set; }

    [Required]
    public int ContractorId { get; set; }
}
