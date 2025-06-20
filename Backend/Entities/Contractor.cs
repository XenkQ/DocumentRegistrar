using System.ComponentModel.DataAnnotations;

namespace Backend.Entities;

internal class Contractor
{
    public int Id { get; set; }

    [Required]
    public string Symbol { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; }

    public List<AdmissionDocument> AdmissionDocuments { get; set; }
        = new List<AdmissionDocument>();
}
