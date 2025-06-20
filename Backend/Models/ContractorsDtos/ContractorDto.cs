using Backend.Models.AdmissionDocument;
using System.ComponentModel.DataAnnotations;

namespace Backend.Models.Contractors;

internal class ContractorDto
{
    public int Id { get; set; }

    [Required]
    public string Symbol { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; }

    public List<AdmissionDocumentDto> AdmissionDocuments { get; set; }
        = new List<AdmissionDocumentDto>();
}
