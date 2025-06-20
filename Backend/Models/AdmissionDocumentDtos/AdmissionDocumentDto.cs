using Backend.Models.Contractors;
using System.ComponentModel.DataAnnotations;

namespace Backend.Models.AdmissionDocument;

internal class AdmissionDocumentDto
{
    public int Id { get; set; }

    [Required]
    public DateTime Date { get; set; }

    [Required]
    public string Symbol { get; set; }

    [Required]
    public int ContractorId { get; set; }

    [Required]
    public string ContractorSymbol { get; set; }

    [Required]
    [StringLength(100)]
    public string ContractorName { get; set; }

    [Required]
    public List<DocumentPositionDto> DocumentPositions { get; set; }
        = new List<DocumentPositionDto>();
}
