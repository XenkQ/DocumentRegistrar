using System.ComponentModel.DataAnnotations;

namespace Backend.Models.ContractorsDtos;

internal class UpdateContractorDto
{
    [Required]
    public string Symbol { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; }
}
