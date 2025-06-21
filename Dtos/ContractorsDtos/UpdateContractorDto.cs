using System.ComponentModel.DataAnnotations;

namespace Dtos.ContractorsDtos;

public class UpdateContractorDto
{
    [Required]
    public string Symbol { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; }
}
