using Backend.Models.DocumentPositionDtos;
using System.ComponentModel.DataAnnotations;

namespace Backend.Models.AdmissionDocumentDtos;

public class AdmissionDocumentDto
{
    public int Id { get; set; }

    [Required]
    public DateOnly Date { get; set; }

    [Required]
    public string Symbol { get; set; }

    [Required]
    public int ContractorId { get; set; }

    [Required]
    public string ContractorSymbol { get; set; }

    [Required]
    [StringLength(100)]
    public string ContractorName { get; set; }

    public List<DocumentPositionDto> DocumentPositions { get; set; }
        = new List<DocumentPositionDto>();
}
