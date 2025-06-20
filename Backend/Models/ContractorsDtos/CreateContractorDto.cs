using Backend.Models.AdmissionDocument;
using System.ComponentModel.DataAnnotations;

namespace Backend.Models.Contractors;

internal class CreateContractorsDto
{
    [Required]
    public string Symbol { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; }
}
