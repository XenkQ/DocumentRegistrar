using System.ComponentModel.DataAnnotations;

namespace Backend.Models.Contractors;

internal class UpdateContractorsDto
{
    public string Symbol { get; set; }

    [StringLength(100)]
    public string Name { get; set; }
}
