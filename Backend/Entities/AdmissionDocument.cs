using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Entities;

internal class AdmissionDocument
{
    public int Id { get; set; }

    [Required]
    public DateTime Date { get; set; }

    [Required]
    public string Symbol { get; set; }

    [Required]
    public int ContractorId { get; set; }

    [Required]
    [ForeignKey("ContractorId")]
    public Contractor Contractor { get; set; }

    [Required]
    public List<DocumentPosition> DocumentPositions { get; set; }
        = new List<DocumentPosition>();
}
