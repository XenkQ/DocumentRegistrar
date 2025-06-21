using System.ComponentModel.DataAnnotations;

namespace Dtos.ContractorsDtos;

public class CreateContractorDto
{
    [Required]
    public string Symbol { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; }
}
