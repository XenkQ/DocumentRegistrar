using System.ComponentModel.DataAnnotations;

namespace Backend.Models.AdmissionDocumentDtos;

public class CreateAdmissionDocumentDto
{
    [Required]
    public DateOnly Date { get; set; }

    [Required]
    public string Symbol { get; set; }

    [Required]
    public int ContractorId { get; set; }
}